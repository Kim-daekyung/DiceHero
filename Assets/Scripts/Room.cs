using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    StartPoint, //시작점
    Enemy,      //적이 있는 방
    Guild,       //상점 방
    Crate,      //상자 방
    Boss,       //보스 방
    Misc        //기타
}

[System.Serializable]
public class Room
{
    public RoomType roomType;
    public Sprite roomImage;

    public bool upSpace;
    public bool downSpace;
    public bool leftSpace;
    public bool rightSpace;

    public bool upPassage;
    public bool downPassage;
    public bool leftPassage;
    public bool rightPassage;

    public bool thisRoomIsActivated;

    public Room(RoomType _roomType, Sprite _roomImage,
        bool _upSpace, bool _downSpace, bool _leftSpace, bool _rightSpace,
        bool _upPassage, bool _downPassage, bool _leftPassage, bool _rightPassage,
        bool _thisRoomIsActivated)
    {
        roomType = _roomType;
        roomImage = _roomImage;

        upSpace = _upSpace;
        downSpace = _downSpace;
        leftSpace = _leftSpace;
        rightSpace = _rightSpace;

        upPassage = _upPassage;
        downPassage = _downPassage;
        leftPassage = _leftPassage;
        rightPassage = _rightPassage;

        thisRoomIsActivated = _thisRoomIsActivated;
    }
}