using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMapDisplayer : MonoBehaviour
{
    public static DungeonMapDisplayer instance;
    public Transform roomSpace;
    public List<RoomSpace> roomSpaceScripts = new List<RoomSpace>();
    public RoomSpace enteredRoomSpace;

    int roomSize;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("DIPLAYING");
        roomSize = DungeonDatabase.instance.roomSize;
        for (int i = 0; i < roomSize; i++)
        {
            for (int j = 0; j < roomSize; j++)
            {
                Debug.Log(DungeonDatabase.instance.roomMap[i, j]);
            }
        }
        for (int i = 0; i < roomSize; i++)
        {
            for (int j = 0; j < roomSize; j++)
            {
                Transform newRoomSpace = Instantiate(roomSpace);
                newRoomSpace.name = "Room" + (i + 1) + "." + (j + 1);
                newRoomSpace.SetParent(this.transform);
                RectTransform roomRect = newRoomSpace.GetComponent<RectTransform>();
                roomRect.anchorMin = new Vector2(0.2f * j + 0.05f, 1 - (0.2f * (i + 1) - 0.05f));
                roomRect.anchorMax = new Vector2(0.2f * (j + 1) - 0.05f, 1 - (0.2f * i + 0.05f));
                roomRect.offsetMin = Vector2.zero;
                roomRect.offsetMax = Vector2.zero;

                roomSpaceScripts.Add(newRoomSpace.GetComponent<RoomSpace>());
                newRoomSpace.GetComponent<RoomSpace>().number = i * roomSize + j;

                roomSpaceScripts[i * roomSize + j].room = DungeonDatabase.instance.roomMap[i , j];
                RoomImageChange(roomSpaceScripts[i * roomSize + j]); // 모든 방의 이미지 오브젝트 비활성화 (방이 활성화 되었다면 반복문 아래의 CheckRoom을 통해 활성화)
            }
        }
        RoomImageDecider(roomSpaceScripts);
        for (int i = 0; i < roomSpaceScripts.Count; i++)
        {
            RoomImageChange(roomSpaceScripts[i]);
            RoomSpaceImageChange(roomSpaceScripts[i]);
        }
    }
    public void RoomImageChange(RoomSpace _roomSpace)
    {
        if (_roomSpace.room.thisRoomIsActivated)
        {
            _roomSpace.transform.GetChild(0).gameObject.SetActive(true);
            _roomSpace.transform.GetChild(0).GetComponent<Image>().sprite = _roomSpace.room.roomImage;
        }
        else
        {
            _roomSpace.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void RoomImageDecider(List<RoomSpace> _roomSpace)
    {
        for (byte i = 0; i < _roomSpace.Count; i++)
        {
            if (_roomSpace[i].room.roomType == RoomType.StartPoint)
            {
                _roomSpace[i].room.roomImage = Resources.Load<Sprite>("RoomImages/startPos");
            }
            else if (_roomSpace[i].room.roomType == RoomType.Misc)
            {
                _roomSpace[i].room.roomImage = Resources.Load<Sprite>("RoomImages/slot");
            }
            else if (_roomSpace[i].room.roomType == RoomType.Enemy)
            {
                _roomSpace[i].room.roomImage = Resources.Load<Sprite>("RoomImages/empty");
            }
            else if (_roomSpace[i].room.roomType == RoomType.Crate)
            {
                _roomSpace[i].room.roomImage = Resources.Load<Sprite>("RoomImages/crate");
            }
            else if (_roomSpace[i].room.roomType == RoomType.Guild)
            {
                _roomSpace[i].room.roomImage = Resources.Load<Sprite>("RoomImages/shop");
            }
            else if (_roomSpace[i].room.roomType == RoomType.Boss)
            {
                _roomSpace[i].room.roomImage = Resources.Load<Sprite>("RoomImages/boss");
            }
        }
    }

    void RoomSpaceImageChange(RoomSpace _roomSpace)
    {
        string U = "";
        string D = "";
        string L = "";
        string R = "";
        if (_roomSpace.room.thisRoomIsActivated)
        {
            if (_roomSpace.room.upPassage)
                U = "U";
            if (_roomSpace.room.downPassage)
                D = "D";
            if (_roomSpace.room.leftPassage)
                L = "L";
            if (_roomSpace.room.rightPassage)
                R = "R";
            _roomSpace.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("PassageImages/" + U + D + L + R);
        }
        else
        {
            _roomSpace.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("PassageImages/noPassage");
        }
    }
}
