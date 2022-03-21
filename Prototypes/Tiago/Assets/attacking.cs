using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacking : MonoBehaviour
{
    public Transform firePoint;
    public GameObject attackPrefab;
    private float distance;
    public Rigidbody2D rb;
    public BoxCollider2D colliderEnemy;
    public float fireRate = 0.5f;
    private float nextFire = 0.0F;

    // Update is called once per frame
    void Update()
    {
        if(colliderEnemy != null)
        {
            distance = rb.Distance(colliderEnemy).distance;
            if(distance <= 3f)
            {
                Shoot();
            }
        }
    }

    void Shoot ()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
