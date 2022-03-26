
/**
    Script Description

    This script is supposed to be attached to the main camera in Unity. There is a serialized field that needs to be filled.
    You need to drag the main player into this field.

    Once this is done the camera should now follow the player's position.
**/

//----------------------------------------------------------

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    
    private void Update() {
    
        //Camera follows player by updating its own position to the same position the player is every frame
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
    
    
}
