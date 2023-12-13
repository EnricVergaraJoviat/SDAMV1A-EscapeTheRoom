using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoomInfo
{
    public RoomInfo(float time, int tries)
    {
        this.time = time;
        this.tries = tries;
    }
    public float time;
    public int tries;
   
}
