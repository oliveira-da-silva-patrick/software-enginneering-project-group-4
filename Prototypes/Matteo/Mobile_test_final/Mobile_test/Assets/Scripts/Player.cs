using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 1.0f;
    private Rigidbody2D rb;
    Vector2 movement;

    bool movingUp;
    bool movingDown;
    bool movingSide;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = joystick.Horizontal * speed;
        movement.y = joystick.Vertical * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}