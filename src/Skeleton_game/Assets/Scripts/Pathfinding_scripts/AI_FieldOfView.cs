
/**
    Script Description

    This script is supposed to be attached to an AI_Enemy in Unity. There are 3 serialized fields that need to be filled.

        Note: For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
    
        * Radius: Set this to whatever value suits your needs.

        * Target Layer: Set to "Player".

        * Obstacle Layer: Set to "Obstacle".

    Once this is done this entity should now be able to attack when in range (and also flip).
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FieldOfView : MonoBehaviour
{

    public float radius = 7f;
    private float angle = 360f;

    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    private GameObject playerRef;


    public bool CanSeePlayer { get; private set;} // If true then follow player

    public bool CanAttack { get; private set;} //Used to turn shooting on/off, can be checked by one of the attacking scripts (Ex: Shooting)

    public Animator animator;
    
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animator.SetBool("Moving", true);

    }


    private void FOV()
    {

        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        CanAttack = false;

        if (Vector2.Distance(transform.position, playerRef.transform.position) < 3.2f)
        {
            animator.SetBool("Moving", false);
        } else
        {
            animator.SetFloat("Horizontal", (playerRef.transform.position.x - transform.position.x));
            animator.SetBool("Moving", true);;
        }

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform; //Gets first entity that walks in range
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    CanSeePlayer = true;
                    CanAttack = true;

                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else
                 CanSeePlayer = false;
        }
        else if (CanSeePlayer)
             CanSeePlayer = false;
    }

    void Update() {
        FOV();
    }



}
