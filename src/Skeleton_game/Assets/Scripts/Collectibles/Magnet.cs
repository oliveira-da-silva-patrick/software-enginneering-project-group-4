using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            //If it is a coin
            coin.setTarget(transform.parent.position);
        }

        //Not a coin
    }
}
