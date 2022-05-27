using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public static class GameInfo
{
    // this array works similar to radiobuttons, not more than one value should be true. the one which is true corresponds to the last visited room
    public static bool[] lastRoom = new[] { false, false, false, false }; // lb, rb, lt, rt

    // saves which rooms have been cleared in the current floor
    public static bool[] clearedRoom = new[] { false, false, false, false, false }; // lb, rb, lt, rt, floor

    // saves whose room's ects has already been claimed
    public static bool[,] allclearedRoomsEver = null;

    public static int currentSceneID = 1;

    // is called when starting a new run
    public static void newGame()
    {
        Load(); // loads everything as if the player would continue, because not everything should be deleted
        SaveLoadSystem.deleteSaveFile();
        if (allclearedRoomsEver == null) // instantiates the ects flags if not already done
        {
            fillEmpty();
        }
        // sets the flags to false
        ResetRoom();
        for (int i = 0; i < lastRoom.Length; i++)
        {
            lastRoom[i] = false;    
        }
        Save(); // save what has beeen changed

    }

    // sets the floor flags to false
    public static void ResetRoom()
    {
        for(int i = 0; i < clearedRoom.Length; i++)
        {
            clearedRoom[i] = false;
        }
    }

    // sets the last room's flag to true and the other's to false
    public static void VisitRoom(int position)
    {
        for (int i = 0; i < lastRoom.Length; i++)
        {
            lastRoom[i] = (i == position);
        }
    }

    // returns which room has been visitted last
    public static int GetLastVisitedRoom()
    {
        for (int i = 0; i < lastRoom.Length; i++)
        {
            if (lastRoom[i]) return i;
        }
        return -1; 
    }

    // saves the current state of the run
    public static void Save()
    {
        currentSceneID = SceneManager.GetActiveScene().buildIndex;
        SaveLoadSystem.SaveGameInfo();
    }

    // loads the data stored in the save file
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
    }

    // sets the ects flags to false
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

    // translates the rooms to numbers that can be worked with
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

    // return the floor we are currently in
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

    // checks if the room has been cleared, adds ects if first time clearing the room and changes the current state of the corresponding flag
    public static void isRoomCleared()
    {
        string currentRoomName = SceneManager.GetActiveScene().name;
        if(!currentRoomName.Equals("Floor_0"))
        {
            int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
            bool result = count == 0;
            int floor = returnFloorIndex(currentRoomName);
            if(allclearedRoomsEver == null)
            {
                fillEmpty();
            }

            // clearedroom updated below
            if (result && !clearedRoom[returnRoomIndex(currentRoomName)] && !allclearedRoomsEver[floor, returnRoomIndex(currentRoomName)])
            {
                SkillTree.ECTS += 6;
                if (floor == 5)
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
    }

    // checks if the whole floor has been cleared
    public static bool isFloorCleared()
    {
        for (int i = 0; i < clearedRoom.Length; i++)
        {
            if (clearedRoom[i] == false) return false;
        }
        return true;
    }
}
