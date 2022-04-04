using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void AccessSkills()
    {
        SceneManager.LoadScene("SkillTree");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
