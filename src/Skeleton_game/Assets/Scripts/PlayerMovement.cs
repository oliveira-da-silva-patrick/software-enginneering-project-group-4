
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


            /**     ANIMATIONS      **/

        // Set animator parameters

        // Animation's name and condition 
        anim.SetBool("run", horizontalInput != 0 || verticalInput != 0); 
    }


    
}
