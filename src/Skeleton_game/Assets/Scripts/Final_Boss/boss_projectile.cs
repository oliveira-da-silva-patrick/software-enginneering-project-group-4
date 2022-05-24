using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_projectile : MonoBehaviour
{
    
    // Projectile properties, changeable in Unity
    public int damage = 15;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // When proojectile collides with an object, it tries to deal damage to it and gets destroyed
    private void OnTriggerEnter2D(Collider2D other) {
        healthSystem opponent = other.GetComponent<healthSystem>();
        if (other.CompareTag("Player") || other.CompareTag("Boundary"))
        {
            if(opponent != null)
            {
                opponent.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }


}