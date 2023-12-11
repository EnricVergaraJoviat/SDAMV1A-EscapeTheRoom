using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    List<RoomInfo> rooms = new List<RoomInfo>();
    public string firstRoom;
    private static GameController instance;

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
        
    }

    public void StartGame()
    {
        rooms.Clear();
        SceneManager.LoadScene(firstRoom);
    }
    
    public void FinishRoom(float timeCurrentRoom, int tries)
    {
        RoomInfo roomInfo = new RoomInfo(timeCurrentRoom, tries);
        rooms.Add(roomInfo);
    }

    public float GetTotalTime()
    {
        float totalTime = 0.0f;
        foreach (var roomInfo in rooms)
        {
            totalTime += roomInfo.time;
        }
        return totalTime;
    }
}