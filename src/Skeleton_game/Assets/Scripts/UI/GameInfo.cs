using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class GameInfo
{
    public static bool[] lastRoom = new[] { false, false, false, false }; // lb, rb, lt, rt
    public static bool[] clearedRoom = new[] { false, false, false, false }; // lb, rb, lt, rt

    public static int currentSceneID = 1;

    public static void ResetRoom()
    {
        for(int i = 0; i < clearedRoom.Length; i++)
        {
            clearedRoom[i] = false;
        }
    }

    public static void VisitRoom(int position)
    {
        for (int i = 0; i < lastRoom.Length; i++)
        {
            lastRoom[i] = (i == position);
        }
    }

    public static int GetLastVisitedRoom()
    {
        for (int i = 0; i < lastRoom.Length; i++)
        {
            if (lastRoom[i]) return i;
        }
        return -1; 
    }

    public static void Save()
    {
        currentSceneID = SceneManager.GetActiveScene().buildIndex;
        SaveLoadSystem.SaveGameInfo();
    }

    public static void Load()
    {
        GameData data = SaveLoadSystem.LoadGameInfo();

        if (data != null)
        {
            lastRoom = (bool[]) data.lastRoom.Clone();
            clearedRoom = (bool[]) data.clearedRoom.Clone();
            currentSceneID = data.currentSceneID;
        }
    }
}
