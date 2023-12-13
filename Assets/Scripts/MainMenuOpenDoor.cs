using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuOpenDoor : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject doorObject_ClassicMode;
    public GameObject doorObject_ChristmascMode;
    // Start is called before the first frame update
    public int indexFirstRoomClassicMode;
    public int indexFirstRoomChristmasMode;
    private float direction1;
    private float direction2;
    private float time = 0f;
    private bool openedClassic = false;
    private bool openedChristmas = false;
    void Start()
    {
        direction1 = 90f;
        direction2 = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        doorObject_ClassicMode.transform.eulerAngles = new Vector3(0f, direction1, 0f);
        doorObject_ChristmascMode.transform.eulerAngles = new Vector3(0f, direction2, 0f);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit == doorObject_ClassicMode && Input.GetMouseButtonDown(0))
            {
                openedClassic = true;
                audioSource.PlayOneShot(audioClip);
            }
            else if (objectHit == doorObject_ChristmascMode && Input.GetMouseButtonDown(0))
            {
                openedChristmas = true;
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
    private void FixedUpdate()
    {
        if (openedClassic)
        {
            if (time < 90f)
            {
                time += Time.deltaTime * 60f;
                direction1 += 1f;
            }
            else
            {
                openedClassic = false;
                SceneManager.LoadScene(indexFirstRoomClassicMode);
            }
        }
        else if (openedChristmas)
        {
            if (time < 90f)
            {
                time += Time.deltaTime * 60f;
                direction2 += 1f;
            }
            else
            {
                openedChristmas = false;
                SceneManager.LoadScene(indexFirstRoomChristmasMode);
            }
        }
    }
}
