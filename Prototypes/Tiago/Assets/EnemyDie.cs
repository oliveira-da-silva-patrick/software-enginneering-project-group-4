using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public int health = 100;
    public GameObject wall;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(gameObject.name == "Enemy")
        {
            Destroy(wall);
        }
        Destroy(gameObject);
    }
}
