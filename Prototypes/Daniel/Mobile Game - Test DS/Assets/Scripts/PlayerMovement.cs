
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    public Joystick joystick;

    private void Awake()
    {
        //Get references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Moves player, speed in editor might be set to 0 by default, change it to make this work.
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (horizontalInput >= .05f)
        {
            horizontalInput = 1f;
        }else if (horizontalInput <= -.05f)
        {
            horizontalInput = -1f;
        }else
        {
            horizontalInput = 0f;
        }

        if (verticalInput <= -.05f)
        {
            verticalInput = -1f;
        }else if (verticalInput >= .05f)
        {
            verticalInput = 1f;
        }else
        {
            verticalInput = 0f;
        }

        body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        // Flips player when pressing right/left arrow keys
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        // Set animator parameters
        // Animation's name and condition 
        anim.SetBool("run", horizontalInput != 0 || verticalInput != 0); 
    }


    
}
