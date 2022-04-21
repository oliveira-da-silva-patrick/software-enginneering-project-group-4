
/**
    Script Description

    This script is supposed to be attached to the Main player in Unity. There are 2 serialized fields that need to be filled.
    
        * Speed: Set this to whatever value suits your needs.

        * rotateSpeed: Set this to whatever value suits your needs.

        * Joystick: Drag your joystick inside this field. If you dont have a joystick yet please follow the instructions on the 'Creator's Guide'.

        * rb: Player's Rigidbody2D.

        * distance: The value of the distance between the closest enemy.

        * movement : Get the input of the joystick and translate it in movement for the player.

        * firePoint: Is the shooting point of the player needs an empty object (which is below the main player called Fire_Point).

        * attackPrefab: Needs the Player_shoot (the projectiles that the player shoots).

        * fireRate: Variable that sets the attack speed of the player.

        * nextFire: Variable which tells the methode that it has to shoot again.

        * enemy: Gets the closest enemy to the player.

    Once this is done the player should now move. (You can also use the 'CameraController' script to make the camera follow your player)

    Animations:

        Within this script you will find an animation section. The script is already setup for a 'Run' animation, but you
    can remove and/or add more animations.

    WARNING!!

    If you dont setup the animator for the player you will get a compilation error or warning BUT dont worry this won't affect your testing.

    For more instructions on how to setup your animations please check out the 'Creator's Guide'.



**/

//----------------------------------------------------------

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float rotateSpeed;

    public Joystick joystick;
    private Rigidbody2D rb;
    private float distance;
    Vector2 movement;
    public Transform firePoint;
    public GameObject attackPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0F;


    private GameObject enemy;



    void Start()
    {
        rotateSpeed = 1500f;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        movement.Normalize();

        enemy = FindClosestEnemy();
        
        if(enemy != null)
        {
            distance = Vector2.Distance (transform.position, enemy.transform.position);
                if(distance <= 3f && movement == Vector2.zero)
            {
                Vector3 relPos = enemy.transform.position - transform.position;
                Vector3  rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * relPos;
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
                if(transform.rotation == toRotation)
                {
                    Shoot();
                }
            }
            else if(movement != Vector2.zero)
            {
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation2, rotateSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(movement != Vector2.zero)
            {
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation2, rotateSpeed * Time.deltaTime);
            }
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x + (movement.x * speed * Time.deltaTime), transform.position.y + (movement.y * speed * Time.deltaTime)));
    }

    void Shoot ()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
