using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDatabase : MonoBehaviour
{
    public static DungeonDatabase instance;
    public Room[,] roomMap;

    public bool stillDungeon = false;
    public int roomSize;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    void Start()
    {
    }
}
