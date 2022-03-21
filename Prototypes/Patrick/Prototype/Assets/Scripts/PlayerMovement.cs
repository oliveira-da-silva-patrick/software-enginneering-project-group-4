using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Joystick joystick;
    private float moveSpeed = 5f;

    private Vector2 movedelta;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = joystick.Horizontal * moveSpeed;
        float verticalMove = joystick.Vertical * moveSpeed;

        movedelta = new Vector2(horizontalMove, verticalMove);
    }

    void FixedUpdate() 
    {
        transform.Translate(movedelta * Time.deltaTime);
    }

}
