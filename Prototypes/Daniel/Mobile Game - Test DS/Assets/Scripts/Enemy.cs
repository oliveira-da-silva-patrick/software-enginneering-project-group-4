using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float followDistance; //Once the player enter this area the enemy will start following player
    public float stoppingDistance; //Distance at which the enemy will will stop approaching the player
    public float retreatDistance; //Once player enters this area the enemy will go away from player
    private Transform player; //Used several times to determine player location
    private Rigidbody2D enemy; //Used within 'Start()' to set mass of enemy to a predetermined value

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
            transform.localScale = new Vector3(4,4,1);
        else if (flip < 0f)
            transform.localScale = new Vector3(-4,4,1);


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
