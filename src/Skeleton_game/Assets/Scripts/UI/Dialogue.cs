using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dMenu;
    public string text;

    private Transform player;
    public Text dText;
    public float openRadius = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnMouseDown()
    {
        if (Vector2.Distance(player.position, transform.position) <= openRadius && Time.timeScale != 0)
        {            
            dMenu.SetActive(true);
            dText.text = text;
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        dMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
