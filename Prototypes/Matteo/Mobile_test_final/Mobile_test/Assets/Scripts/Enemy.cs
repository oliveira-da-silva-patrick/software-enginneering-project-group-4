using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5.0f;
    private bool isLocked = false;  

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < 5)
        {
            isLocked = true;
        }
        if (isLocked)
        {
            turn();
        }
    }
    private void FixedUpdate()
    {
        if (isLocked)
        {
            moveCharacter(movement);
        }
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void turn()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        // sets direction value between -1 and 1
        direction.Normalize();
        movement = direction;
    }
}
