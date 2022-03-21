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
