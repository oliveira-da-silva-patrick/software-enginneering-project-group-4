using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_Player : MonoBehaviour
{
   public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        healthSystem opponent = hitInfo.GetComponent<healthSystem>();
        if(opponent != null)
        {
            opponent.TakeDamage(damage);
        }
        if(hitInfo.name != "Player_shooting(Clone)" && hitInfo.name != "Projectile_ball(Clone)")
        {
            Destroy(gameObject);
        }
    }

    
}
