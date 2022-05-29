/**
    Script Description

    This script is supposed to be attached to the HealthBar in the HealthBar scene.
    The HealthBar contains the health of the main character.

        * orbitDistance: The value for the distance between the player and the orbiting magnet.

        * rotationSpeed: The value for the speed of the orbiting magnet.

        * pivotObject: The refernce to the main character.

        * damage: The value for the damage of the orbiting magnet.

        * relativeDistance: An reference to the magnet relative position to the main player.      
**/

//----------------------------------------------------------




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet_Behaviour : MonoBehaviour
{
    public float orbitDistance = 3.0f;
    private float rotationSpeed = 100f;
    private GameObject pivotObject;
    public int damage = 15;
    public Vector3 relativeDistance = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        pivotObject = gos[0];
        if(pivotObject.transform != null) 
         {
            relativeDistance = transform.position - pivotObject.transform.position;
         }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(pivotObject != null)
        {
            Orbit();
        }*/
    }
    void LateUpdate () {
         
         Orbit();
     
     }

    //The orbit(magnet) does damage when it collides with an enemy
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        healthSystem opponent = hitInfo.GetComponent<healthSystem>();
        if(opponent != null)
        {
            opponent.TakeDamage(damage);
        }
    }

    //In this Methode the orbit(magnet) can rotate/orbit around the main player
    void Orbit()
     {
         if(pivotObject != null)
         {
            transform.position = pivotObject.transform.position + relativeDistance;
            transform.RotateAround(pivotObject.transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
            // Reset relative position after rotate
            relativeDistance = transform.position - pivotObject.transform.position;
         }
     }
}
