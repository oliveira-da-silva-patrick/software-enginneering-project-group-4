using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
