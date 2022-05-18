using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessary for switching scenes


public class UIElements : MonoBehaviour
{

    // the pause menu canvas
    [SerializeField] GameObject pauseMenu;

    public void startNewGame()
    {
        GameObject go = GameObject.Find("LevelLoader");
        LevelLoader levelloader = (LevelLoader)go.GetComponent(typeof(LevelLoader));
        levelloader.LoadNextLevel("Floor_1");
        SaveLoadSystem.deleteSaveFile();
        GameInfo.Save();
    }

    public void continueGame()
    {
        GameInfo.Load();
        SceneManager.LoadScene(GameInfo.currentSceneID);
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
        if (Time.timeScale != 0)
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

}
