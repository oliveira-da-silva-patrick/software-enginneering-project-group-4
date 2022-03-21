using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    public float characterVelocity;
    public float rotateSpeed;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    public BoxCollider2D colliderEnemy;
    private float distance;

    public Transform target;
 
 
    void Start(){
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }
    
    void calcuateNewMovementVector(){
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }
 
    void Update(){
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime){
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        distance = rb.Distance(colliderEnemy).distance;
        if(distance <= 3f)
        {
            Vector3 relPos = target.position - transform.position;
            Vector3  rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * relPos;
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
        else if(movementDirection != Vector2.zero)
        {
            Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation2, rotateSpeed * Time.deltaTime);
        }
        
        //move enemy: 
        rb.MovePosition(new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y + (movementPerSecond.y * Time.deltaTime)));
    
    }
}
