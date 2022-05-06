using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomCondition : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        isRoomCleared();
    }

    public static void isRoomCleared()
    {
        string currentRoomName = SceneManager.GetActiveScene().name;
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bool result = count == 0;
        if (currentRoomName.Contains("LB"))
            GameInfo.clearedRoom[0] = result;
        else if (currentRoomName.Contains("RB"))
            GameInfo.clearedRoom[1] = result;
        else if (currentRoomName.Contains("LT"))
            GameInfo.clearedRoom[2] = result;
        else if (currentRoomName.Contains("RT"))
            GameInfo.clearedRoom[3] = result;
        else if (currentRoomName.Contains("Floor"))
            GameInfo.clearedRoom[4] = result;
    }
}
