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
    public bool NormalMode;
    public GameController gameController;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
                NormalMode = true;
                gameController.SetClassicMode(NormalMode);

            }
            else if (objectHit == doorObject_ChristmascMode && Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(audioClip);
                SceneManager.LoadScene(indexFirstRoomChristmasMode);
                NormalMode = false;
                gameController.SetClassicMode(NormalMode);

            }
        }
    }
}
