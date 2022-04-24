using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessary for switching scenes


public class UIElements : MonoBehaviour
{

    // the pause menu canvas
    [SerializeField] GameObject pauseMenu;

    // loads the game
    public void goToGame()
    {
        SceneManager.LoadScene("Floor1");
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
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    // resumes everything and closes the pause menu
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

}
