using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class is responsible for the interactions with npcs
public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dMenu;
    public string text; // the npc's answer

    private Transform player;
    public Text dText; // the text field containing the text
    public float openRadius = 3; // the interaction radius

    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player").transform; // instantiate the player
    }

    private void OnMouseDown()
    {
        if (Vector2.Distance(player.position, transform.position) <= openRadius && Time.timeScale != 0) // check if player is in radius and if game is not already paused
        {            
            dMenu.SetActive(true); // activates the dialogue menu
            dText.text = text; // changes to text to fit the npc
            Time.timeScale = 0f; // pause the game
        }
    }

    // resumes the game
    public void Resume()
    {
        dMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
