using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{

    [SerializeField] GameObject vmMenu;

    private Transform player;
    public float openRadius = 3; //Player has to be within radius to open chest

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //When player clicks on chest
    private void OnMouseDown()
    {
        if (Vector2.Distance(player.position, transform.position) <= openRadius)
        {
            vmMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        vmMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
