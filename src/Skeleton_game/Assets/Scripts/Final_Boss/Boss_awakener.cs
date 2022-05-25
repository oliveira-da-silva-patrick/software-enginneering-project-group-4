/**
*
*   Script Description: This script is used by a detector (Circle Collider 2D) attached to the Final Boss prefab.
*                       The purpose of this is to change the state of the Final Boss once the Player enters its radius.
*                       Once this happens the Final Boss is then fully active and the fight begins.
*
*   Author: Daniel Sousa
*
**/
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