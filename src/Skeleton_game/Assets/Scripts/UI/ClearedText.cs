using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearedText : MonoBehaviour
{
    // the pause menu canvas
    [SerializeField] GameObject clearedText;

    // Update is called once per frame
    void Update()
    {
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log(count);
        if (count == 0 && !clearedText.active) // checks if enemies have been defeated and text not already active
        {
            if (SceneManager.GetActiveScene().name.Contains("Floor_6")) // if last floor -> load credits
            {
                GameObject gameObject = GameObject.Find("LevelLoader");
                LevelLoader levelloader = (LevelLoader)gameObject.GetComponent(typeof(LevelLoader));
                levelloader.LoadNextLevel("WinScene");
            }
            GameInfo.isRoomCleared(); // changes the flag of the room to true
            GameInfo.Save(); // saves the game
            SaveLoadSystem.SaveHealth(); // saves the health
            clearedText.SetActive(true); // activates cleared message
        } else if(count != 0)
        {
            clearedText.SetActive(false); // should normally never happen, as it is disabled by default
        }
    }
}
