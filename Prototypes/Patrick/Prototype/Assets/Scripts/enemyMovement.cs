using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public Transform player;
    private bool isPlayerClose = false;
    private bool isTooClose = false;

    private Vector2 movedelta;
    private float movespeed = 1f;

    private float dx;
    private float dy;

    private const int waitTime = 30;
    private int waitFrames = waitTime;
    private bool wait = false;


    // Update is called once per frame
    void Update()
    {
        waitFrames--;
        updateMovedelta();
        wait = true;
        if (waitFrames <= 0)
        {
            wait = false;
            waitFrames = waitTime;
        }
    }

    void FixedUpdate()
    {
        if (isPlayerClose && !isTooClose || !wait && !isPlayerClose)
        {
            transform.Translate(movedelta * movespeed * Time.deltaTime);
        }
    }

    void updateMovedelta()
    {
        if (Vector2.Distance(transform.position, player.position) < 7)
        {
            isPlayerClose = true;
        }
        else
        {
            isPlayerClose = false;
        }

        if (Vector2.Distance(transform.position, player.position) < 4)
        {
            isTooClose = true;
        }
        else
        {
            isTooClose = false;
        }

        if (isPlayerClose)
        {
            wait = false;
            movedelta = player.position - transform.position;
        }
        else
        {
            wait = false;
            dx = Random.Range(-6f, 6f);
            dy = Random.Range(-6f, 6f);
            movedelta = new Vector2(dx, dy);
        }
    }
}
