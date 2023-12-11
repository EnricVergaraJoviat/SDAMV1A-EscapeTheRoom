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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit == doorObject_ClassicMode && Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(audioClip);
                SceneManager.LoadScene(indexFirstRoomClassicMode);
                
            }else if (objectHit == doorObject_ChristmascMode && Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(audioClip);
                SceneManager.LoadScene(indexFirstRoomChristmasMode);
                
            }
        }
    }
}
