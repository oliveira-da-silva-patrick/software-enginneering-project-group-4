using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public static class GameInfo
{
    public static bool[] lastRoom = new[] { false, false, false, false }; // lb, rb, lt, rt
    public static bool[] clearedRoom = new[] { false, false, false, false, false }; // lb, rb, lt, rt, floor
    public static bool[,] allclearedRoomsEver = null;

    public static int currentSceneID = 1;

    public static void newGame()
    {
        Load();
        SaveLoadSystem.deleteSaveFile();
        if (allclearedRoomsEver == null)
        {
            fillEmpty();
        }
        ResetRoom();
        for (int i = 0; i < lastRoom.Length; i++)
        {
            lastRoom[i] = false;
        }
        Save();

    }

    //private static bool areAllRoomsCleared()
    //{
    //    bool cleared = false;
    //    foreach (bool i in clearedRoom)
    //    {
    //        if (!i)
    //        {
    //            cleared = i;
    //        }
    //    }
    //    return cleared;
    //}

    //private static void enableElevator()
    //{
    //    if(currentSceneID == 4)
    //    {
    //        GameObject escalator = GameObject.Find("Escalator");
    //        if (!escalator.activeSelf)
    //        {
    //            escalator.SetActive(true);
    //        }
    //    }
    //}

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
            if(data.allClearedRoomsEver != null)
            {
                allclearedRoomsEver = (bool[,])data.allClearedRoomsEver.Clone();
            } else
            {
                fillEmpty();
            }
        } else
        {
            currentSceneID = 0;
        }
        //if (areAllRoomsCleared())
        //{
        //    enableElevator();
        //}
    }

    public static void fillEmpty()
    {
        allclearedRoomsEver = new bool[6, 5];
        for (int i = 0; i < allclearedRoomsEver.GetLength(0); i++)
        {
            for (int j = 0; j < allclearedRoomsEver.GetLength(1); j++)
            {
                allclearedRoomsEver[i, j] = false;
            }
        }
        GameInfo.Save();
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

    public static int returnFloorIndex(string currentRoomName)
    {
        if (currentRoomName.Contains("1"))
            return 0;
        else if (currentRoomName.Contains("2"))
            return 1;
        else if (currentRoomName.Contains("3"))
            return 2;
        else if (currentRoomName.Contains("4"))
            return 3;
        else if (currentRoomName.Contains("5"))
            return 4;
        else if (currentRoomName.Contains("6"))
            return 5;
        else
            return -1;
    }

    public static void isRoomCleared()
    {
        string currentRoomName = SceneManager.GetActiveScene().name;
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bool result = count == 0;
        int floor = returnFloorIndex(currentRoomName);

        // clearedroom updated below
        if(result && !clearedRoom[returnRoomIndex(currentRoomName)] && !allclearedRoomsEver[floor , returnRoomIndex(currentRoomName)])
        {
            SkillTree.ECTS += 6;
            if(floor == 5)
            {
                SkillTree.ECTS += 24;
            }
            allclearedRoomsEver[floor, returnRoomIndex(currentRoomName)] = true;
            SaveLoadSystem.SaveSkillTree();
        }

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

        Save();
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
