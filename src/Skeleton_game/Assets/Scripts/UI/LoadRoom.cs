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
                GameInfo.VisitRoom(0);
            else if (currentRoomName.Contains("RB"))
                GameInfo.VisitRoom(1);
            else if (currentRoomName.Contains("LT"))
                GameInfo.VisitRoom(2);
            else if (currentRoomName.Contains("RT"))
                GameInfo.VisitRoom(3);
            else GameInfo.VisitRoom(-1);
            SceneManager.LoadScene(roomToLoad);
            GameInfo.Save();
        }
    }
}
