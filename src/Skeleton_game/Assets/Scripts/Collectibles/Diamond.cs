
/**
    Script Description

    This script is supposed to be attached to a Diamond in Unity. There is 1 serialized field that needs to be filled.

        Note: For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
    
        * Audio Manager: In the scene's hierarchy you will find a game object 'AudioManager'. Assign it to this variable.
                        This  takes cares of the diamond soundFX.

    Once this is done the diamond should now be collectible and be attracted to player when in range.

    NOTE: The 'AudioManager' on the hierarchy cannot be assigned to the prefab 'Diamond', therefore the instanciated diamonds
            in the chest script are assigned the audiomanager during runtime.

    ERRORS: 
        
        * If the diamond is no longer collectible, verify the player tag and make sure the AudioManager in the scene is correctly named.
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System;

public class Diamond : MonoBehaviour
{

    //Sound
    public AudioManager audioManager;

    Rigidbody2D rb;
    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 5f; //Diamond speed chasing the player; IDEA: Get player speed + 2 or player speed * 1.5

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
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
        //ADD TO MONEY COUNT

        //Debug.Log("Diamond Collected");
        Destroy(gameObject);
    }

    //Make sure 'Is Trigger' is selected on the collectible
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            //Prevents ocasional bug where 'Chest' script will fail to attribute an audioManager to the instantiated coin.
            if(audioManager == null)
            {
                Destroy(gameObject);
            }
            audioManager.playDiamondSound();
            Collect();
        }
    }
}
