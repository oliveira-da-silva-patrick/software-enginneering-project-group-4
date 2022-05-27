using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessary for switching scenes
using System.IO;

// contains most methods allowing to change between scenes and other tools to manage ui elements
public class UIElements : MonoBehaviour
{

    // the pause menu canvas
    [SerializeField] GameObject pauseMenu;

    // start a new run (not to be misinterpreted with resetting ECTS which are kept)
    // saving a new game destroys the old save file
    public void startNewGame()
    {
        // checks if there is overall game progress
        if (GameInfo.allclearedRoomsEver == null)
        {
            GameInfo.fillEmpty();
        }
        GameInfo.newGame();
        SaveLoadSystem.SaveSkillTree();
        GameObject go = GameObject.Find("LevelLoader");
        LevelLoader levelloader = (LevelLoader)go.GetComponent(typeof(LevelLoader));
        levelloader.LoadNextLevel("Floor_0");
        Damage.lostHealth = 0;
        Damage.lostShield = 250;
        SaveLoadSystem.SaveHealth();
        GameInfo.Save();
    }

    // loads the game in the last room the player was
    public void continueGame()
    {
        GameInfo.Load();
        if (GameInfo.currentSceneID >= 2) // currentSceneID < 2 should be impossible.
        {
            SceneManager.LoadScene(GameInfo.currentSceneID);
        }
    }

    // loads the skill tree
    public void goToSkillTree()
    {
        SceneManager.LoadScene("SkillTree");
    }

    // loads the main menu
    public void goToMainMenu()
    {
        Time.timeScale = 1f; // in here for the case that you are returning from the pause menu
        SceneManager.LoadScene("MainMenu");
    }

    // pauses everything and opens the pause menu
    public void PauseGame()
    {
        if (Time.timeScale != 0) // checks if the game is running
        {
            GameInfo.Save();
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // resumes everything and closes the pause menu
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // closes the application
    public void Exit()
    {
        Application.Quit();
    }

}
