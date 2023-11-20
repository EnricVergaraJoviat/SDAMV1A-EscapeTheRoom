using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public TextMeshProUGUI labelTotalTime;
    public TMP_InputField inputF_Solution;
    public int solutionCode;
    public string nextRoom;
    public GameObject[] lights;
    public float timeToShowLights = 100;

    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        foreach (var light in lights)
        {
            light.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToShowLights)
        {
            foreach (var light in lights)
            {
                light.SetActive(true);
            }
        }

        labelTotalTime.text = "Temps total: "+ timer;
    }

    public void CheckSolution()
    {
        if (inputF_Solution.text == this.solutionCode.ToString())
        {
            Debug.Log("The CODE is Correct!");
            SceneManager.LoadScene(nextRoom);
        }
        else
        {
            Debug.Log("The CODE is Wrong!");
        }
    }
}
