using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_behaviour : MonoBehaviour
{

    // Parameters for FOV (defines whether boss can see and attack player using CircleCast)
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



    // Calculates distance from player (used in animations)
    private GameObject player;
    private float distanceFromPlayerX;
    private float distanceFromPlayerY;

    //Animator of the Final Boss (could also use method GetComponent but it's irrelevant since this is used in a prefab that doesnt require any setup)
    public Animator animator;

    //Projectile to be thrown by the final boss
    public GameObject projectile;

    // ATTACKS
    private int rd = 2; //If more attacks are implemented either increase this manually or through some trigger such as health.
    private float timeBtwShots;

    // Attack 1
    private float startTimeBtwShotsA1 = 0.5f;
    private bool attack1 = false;
    private int a1Counter = 0;

    //Attack 2
    private float startTimeBtwShotsA2 = 0.2f;
    private bool attack2 = false;
    private int a2Counter = 0;
    
    // Attack caller
    private float attackloop;
    private int startAttackloop = 5;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void chooseAttack(int type) {

        switch (type)
        {
            case 1:
                attack1 = true;
                a1Counter = 0;
                break;
            case 2:
                attack2 = true;
                a2Counter = 0;
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



    // Verifies whether Final Boss is dead or not. Currently Boss is considered dead at 150 health to avoid being destroyed by methods in 'healthSystem'
    // script component before the Death animation can actually play.
    bool checkDeath() {
        if (gameObject.GetComponent<healthSystem>().health <= 100)
        {
            return true;
        }else return false;
    }

    // This method starts the Boss. It is called in an animation event, namely in the 'Boss_Awaken' animation.
    void startBoss() {
        animator.SetBool("startFight", true);
    }

    // Each 
    void attackCallerTimer() {

        if (attackloop <= 0)
        {
            var x = Random.Range(1,rd+1);
            chooseAttack(x);
            attackloop = startAttackloop;
            Debug.Log("Attack " + x + " activated.");

        } else
        {
            attackloop -= Time.deltaTime; 
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Tells Animator if Boss is dead.
        animator.SetBool("isDead", checkDeath());

        if (animator.GetBool("startFight"))
        {
            // Movement
            FOV();

            // Attacks

            //This method activates 1 of the attacks randomly
            attackCallerTimer();

            //Checks which attacks are activated and calls them
            //Attack 1
            if (attack1 && a1Counter <= 8)
            {
                spamProjectiles();
            } else {
                attack1 = false;
            }

            // Attack 2
            if (attack2 && a2Counter <= 5)
            {
                spamFourDirections();
            } else {
                attack2 = false;
            }
        }
        
        // Sends horizontal and vertical values of the Boss relative to the player, this is used to determine which animation should play
        distanceFromPlayerX = player.transform.position.x - transform.position.x;
        distanceFromPlayerY = player.transform.position.y - transform.position.y;
        animator.SetFloat("Horizontal", distanceFromPlayerX);
        animator.SetFloat("Vertical", distanceFromPlayerY);
        
    }

    // ATTACK METHODS

    void spamProjectiles() {

        if (timeBtwShots <= 0)
        {
            //Instantiates projectiles and sends each in random direction and speed

            var x = Random.Range(-2f, 2f); 
            var y = Random.Range(-2f, 2f);
            
            GameObject ProjectileObject = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);

            ProjectileObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x*10,y*10);

            timeBtwShots = startTimeBtwShotsA1;
            a1Counter++;
        } else
        {
            timeBtwShots -= Time.deltaTime; 
        }
    }

    void spamFourDirections() {
        if (timeBtwShots <= 0)
        {

            //Instantiates and lauches projectiles in 4 directions in a spiral pattern
            
            GameObject ProjectileObject1 = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            GameObject ProjectileObject2 = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            GameObject ProjectileObject3 = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            GameObject ProjectileObject4 = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);

            ProjectileObject1.GetComponent<Rigidbody2D>().velocity = new Vector2(5,0 + a2Counter);
            ProjectileObject2.GetComponent<Rigidbody2D>().velocity = new Vector2(-5,0 - a2Counter);
            ProjectileObject3.GetComponent<Rigidbody2D>().velocity = new Vector2(0 - a2Counter,5);
            ProjectileObject4.GetComponent<Rigidbody2D>().velocity = new Vector2(0 + a2Counter,-5);

            timeBtwShots = startTimeBtwShotsA2;
            a2Counter++;
        } else
        {
            timeBtwShots -= Time.deltaTime; 
        }
    }

    // FOV METHOD - Player detection mechanism

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
