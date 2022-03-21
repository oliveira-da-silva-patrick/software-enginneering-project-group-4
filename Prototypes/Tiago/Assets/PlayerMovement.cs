using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;

    public Joystick joystick;
    public Rigidbody2D rb;
    private float distance;
    Vector2 movement;
    public BoxCollider2D colliderEnemy;


    public Transform target;


    // Update is called once per frame
    void Update()
    {
        /*if(joystick.Horizontal >= 0.2f)
        {
            movement.x = 1f;
        }else if(joystick.Horizontal <= -0.2f)
        {
            movement.x = -1f;
        }
        else
        {
            movement.x = 0f;
        }
        if(joystick.Vertical >= 0.2f)
        {
            movement.y = 1f;
        }else if(joystick.Vertical <= -0.2f)
        {
            movement.y = -1f;
        }
        else
        {
            movement.y = 0f;
        }*/
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        movement.Normalize();
        
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
        }

        /*if(rb.Distance(colliderEnemy) == 5f)
        {
            Instantiate(Shooting, LaunchOffset.position, transform.rotation);
        }*/
        
        if(colliderEnemy != null)
        {
            distance = rb.Distance(colliderEnemy).distance;
                if(distance <= 3f)
            {
                Vector3 relPos = target.position - transform.position;
                Vector3  rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * relPos;
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
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
        rb.MovePosition(new Vector2(transform.position.x + (movement.x * moveSpeed * Time.deltaTime), transform.position.y + (movement.y * moveSpeed * Time.deltaTime)));
    }




}
