using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private float totalTime;
    private int totalTries;
    [SerializeField] private List<RoomInfo> rooms = new List<RoomInfo>();
    public List<RoomInfo> Rooms => rooms;
    private static GameController instance;

    public bool ClassicMode;
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
    
    public void FinishRoom(float timeCurrentRoom, int tries)
    {
        RoomInfo roomInfo = new RoomInfo(timeCurrentRoom, tries);
        rooms.Add(roomInfo);
    }

    public float GetTotalTime()
    {
        totalTime = 0.0f;
        foreach (var roomInfo in rooms)
        {
            totalTime += roomInfo.time;
        }
        return totalTime;
    }
    public int GetTotalTries()
    {
        totalTries = 0;
        foreach (var roomInfo in rooms)
        {
            totalTries += roomInfo.tries;
        }
        return totalTries;
    }
    public void SetClassicMode(bool value)
    {
        ClassicMode = value;
    }
}