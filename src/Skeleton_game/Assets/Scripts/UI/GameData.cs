using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool[] lastRoom;
    public bool[] clearedRoom;
    public int currentSceneID;

    public GameData()
    {
        lastRoom = (bool[]) GameInfo.lastRoom.Clone();
        clearedRoom = (bool[]) GameInfo.clearedRoom.Clone();
        currentSceneID = GameInfo.currentSceneID;
    }
}
