using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_behaviour : MonoBehaviour
{
    public float radius = 100f;
    public float stoppingDistance = 3f;
    public float retreatDistance = 2f;
    public float speed = 3f;
    [Range (1,360)] public float angle = 360f;

    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    private GameObject playerRef;

    public bool CanSeePlayer { get; private set;} // If true then follow player

    public bool CanAttack { get; private set;} //Used to turn shooting on/off, can be checked by one of the attacking scripts (Ex: Shooting)




    private GameObject player;
    private float distanceFromPlayerX;
    private float distanceFromPlayerY;

    public Animator animator;

    public GameObject projectile;
    private float timeBtwShots;
    private float startTimeBtwShots = 0.5f;

    private int rd = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void chooseAttack() {

        switch (rd)
        {
            case 1:
                // Attack 1
                break;
            case 2:
                // Attack 2
                break;
            case 3:
                // Attack 3
                break;
            case 4:
                // Attack 4
                break;
            default:
                break;
        }
    }




    bool checkDeath() {
        if (gameObject.GetComponent<healthSystem>().health <= 100)
        {
            return true;
        }else return false;
    }

    void startBoss() {
        animator.SetBool("startFight", true);
    }



    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isDead", checkDeath());

        if (animator.GetBool("startFight"))
        {
            FOV();
        }
        
        distanceFromPlayerX = player.transform.position.x - transform.position.x;
        distanceFromPlayerY = player.transform.position.y - transform.position.y;
        animator.SetFloat("Horizontal", distanceFromPlayerX);
        animator.SetFloat("Vertical", distanceFromPlayerY);
        

        if (Input.GetKeyDown(KeyCode.Z))
        {
            spamProjectiles();
        }
        spamProjectiles();
    }

    void spamProjectiles() {

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } else
        {
            timeBtwShots -= Time.deltaTime; 
        }

        // for (int i = 0; i < 10; i++)
        // {

        //     Instantiate(projectile, transform.position, Quaternion.identity);
        // }
    }












    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        CanAttack = false;

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform; //Gets first entity that walks in range
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                
                    float lastPos = transform.position.x;
                    CanSeePlayer = true;
                    CanAttack = true;
                    if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
                    {
                        animator.SetBool("Moving", true);
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                    else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
                    {
                        animator.SetBool("Moving", true);
                        transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
                        
                    }else
                    {
                        animator.SetBool("Moving", false);
                    }

                }
                else
                {
                    CanSeePlayer = false;
                    animator.SetBool("Moving", false);
                }
            }
            else
                 CanSeePlayer = false;
        }
        else if (CanSeePlayer)
             CanSeePlayer = false;
    }

}
