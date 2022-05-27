using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{

    [SerializeField] GameObject vmMenu; // vending machine menu canvas

    private Transform player;
    public float openRadius = 3; //Player has to be within radius to interact with vending machine

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //When player clicks on vending machine
    private void OnMouseDown()
    {
        // checks if player is in range and if game is not already paused
        if (Vector2.Distance(player.position, transform.position) <= openRadius && Time.timeScale != 0)
        {
            vmMenu.SetActive(true); // activates the vending machine canvas
            Time.timeScale = 0f; // pauses the game
        }
    }

    // resume game and exit vm menu
    public void Resume()
    {
        vmMenu.SetActive(false);
        Time.timeScale = 1f; // resumes the game
    }
}
