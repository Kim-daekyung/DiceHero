using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    void Awake()
    {
        DungeonDatabase.instance.stillDungeon = false;
        //DungeonDatabase.instance.activatedRoom.RemoveRange(0, DungeonDatabase.instance.activatedRoom.Count);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
