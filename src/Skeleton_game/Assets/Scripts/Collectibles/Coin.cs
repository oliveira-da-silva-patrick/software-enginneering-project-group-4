
/**
    Script Description

    This script is supposed to be attached to a Coin in Unity. There is 1 serialized field that needs to be filled.

        Notes: 
        
            * For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
    
            * Make sure there's an audioManager in the hierarchy for the coins to work properly without errors.

    Once this is done the coin should now be collectible and be attracted to player when in range.
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System;

public class Coin : MonoBehaviour
{

    //Sound
    AudioManager audioManager;

    Rigidbody2D rb;
    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 5f; //Coin speed chasing the player; IDEA: Get player speed + 2 or player speed * 1.5

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void FixedUpdate() {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    //Called by 'Magnet' script attached to player to pull in collectibles
    public void setTarget(Vector3 position) {
        targetPosition = position;
        hasTarget = true;
    }

    
    public void Collect()
    {

        //Debug.Log("Coin Collected");
        Destroy(gameObject);
    }

    //Make sure 'Is Trigger' is selected on the collectible
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            //Prevents ocasional bug where'Chest' script will fail to attribute an audioManager to the instantiated coin.
            if(audioManager == null)
            {
                Destroy(gameObject);
            } else
            {
                audioManager.playCoinSound();
            }
            //ADD TO MONEY COUNT
            other.GetComponent<PlayerMoney>().money += 5;
            Collect();
        }
    }
}
