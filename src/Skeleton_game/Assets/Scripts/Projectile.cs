
/**
    Script Description

    This script is supposed to be attached to a projectile Game Object in Unity. There is a serialized field that needs to be filled.
    
        * Speed: Set this to whatever value suits your needs.

    Once this is done the projectile should now be shot in the player's direction.

    Note: The projectile is set to travel 10 units of distance and the self-destruct. You can change this by altering the 'maxDistance'
          variable.

    For more info on how to create a projectile please read the 'Creator's Guide'
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    private Vector3 normDir;
    private float maxDistance = 10f;
    private Vector2 initialPosition;

    private void Start() {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        normDir = (player.position - transform.position).normalized;
        initialPosition = transform.position;

    }

    private void Update() {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position += normDir * speed * Time.deltaTime;


        if (Vector2.Distance(initialPosition,transform.position) >= maxDistance)
        {
            DestroyProjectile();
        }
    }

    /** SELF DESTRUCTION

        Is the projectile going through walls and/or through the player?

        Make sure the walls and player are correctly TAGGED with 'Boundary' and 'Player' respectively.

        Is the projectile NOT self-destructing on collision?

        Make sure the GameObject has the option "Is Trigger" activated.

    **/

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") || other.CompareTag("Boundary"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }
}
