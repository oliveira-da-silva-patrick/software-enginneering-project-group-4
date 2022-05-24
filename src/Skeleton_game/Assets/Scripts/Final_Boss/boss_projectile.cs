using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_projectile : MonoBehaviour
{
    
    public int damage = 15;
    public float speed = 5;
    private Vector3 target;
    private Vector3 normDir;
    private float maxDistance = 10f;
    private Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        var x = Random.Range(-20f, 20f); 
        var y = Random.Range(-20f, 20f);

        target = new Vector3(transform.position.x + x, transform.position.y + y, 0);
        normDir = (target - transform.position).normalized;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normDir * speed * Time.deltaTime;
    }

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