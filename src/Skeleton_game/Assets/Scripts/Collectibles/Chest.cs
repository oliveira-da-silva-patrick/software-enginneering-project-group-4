
/**
    Script Description

    This script is supposed to be attached to a Coin in Unity. There are 7 serialized fields that need to be filled.

        Note: For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
    
        * Open Radius: Minimum distance at which the player should be to open the chest.
        
        * Sprite Renderer: Initial sprite of chest. (Initial state)

        * New Sprite: Sprite of opened chest.

           -- NOTE: Instead of assigning sprites to vars manually, have the method find the sprite by name and auto assign it.
                    This may cause problems if sprite is rellocated.

        * Coin: In here you have to put a coin prefab. All instantiated coins will be a copy of this prefab.

        * Diamond: In here you have to put a diamond prefab. All instantiated diamonds will be a copy of this prefab.

        * Number of coins: This defines the number of coins that will pop out of the chest.

        * Number of diamonds: This defines the number of diamonds that will pop out of the chest.

    Once this is done the chest should now be interactable within a defined range .
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

    private Transform player;

    private bool isOpen = false;
    public float openRadius = 3; //Player has to be within radius to open chest

    // Image change: closed/open
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    //Sound
    public AudioManager audioManager;

    // Loot
    public GameObject coin;
    public GameObject diamond;
    [Range (1,30)]public int numberOfCoins;
    [Range (1,50)]public int numberOfDiamonds;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Called by clicking on chest; Changes chest sprite Closed/Open
    public void ChangeSprite()
    {
        isOpen = true;
        spriteRenderer.sprite = newSprite; 
    }

    //Called by clicking on chest
    public void DropLoot()
    {
        //Spawns coins
        for (int i = 0; i < numberOfCoins; i++)
        {
        //Defines explosion radius for the collectibles in the chest
        var x = Random.Range(-2f, 2f); 
        var y = Random.Range(-2f, 2f);
        //Creates Coin game object in scene
        GameObject CoinObject = (GameObject)Instantiate(coin, transform.position, Quaternion.identity);

        //Finds and assigns "AudioManager" found in hierarchy to the 'audioManager' var of the coin game object
        coin.GetComponent<Coin>().audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //Adds velocity to each instantiated coin in a random direction to create explosion effect
        CoinObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x*10,y*10);
        
        }
        //Spawns Diamonds
        for (int i = 0; i < numberOfDiamonds; i++)
        {
           
            var x = Random.Range(-2f, 2f); 
            var y = Random.Range(-2f, 2f);
            
            GameObject DiamondObject = (GameObject)Instantiate(diamond, transform.position, Quaternion.identity);

            diamond.GetComponent<Diamond>().audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

            DiamondObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x*10,y*10);
        }
    }

    //When player clicks on chest
    private void OnMouseDown() {
        if (!isOpen && Vector2.Distance(player.position,transform.position) <= openRadius)
        {
            audioManager.playChestSound();
            ChangeSprite(); //Makes chest open
            DropLoot();
            
        }
    }
}
