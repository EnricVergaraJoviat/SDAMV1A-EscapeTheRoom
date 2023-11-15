using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public float timer;
    public RoomInfo[] rooms;
    public string nextRoom;
    private static RoomManager instance;
    private bool startGame = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        timer = 0f;
    }

   
    void Update()
    {
        
        if (startGame)
        {
            timer += Time.deltaTime;
            // Format the timer to x.xx seconds
            float formattedTime = Mathf.Floor(timer) + Mathf.Floor((timer % 1) * 100) / 100;

        }
    }

    public void onClick()
    {
        startGame = true;
        SceneManager.LoadScene(nextRoom);
    }
}