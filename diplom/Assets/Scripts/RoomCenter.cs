using UnityEngine;
using System.Collections.Generic;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared;

    public List<GameObject> enemies = new List<GameObject>();

    public Room theRoom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (openWhenEnemiesCleared) 
        {
            theRoom.closeWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared) 
        { 
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                   
                    i--;
                }
            }

            if(enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }
    }
}
