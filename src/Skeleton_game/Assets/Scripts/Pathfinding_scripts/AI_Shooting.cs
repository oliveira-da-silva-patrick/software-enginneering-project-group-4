
/**
*   Script Description: This script has the purpose of controlling the intelligent enemies' shooting.
*
*   This script is supposed to be attached to an Enemy in Unity. There are 2 serialized fields that need to be filled.
*
*       Note: For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
*   
*       * Start Time Btw Shots: Interval of seconds between shots. - (Recommended: Fast: 0.3 , Slow: 2)
*
*       * Projectile: Drag to this field the GameObject you want this enemy to shoot. (Also make sure that the GameObject dragged here has been setup with the projectile script)
*
*   Once this is done the enemy should now shoot the player.
*
*   Author: Daniel Sousa
*
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shooting : MonoBehaviour
{
    private Transform player;

    private float timeBtwShots;
    public float startTimeBtwShots; // Defined in Unity
    public GameObject projectile;

    private AI_FieldOfView attack;


    void Start() {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        attack = GetComponent<AI_FieldOfView>();

    }

    
    void Update()
    {
        
        if (timeBtwShots <= 0 && attack.CanAttack)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } else
        {
            timeBtwShots -= Time.deltaTime; 
        }
    }
}
