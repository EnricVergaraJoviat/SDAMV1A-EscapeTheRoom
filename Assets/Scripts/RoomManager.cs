using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    //----Links to UI----
    [Header("________________________")]
    [Header("NO TOCAR RES D'AIXÒ:")]
    public TextMeshProUGUI labelTimeInCurrentRoom;
    public TextMeshProUGUI labelTotalTime;
    public TMP_InputField inputF_Solution;
    public GameObject panelRoomSolved;
    public GameObject[] panelToHideWithVictory;
    public TextMeshProUGUI labelTries;
    public AudioClip[] audio_code_correct;
    public AudioClip audio_code_wrong;
    public AudioClip audio_next;
    public AudioSource audioSource;
    public AudioClip singleKeyType;
    public ProgressBar pb;
    public GameObject particleConfetti1;
    public GameObject particleConfetti2;
    public TextMeshProUGUI labelAuthor1;
    public TextMeshProUGUI labelAuthor2;
    public TextMeshProUGUI labelTitle;
    public TextMeshProUGUI labelSubtitle;
    public TextMeshProUGUI labelLogo;
    public GameObject popUpObject;
    public GraphicRaycaster raycaster;
    public Image imgLight;
    
    //---Setup custom room:
    [Header("________________________")]
    [Header("CONFIGURAR AIXÒ:")]
    public Transform cameraTransform;
    public GameObject door;
    public int solutionCode;
    public int nextRoom;
    public Light[] lights;
    public float timeToShowLights = 100;
    public string author1;
    public string author2;
    public string title;
    public string subtitle;
    public float durationLightAnimation = 2.0f;
    
    //---Private variables:
    private int tries;
    private float timer;
    private float totalTime;
    private float totalTries;
    private GameController gc = null;
    private bool roomSolved;
    private bool isTimeToShowLights = false;
    private float[] initialIntensities;
    
    void Start()
    {
        
        timeToShowLights = 30;
        initialIntensities = new float[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            initialIntensities[i] = lights[i].intensity;
        }
        
        popUpObject.SetActive(false);
        raycaster.enabled = false;
        labelAuthor1.text = author1;
        labelAuthor2.text = author2;
        labelTitle.text = title;
        labelSubtitle.text = subtitle;
        labelLogo.text = "Escape Room\n" + title;
        tries = 0;
        panelRoomSolved.SetActive(false);
        roomSolved = false;
        GameObject o = GameObject.FindGameObjectWithTag("GameController");
        if (o != null)
        {
            gc = o.GetComponent<GameController>();
            if (gc != null)
            {
                totalTime = gc.GetTotalTime();
                totalTries = gc.GetTotalTries();
            }
            else
            {
                Debug.LogWarning("GameController object wihtout script GameController");
            }
        }
        else
        {
            Debug.LogWarning("There's no GameController");
            totalTime = 0.0f;
        }
        timer = 0.0f;
        foreach (var light in lights)
        {
            light.enabled = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (roomSolved)
        {
            
            return;
        }
        
        timer += Time.deltaTime;
        totalTime += Time.deltaTime;
        if (timer > timeToShowLights && !isTimeToShowLights)
        {
            isTimeToShowLights = true;
            foreach (var light in lights)
            {
                light.enabled = true;
            }

            if (lights.Length > 0)
            {
                popUpObject.SetActive(true);
                
            }
            StartCoroutine(ChangeIntensity());
            
        }

        int minutes = (int)timer / 60;
        int seconds = (int)timer % 60;

        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        labelTimeInCurrentRoom.text = timeText;
        
        minutes = (int)totalTime / 60;
        seconds = (int)totalTime % 60;

        timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        labelTotalTime.text = timeText;

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
        {
            labelSubtitle.text = ""+solutionCode;
        }
        
    }

    public void CheckSolution()
    {
        audioSource.PlayOneShot(audio_next);
        if (inputF_Solution.text == this.solutionCode.ToString())
        {
            foreach (var o in panelToHideWithVictory)
            {
                o.SetActive(false);
            }
            popUpObject.SetActive(false);
            Debug.Log("The CODE is Correct!");
            StartCoroutine(RotateDoor(100, 4.0f));
            StartCoroutine(AnimateProgressBar(0.0f, 25.0f, 4.0f));
            StartCoroutine(ShowConfetti());
            int randomIndex = Random.Range(0, audio_code_correct.Length);
            AudioClip randomClip = audio_code_correct[randomIndex];
            audioSource.clip = randomClip;
            audioSource.Play();
            
            panelRoomSolved.SetActive(true);
            roomSolved = true;
            panelRoomSolved.SetActive(true);
            if (gc != null)
            {
                gc.FinishRoom(timer, tries);
            }
        }
        else
        {
            tries++;
            labelTries.text ="" + tries;
            labelTries.color = Color.red;
            Debug.Log("The CODE is Wrong!");
            audioSource.clip = audio_code_wrong;
            audioSource.Play();
        }
    }

    private IEnumerator ShowConfetti()
    {
        for (int i = 0; i < 3; i++) // Llamará la función 3 veces
        {
            GameObject.Instantiate(particleConfetti1, cameraTransform.position + cameraTransform.forward*1 +cameraTransform.right*1, Quaternion.identity);
            GameObject.Instantiate(particleConfetti2, cameraTransform.position + cameraTransform.forward*1 +cameraTransform.right*1, Quaternion.identity);
    
            GameObject.Instantiate(particleConfetti1, cameraTransform.position + cameraTransform.forward*1 -cameraTransform.right*1, cameraTransform.rotation);
            GameObject.Instantiate(particleConfetti2, cameraTransform.position + cameraTransform.forward*1 -cameraTransform.right*1, cameraTransform.rotation);

            yield return new WaitForSeconds(1f); // Espera 1/3 de segundo entre cada llamada
        }
        
    }

    
    public void GoNextRoom()
    {
        SceneManager.LoadScene(nextRoom);
    }
    
    
    // Coroutina para rotar la puerta
    IEnumerator RotateDoor(float angle, float duration)
    {
        Quaternion initialRotation = door.transform.rotation;
        Quaternion finalRotation = door.transform.rotation * Quaternion.Euler(0, angle, 0);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            door.transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        door.transform.rotation = finalRotation; // Asegura que la rotación termine exactamente en el ángulo final
    }
    
    IEnumerator AnimateProgressBar(float startValue, float endValue, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float currentPercent = Mathf.Lerp(startValue, endValue, elapsed / duration);
            pb.currentPercent = currentPercent;
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        pb.currentPercent = endValue; // Asegura que el valor final sea exactamente endValue
    }

    public void OnChangedInputFiled()
    {
        audioSource.PlayOneShot(singleKeyType);
    }
    
    IEnumerator ChangeIntensity()
    {
        while (true)
        {
            // De 1 a 2
            yield return LerpIntensity(0f, 1f, durationLightAnimation);
            // De 2 a 1
            yield return LerpIntensity(1f, 0f, durationLightAnimation);
        }
    }

    IEnumerator LerpIntensity(float startIntensity, float endIntensity, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            float intensity = Mathf.Lerp(startIntensity, endIntensity, time / duration);
            int index = 0;
            foreach (var light in lights)
            {
                light.intensity = intensity*initialIntensities[index];
                Color color = imgLight.color;
                color.a = intensity;
                imgLight.color = color;
                index++;
            }
            time += Time.deltaTime;
            yield return null;
        }
        // Asegurar que las luces lleguen a la intensidad final
        foreach (var light in lights)
        {
            light.intensity = endIntensity;
        }
    }
    
}
