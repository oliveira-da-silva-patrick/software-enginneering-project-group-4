using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float speed = 2f;
    

    void Start(){
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
