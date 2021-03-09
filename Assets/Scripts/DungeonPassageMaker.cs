using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonPassageMaker : MonoBehaviour
{
    public static DungeonPassageMaker instance;
    public Room[,] roomMap;
    public int roomSize;

    public int startPos;
    public int endPos;

    private List<int> roomQueue = new List<int>(); //길 뚫는 순서 대기열


    private void Awake()
    {
        instance = this;

        if (!DungeonDatabase.instance.stillDungeon)
        {
            int y, x;
            roomSize = Random.Range(4, 5); //a 이상 b 미만(이하가 아님, 범위 : a~(b-1))
            DungeonDatabase.instance.roomSize = roomSize;
            //Debug.Log("사이즈 : " + roomSize);
            roomMap = new Room[roomSize, roomSize];
            y = Random.Range(0, roomSize);
            if (y == 0 || y == roomSize - 1)
                x = Random.Range(0, roomSize);
            else
            {
                x = Random.Range(0, 1);
                if (x == 1) x = roomSize - 1;
            }
            roomQueue.Add((y * 10) + x); //시작점 추가
            startPos = roomQueue[0];
            for (byte i = 0; i < roomSize; i++)
            {
                for (byte j = 0; j < roomSize; j++)
                {
                    roomMap[i, j] = new Room(RoomType.Misc, null, true, true, true, true, false, false, false, false, false); //정신나갈거 같아
                }
            }
            //Debug.Log("NO PROBLEM TO MAKE ROOM");

            //Debug.Log("y:" + y + "x:" + x);
            int countQueue = 0;
            while (countQueue < 16)
            {
                //Debug.Log("FIRST, " + countQueue);
                if (roomQueue.Count == 0)
                    break;
                RoomUDLRChecker(roomQueue[0]);
                //Debug.Log("AFTER");
                countQueue++;
            }

            //Debug.Log("ROOM MAP TO ROOM MAP");
            DungeonDatabase.instance.roomMap = roomMap;
            //Debug.Log("STILL DUNGEON");
            DungeonDatabase.instance.stillDungeon = true;

            //Debug.Log("CHANGE TO ENEMY");

            for (int i = 0; i < roomSize; i++)
            {
                for (int j = 0; j < roomSize; j++)
                {
                    if (DungeonDatabase.instance.roomMap[i, j].thisRoomIsActivated)
                        DungeonDatabase.instance.roomMap[i, j].roomType = RoomType.Enemy;
                }
            }

            //Debug.Log("DECIDER");
            RoomTypeDecider(roomSize, startPos, endPos);
            //Debug.Log("DELETE");
            RoomTypeMiscPassageDelete(DungeonDatabase.instance.roomMap, roomSize);
            //Debug.Log("FILTER");
            RoomFilter(DungeonDatabase.instance.roomMap, roomSize);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RoomUDLRChecker(int curPos) //ROOM'S UP, DOWN, LEFT, RIGHT CHECKER
    {
        //Debug.Log("ENTERED");
        //현재 위치의 상하좌우에 방이 있는가 확인함
        //체크 순서는 편의상 상하좌우로 함
        //curPos 는 10의 자리 숫자가 y값, 1의 자리 숫자가 x값 (배열의 y, x임 / y:↓  x:→ / arr[y,x])
        sbyte up = -1;
        sbyte down = 1;
        sbyte left = -1;
        sbyte right = 1;
        //Debug.Log("POS CHECK");
        int y = curPos / 10; // 대기열 1순위의 y값
        int x = curPos % 10; // 대기열 1순위의 x값

        bool passageExist = false;
        bool noSpace = false;

        //Debug.Log(y + ", " + x);
        roomMap[y, x].thisRoomIsActivated = true;

        //상하좌우에 공간이 있는가 확인 (배열의 좌표를 넘어서는가 확인하는 과정)
        //상화좌우가 이미 길을 만들었던 공간일 경우 공간이 없다고 판정함 (상하좌우 각각 판정)
        if (y + up < 0) roomMap[y, x].upSpace = false;
        //Debug.Log("PASS 1");

        if (y + down >= roomSize) roomMap[y, x].downSpace = false;
        //Debug.Log("PASS 2");

        if (x + left < 0) roomMap[y, x].leftSpace = false;
        //Debug.Log("PASS 3");

        if (x + right >= roomSize) roomMap[y, x].rightSpace = false;
        //Debug.Log("PASS 4");

        //상하좌우 공간 없음 판정이면 반복문을 들어가면 안되므로 만듬
        if (!roomMap[y, x].upSpace && !roomMap[y, x].downSpace
            && !roomMap[y, x].leftSpace && !roomMap[y, x].rightSpace)
            noSpace = true;
        //Debug.Log("NO PROBLEM TO noSpace");

        while (!passageExist && !noSpace) //통로가 최소 한개가 나올 때 까지
        {
            //Debug.Log("WHILE");
            if (roomMap[y, x].upSpace) //위에 공간이 있는가
            {
                //Debug.Log("upSpace");
                if (Random.Range(0, 2) == 1)
                {
                    //Debug.Log("upPassage");
                    roomMap[y, x].upPassage = true;
                    passageExist = true;

                    roomMap[y + up, x].downPassage = true; //[y,x] 기준 위의 공간이 위 아래로 다닐 수 있는 쌍방통행 설정

                    roomQueue.Add(curPos + (up * 10));
                }
                else roomMap[y, x].upPassage = false;
                if (y + up > -1) roomMap[y + up, x].downSpace = false; //[y+up,x] 기준 아래의 공간은 더이상 통로 개척 대상이 아님을 설정
            }

            if (roomMap[y, x].downSpace) //아래에 공간이 있는가
            {
                //Debug.Log("downSpace");
                if (Random.Range(0, 2) == 1)
                {
                    //Debug.Log("downPassage");
                    roomMap[y, x].downPassage = true;
                    passageExist = true;

                    roomMap[y + down, x].upPassage = true; //[y,x] 기준 아래의 공간이 위 아래로 다닐 수 있는 쌍방통행 설정

                    roomQueue.Add(curPos + (down * 10));
                }
                else roomMap[y, x].downPassage = false;
                if (y + down < roomSize) roomMap[y + down, x].upSpace = false; //[y+down,x] 기준 위의 공간은 더이상 통로 개척 대상이 아님을 설정
            }

            if (roomMap[y, x].leftSpace) //왼쪽에 공간이 있는가
            {
                //Debug.Log("leftSpace");
                if (Random.Range(0, 2) == 1)
                {
                    //Debug.Log("leftPassage");
                    roomMap[y, x].leftPassage = true;
                    passageExist = true;

                    roomMap[y, x + left].rightPassage = true; //[y,x] 기준 왼쪽의 공간이 좌우로 다닐 수 있는 쌍방통행 설정

                    roomQueue.Add(curPos + left);
                }
                else roomMap[y, x].leftPassage = false;
                if (x + left > -1) roomMap[y, x + left].rightSpace = false; //[y,x+left] 기준 오른쪽의 공간은 더이상 통로 개척 대상이 아님을 설정
            }

            if (roomMap[y, x].rightSpace) //오른쪽에 공간이 있는가
            {
                //Debug.Log("rightSpace");
                if (Random.Range(0, 2) == 1)
                {
                    //Debug.Log("rightPassage");
                    roomMap[y, x].rightPassage = true;
                    passageExist = true;

                    roomMap[y, x + right].leftPassage = true; //[y,x] 기준 오른쪽의 공간이 좌우로 다닐 수 있는 쌍방통행 설정

                    roomQueue.Add(curPos + right);
                }
                else roomMap[y, x].rightPassage = false;
                if (x + right < roomSize) roomMap[y, x + right].leftSpace = false; //[y,x+right] 기준 왼쪽의 공간은 더이상 통로 개척 대상이 아님을 설정
            }
        }
        //Debug.Log("1:"+roomQueue[0]);
        endPos = roomQueue[0];
        //Debug.Log(endPos);
        roomQueue.Remove(roomQueue[0]);
        //Debug.Log("REMOVED");
        if (y + up > -1)
        {
            roomMap[y + up, x].downSpace = false;
        }
        if (y + down < roomSize)
        {
            roomMap[y + down, x].upSpace = false;
        }
        if (x + left > -1)
        {
            roomMap[y, x + left].rightSpace = false;
        }
        if (x + right < roomSize)
        {
            roomMap[y, x + right].leftSpace = false;
        }
    }
    private void RoomTypeDecider(int _roomSize, int _startPos, int _endPos)
    {
        bool GuildExist = false;
        bool crateExist = false;

        for (int i = 0; i < _roomSize; i++)
        {
            for (int j = 0; j < _roomSize; j++)
            {
                if (i * 10 + j == _startPos) //시작 좌표
                {
                    DungeonDatabase.instance.roomMap[i, j].roomType = RoomType.StartPoint; //사실 이 부분은 UDLR 이전에 설정해도 되지만 편의상 여기에서 설정함
                }
                else if (i * 10 + j == _endPos) //마지막에 탐색했던 방을 보스방으로
                {
                    DungeonDatabase.instance.roomMap[i, j].roomType = RoomType.Boss; //이 또한 UDLR이 끝난 직후 해도 되지만 편의상 여기에서 설정
                }
            }
        }

        while (!GuildExist || !crateExist)
        {
            for (int i = 0; i < _roomSize; i++)
            {
                for (int j = 0; j < _roomSize; j++)
                {
                    if (DungeonDatabase.instance.roomMap[i, j].roomType == RoomType.Enemy)
                    {
                        if (!GuildExist) //상점이 존재하지 않으면 실행
                        {
                            if (Random.Range(0, 2) == 1)
                            {
                                DungeonDatabase.instance.roomMap[i, j].roomType = RoomType.Guild;
                                GuildExist = true;
                            }
                        }
                        else if (!crateExist) //상자방이 존재하지 않으면 실행
                        {
                            if (Random.Range(0, 2) == 1)
                            {
                                DungeonDatabase.instance.roomMap[i, j].roomType = RoomType.Crate;
                                crateExist = true;
                            }
                        }
                    }
                }
            }
        }
    }
    private void RoomTypeMiscPassageDelete(Room[,] _roomMap, int _roomSize)
    {
        for (int i = 0; i < _roomSize; i++)
        {
            for (int j = 0; j < _roomSize; j++)
            {
                if (_roomMap[i , j].roomType == RoomType.Misc)
                {
                    _roomMap[i, j].upPassage = false;
                    _roomMap[i, j].downPassage = false;
                    _roomMap[i, j].leftPassage = false;
                    _roomMap[i, j].rightPassage = false;
                }
            }
        }
    }
    private void RoomFilter(Room[,] _roomMap, int _roomSize)
    {
        sbyte up = -1;
        sbyte down = 1;
        sbyte left = -1;
        sbyte right = 1;
        for (int i = 0; i < _roomSize; i++)
        {
            for (int j = 0; j < _roomSize; j++)
            {
                if (_roomMap[i, j].roomType == RoomType.Misc)
                {
                    //Debug.Log("MISC : " + i + ", " + j);
                    if (i + up > -1)
                        _roomMap[i + up, j].downPassage = false;
                    if (i + down < roomSize)
                        _roomMap[i + down, j].upPassage = false;
                    if (j + left > -1)
                        _roomMap[i, j + left].rightPassage = false;
                    if (j + right < roomSize)
                        _roomMap[i, j + right].leftPassage = false;
                }
                else
                {
                    //Debug.Log("ELSE : " + i + ", " + j);
                    if (i + up > -1)
                    {
                        if (_roomMap[i + up, j].downPassage)
                            _roomMap[i, j].upPassage = true;
                    }

                    if (i + down < roomSize)
                    {
                        if (_roomMap[i + down, j].upPassage)
                            _roomMap[i, j].downPassage = true;
                    }

                    if (j + left > -1)
                    {
                        if (_roomMap[i, j + left].rightPassage)
                            _roomMap[i, j].leftPassage = true;
                    }

                    if (j + right < roomSize)
                    {
                        if (_roomMap[i, j + right].leftPassage)
                            _roomMap[i, j].rightPassage = true;
                    }
                }
            }
        }
    }
}