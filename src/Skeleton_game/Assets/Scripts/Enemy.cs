
/**
    Script Description

    This script is supposed to be attached to an Enemy in Unity. There are 6 serialized fields that need to be filled.

        Note: For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
    
        * Speed: Set this to whatever value suits your needs. - (Recommended: Lower than player's)

        * Follow Distance: If the player is within this distance enemy will follow it. - (Recommended: Depends on Game Objects sizes)

        * Stopping Distance: Once the enemy gets this close to the player, it will stop following. - (Recommended: )

        * Retreat Distance: If the player gets within this radius the enemy will run away from player. - (Recommended: )

        * Start Time Btw Shots: Interval of seconds between shots. - (Recommended: )

        * Projectile: Drag to this field the GameObject you want this enemy to shoot. (Also make sure that the GameObject dragged here has been setup with the projectile script)

    Once this is done the camera should now follow the player's position.
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float followDistance; //Once the player enter this area the enemy will start following player
    public float stoppingDistance; //Distance at which the enemy will stop approaching the player
    public float retreatDistance; //Once player enters this area the enemy will go away from player
    private Transform player; //Used several times to determine player location
    private Rigidbody2D enemy; //Used within 'Start()' to set mass of enemy to a predetermined value

    private Vector3 enemySize;

    //====================== [ SHOOTING ] ==============================

    private float timeBtwShots;
    public float startTimeBtwShots; // Defined in Unity
    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        //Self-explanatory, finds player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Rigidbody2D>();
        enemy.mass = 999999; //This solves problem where enemy will just go away from player
        enemySize = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z); // Saves player scale (used in flipping)

        //===========================

        timeBtwShots = startTimeBtwShots;
    }


    // Update is called once per frame
    private void Update()
    {
        //Saves enemy last position (used to flip enemy)
        float lastPosition = transform.position.x;


        //Series of conditions to determine enemy movement [Towards/Stationary/Away] respectively
        if (Vector2.Distance(transform.position, player.position) < followDistance &&  Vector2.Distance(transform.position, player.position) >= stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        //Flips enemy when player goes to the enemy's left/right side
        float flip = transform.position.x - lastPosition;

        if (flip > 0f)
            transform.localScale = Vector3.Scale(new Vector3(1,1,1) , enemySize);
        else if (flip < 0f)
            transform.localScale = Vector3.Scale(new Vector3(-1,1,1) , enemySize);


        //====================== [ SHOOTING ] ==============================

        if (timeBtwShots <= 0 && Vector2.Distance(transform.position, player.position) < followDistance)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } else
        {
            timeBtwShots -= Time.deltaTime; 
        }
    }
}
