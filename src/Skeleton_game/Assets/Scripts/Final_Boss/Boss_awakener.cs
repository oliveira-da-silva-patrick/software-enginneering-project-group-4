using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_awakener : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<Animator>().SetBool("Awaken",true);
        }
    }

    
}