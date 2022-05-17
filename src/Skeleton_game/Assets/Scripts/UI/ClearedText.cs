using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            clearedText.SetActive(true);
        } else
        {
            clearedText.SetActive(false);
        }
    }
}
