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

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        healthSystem opponent = hitInfo.GetComponent<healthSystem>();
        if(opponent != null)
        {
            opponent.TakeDamage(damage);
        }
    }

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
