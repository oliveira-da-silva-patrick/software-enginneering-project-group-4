using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool[] lastRoom;
    public bool[] clearedRoom;
    public int currentSceneID;
    public bool[,] allClearedRoomsEver;

    public GameData()
    {
        lastRoom = (bool[]) GameInfo.lastRoom.Clone();
        clearedRoom = (bool[]) GameInfo.clearedRoom.Clone();
        currentSceneID = GameInfo.currentSceneID;
        if (GameInfo.allclearedRoomsEver != null)
        {
            allClearedRoomsEver = (bool[,])GameInfo.allclearedRoomsEver.Clone();
        } else
        {
            allClearedRoomsEver = null;
        }
    }
}
