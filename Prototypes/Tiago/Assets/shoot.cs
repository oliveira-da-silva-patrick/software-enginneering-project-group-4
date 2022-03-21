using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
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
        Debug.Log(hitInfo.name);
        EnemyDie enemy = hitInfo.GetComponent<EnemyDie>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if(hitInfo.name != "Shoot" && hitInfo.name != "EnemyShoot(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
