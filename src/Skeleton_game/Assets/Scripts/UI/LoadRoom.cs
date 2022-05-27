using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// this script tells the player were to "spawn" and what rooms to load
public class LoadRoom : MonoBehaviour
{
    public string roomToLoad; // the room we are about to visit

    // method executed by doors when colliding with them
    void OnTriggerEnter2D(Collider2D other)
    {
        string currentRoomName = SceneManager.GetActiveScene().name;
        bool go = true; // going from one room to another should always be allowed
        if(currentRoomName.Contains("Floor_") && roomToLoad.Contains("Floor_")) // checks if the floor has been cleared when trying to move from one floor to another
        {
            go = GameInfo.isFloorCleared();
        }
        if(currentRoomName.Equals("Floor_0")) // if the current floor is the beginning floor, just go, the flags will tell not to go
        {
            go = true;
        }
        if (go && other.gameObject.name == "Main_player") // checks if it's the player triggering this function
        {
            // checks the currentroom and saves what room has been visited
            if (currentRoomName.Contains("LB"))
                GameInfo.VisitRoom(0);
            else if (currentRoomName.Contains("RB"))
                GameInfo.VisitRoom(1);
            else if (currentRoomName.Contains("LT"))
                GameInfo.VisitRoom(2);
            else if (currentRoomName.Contains("RT"))
                GameInfo.VisitRoom(3);
            else GameInfo.VisitRoom(-1);
            
            GameInfo.isRoomCleared(); // if room is cleared, flag will be set
            if(currentRoomName.Contains("Floor_") && roomToLoad.Contains("Floor_")) { // when moving from floor to floor, flags have to be reset
                GameInfo.ResetRoom(); 
            }

            // game is saved and next room is loaded when entering/exiting room
            SaveLoadSystem.SaveHealth();
            GameObject gameObject = GameObject.Find("LevelLoader");
            LevelLoader levelloader = (LevelLoader)gameObject.GetComponent(typeof(LevelLoader));
            levelloader.LoadNextLevel(roomToLoad);
            GameInfo.Save();
        }
    }

    // checks if the room has been cleared before
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        int position = roomPosition(scene.name);
        if (GameInfo.clearedRoom[position])
        {
            // destroy the enemies if room has been cleared before
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i].gameObject);
            }
        }
    }

    // gives the scene manager an additional action to do when done with loading new scene
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // translates the room to an index that can be worked with
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
