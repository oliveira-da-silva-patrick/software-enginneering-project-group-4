using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadRoom : MonoBehaviour
{
    public string roomToLoad;

    void OnTriggerEnter2D(Collider2D other)
    {
        string currentRoomName = SceneManager.GetActiveScene().name;
        if (other.gameObject.name == "Main_player")
        {
            Debug.Log(currentRoomName + " -> " + roomToLoad);
            if (currentRoomName.Contains("LB"))
                FloorInfo.visitRoom(0);
            else if (currentRoomName.Contains("RB"))
                FloorInfo.visitRoom(1);
            else if (currentRoomName.Contains("LT"))
                FloorInfo.visitRoom(2);
            else if (currentRoomName.Contains("RT"))
                FloorInfo.visitRoom(3);
            else FloorInfo.visitRoom(-1);
            SceneManager.LoadScene(roomToLoad);
        }
    }
}
