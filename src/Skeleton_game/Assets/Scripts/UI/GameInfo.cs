using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class GameInfo
{
    public static bool[] lastRoom = new[] { false, false, false, false }; // lb, rb, lt, rt
    public static bool[] clearedRoom = new[] { false, false, false, false, false }; // lb, rb, lt, rt, floor

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

    public static int returnRoomIndex(string currentRoomName)
    {
        if (currentRoomName.Contains("LB"))
            return 0;
        else if (currentRoomName.Contains("RB"))
            return 1;
        else if (currentRoomName.Contains("LT"))
            return 2;
        else if (currentRoomName.Contains("RT"))
            return 3;
        else if (currentRoomName.Contains("Floor"))
            return 4;
        else
            return -1;
    }

    public static void isRoomCleared()
    {
        string currentRoomName = SceneManager.GetActiveScene().name;
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bool result = count == 0;

        if(result != clearedRoom[returnRoomIndex(currentRoomName)])
        {
            SkillTree.ECTS += 6;
        }
        // Debug.Log(SkillTree.ECTS);

        if (currentRoomName.Contains("LB"))
            clearedRoom[0] = result;
        else if (currentRoomName.Contains("RB"))
            clearedRoom[1] = result;
        else if (currentRoomName.Contains("LT"))
            clearedRoom[2] = result;
        else if (currentRoomName.Contains("RT"))
            clearedRoom[3] = result;
        else if (currentRoomName.Contains("Floor"))
            clearedRoom[4] = result;
    }

    public static bool isFloorCleared()
    {
        for (int i = 0; i < clearedRoom.Length; i++)
        {
            if (clearedRoom[i] == false) return false;
        }
        return true;
    }
}
