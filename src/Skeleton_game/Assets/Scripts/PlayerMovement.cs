
/**
    Script Description

    This script is supposed to be attached to the Main player in Unity. There are 2 serialized fields that need to be filled.
    
        * Speed: Set this to whatever value suits your needs.

        * Joystick: Drag your joystick inside this field. If you dont have a joystick yet please follow the instructions on the 'Creator's Guide'.

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
    public float rotateSpeed;

    public Joystick joystick;
    public Rigidbody2D rb;
    private float distance;
    Vector2 movement;
    public BoxCollider2D colliderEnemy;
    public Transform firePoint;
    public GameObject attackPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0F;


    public Transform target;


    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        movement.Normalize();
        
        if(colliderEnemy != null)
        {
            distance = rb.Distance(colliderEnemy).distance;
                if(distance <= 3f && movement == Vector2.zero)
            {
                Vector3 relPos = target.position - transform.position;
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
}
