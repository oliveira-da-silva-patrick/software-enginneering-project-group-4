
/**
    Script Description

    This script is supposed to be attached to an Enemy in Unity. There are 7 serialized fields that need to be filled.

        Note: For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
    
        * Radius: Set this to whatever value suits your needs. - (Recommended: Higher than the 'StoppingDistance')

        * Stopping Distance: Once the enemy gets this close to the player, it will stop following. - (Recommended: Lower than 'Radius')

        * Retreat Distance: If the player gets within this radius the enemy will run away from player. - (Recommended: Lower than the 'StoppingDistance')

        * Speed: Set this to whatever value suits your needs. - (Recommended: Lower than player's)

        * Angle: Between 0 and 360, the higher the the value the wider the entity's FOV (Field of View).

        * Target Layer: Set to "Player".

        * Obstacle Layer: Set to "Obstacle".

    Once this is done this entity should now follow the player's position.
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float radius = 7f;
    public float stoppingDistance = 3f;
    public float retreatDistance = 2f;
    public float speed = 3f;
    [Range (1,360)] public float angle = 360f;

    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    private GameObject playerRef;

    public bool CanSeePlayer { get; private set;} // If true then follow player

    public bool CanAttack { get; private set;} //Used to turn shooting on/off, can be checked by one of the attacking scripts (Ex: Shooting)

    public Animator animator;

    
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

    }


    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        CanAttack = false;

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform; //Gets first entity that walks in range
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                
                    float lastPos = transform.position.x;
                    CanSeePlayer = true;
                    CanAttack = true;
                    if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                        if (Mathf.Abs((transform.position.x - target.position.x)) > 0)
                        {
                            animator.SetBool("Moving", true);
                            animator.SetFloat("Horizontal", (target.position.x - transform.position.x));
                        }else{
                            animator.SetBool("Moving", false);
                        }
                    }
                    else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
                        if (Mathf.Abs((transform.position.x - target.position.x)) > 0)
                        {
                            animator.SetBool("Moving", true);
                            animator.SetFloat("Horizontal", (transform.position.x - target.position.x));
                        }else{
                            animator.SetBool("Moving", false);
                        }
                        
                    }else if (Vector2.Distance(transform.position, target.position) <= stoppingDistance && Vector2.Distance(transform.position, target.position) >= retreatDistance)
                    {
                        animator.SetBool("Moving", false);
                    }

                }
                else
                {
                    CanSeePlayer = false;
                    animator.SetBool("Moving", false);
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
