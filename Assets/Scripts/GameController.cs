using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public float timer;
    public float totalTime;
    List<RoomInfo> rooms = new List<RoomInfo>();
    public string firstRoom;
    private static GameController instance;
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
   
    void Update()
    {
        if (startGame)
        {
            timer += Time.deltaTime;
            totalTime += Time.deltaTime;
            float formattedTime = Mathf.Floor(timer) + Mathf.Floor((timer % 1) * 100) / 100;
        }
    }

    public void StartGame()
    {
        totalTime = timer = 0.0f;
        rooms.Clear();
        startGame = true;
    }
    public void StartRoom()
    {
        timer = 0.0f;
        SceneManager.LoadScene(firstRoom);
    }

    public void FinishRoom()
    {
        RoomInfo roomInfo = new RoomInfo(timer, 0);
        rooms.Add(roomInfo);
    }
}