using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 2.0f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Transform closestEnemy;
    public bool enemyContact = false;
    private GameObject[] multipleEnemies;
   

    void Update()
    {
        movement.x = joystick.Horizontal * speed;
        movement.y = joystick.Vertical * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        closestEnemy = getClosestEnemy();
        if(closestEnemy != null)
        {
            //closestEnemy.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, 0.7f, 0, 1);
        }
    }

    public Transform getClosestEnemy()
    {
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;
        foreach(GameObject go in multipleEnemies)
        {
            float currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance && currentDistance < 5)
            {
                closestDistance = currentDistance;
                trans = go.transform;
                enemyContact = true;
                go.GetComponent<SpriteRenderer>().material.color = new Color(1, 0.7f, 0, 1);
            } else
            {
                go.GetComponent<SpriteRenderer>().material.color = new Color(0, 0.2f, 1, 1);
            }
        }
        return trans;
    }
}