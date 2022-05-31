/**
    Script Description

    This script is used to play a loading animation (fade to black) whenever a player switches scenes
    It is attached to an empty game object, which should be placed in every scene
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    public void LoadNextLevel(string nextScene)
    {
        StartCoroutine(LoadLevel(nextScene));
    }

    IEnumerator LoadLevel(string nextScene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nextScene);
    }
}
