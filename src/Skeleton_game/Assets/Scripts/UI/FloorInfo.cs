using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloorInfo
{
    public static bool[] lastRoom = new[] { false, false, false, false }; // lb, rb, lt, rt
    public static bool[] clearedRoom = new[] { false, false, false, false }; // lb, rb, lt, rt

    public static int currentFloor = 1;

    public static void resetRoom()
    {
        for(int i = 0; i < clearedRoom.Length; i++)
        {
            clearedRoom[i] = false;
        }
    }

    public static void visitRoom(int position)
    {
        for (int i = 0; i < lastRoom.Length; i++)
        {
            lastRoom[i] = (i == position);
        }
    }

    public static int getLastVisitedRoom()
    {
        for (int i = 0; i < lastRoom.Length; i++)
        {
            if (lastRoom[i]) return i;
        }
        return -1; 
    }
}
