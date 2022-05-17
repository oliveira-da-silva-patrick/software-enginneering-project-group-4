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
        if (count == 0)
        {
            if (SceneManager.GetActiveScene().name.Contains("Floor_6"))
            {
                GameObject gameObject = GameObject.Find("LevelLoader");
                LevelLoader levelloader = (LevelLoader)gameObject.GetComponent(typeof(LevelLoader));
                levelloader.LoadNextLevel("WinScene");
            }
            clearedText.SetActive(true);
        } else
        {
            clearedText.SetActive(false);
        }
    }
}
