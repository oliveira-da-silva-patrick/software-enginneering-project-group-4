
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    
    private void Update() {
    
        //Camera follows player by updating its own position to the same position the player is every frame
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
    
    
}
