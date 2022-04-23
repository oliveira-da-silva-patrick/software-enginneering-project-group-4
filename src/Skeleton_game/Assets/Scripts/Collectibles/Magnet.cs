
/**
    Script Description

    This script is supposed to be attached to the MagnetCollector (child game object of Main_Player) in Unity.

    Once this is done the collectibles should now be attracted to player when in range.

    To change the radius of the magnet, either change the radius of the Circle Collider 2D or create a var in this script that does the same.
**/

//----------------------------------------------------------

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

        }else if (collision.gameObject.TryGetComponent<Diamond>(out Diamond diamond))
        {
            //If it is a diamond
            diamond.setTarget(transform.parent.position);
        }

        //Not a coin or diamond
    }
}
