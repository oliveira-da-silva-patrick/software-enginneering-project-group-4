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
            if (currentRoomName.Contains("LB"))
                GameInfo.VisitRoom(0);
            else if (currentRoomName.Contains("RB"))
                GameInfo.VisitRoom(1);
            else if (currentRoomName.Contains("LT"))
                GameInfo.VisitRoom(2);
            else if (currentRoomName.Contains("RT"))
                GameInfo.VisitRoom(3);
            else GameInfo.VisitRoom(-1);
            
            GameInfo.isRoomCleared();
            GameObject go = GameObject.Find("LevelLoader");
            LevelLoader levelloader = (LevelLoader)go.GetComponent(typeof(LevelLoader));
            levelloader.LoadNextLevel(roomToLoad);
            GameInfo.Save();
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        int position = roomPosition(scene.name);
        if (GameInfo.clearedRoom[position])
        {
            //Debug.Log("room cleared");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i].gameObject);
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    public int roomPosition(string roomName)
    {
        if (roomName.Contains("LB"))
            return 0;
        else if (roomName.Contains("RB"))
            return 1;
        else if (roomName.Contains("LT"))
            return 2;
        else if (roomName.Contains("RT"))
            return 3;
        else return 4;
    }
}
