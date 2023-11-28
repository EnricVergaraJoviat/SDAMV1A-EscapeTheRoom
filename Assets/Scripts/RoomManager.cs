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
    
    //---Setup custom room:
    public int solutionCode;
    public string nextRoom;
    public GameObject[] lights;
    public float timeToShowLights = 100;

    //---Private variables:
    private int tries;
    private float timer;
    private float totalTime;
    private GameController gc = null;
    private bool roomSolved;
    
    void Start()
    {
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
            light.SetActive(false);
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
        if (timer > timeToShowLights)
        {
            foreach (var light in lights)
            {
                light.SetActive(true);
            }
        }

        int minutes = (int)timer / 60;
        int seconds = (int)timer % 60;

        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        labelTimeInCurrentRoom.text = timeText;
        
        minutes = (int)totalTime / 60;
        seconds = (int)totalTime % 60;

        timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        labelTotalTime.text = timeText;
        
        
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
            Debug.Log("The CODE is Correct!");
            
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

    public void GoNextRoom()
    {
        SceneManager.LoadScene(nextRoom);
    }
}
