
/**
*   Script Description: This script has the purpose of controlling the audios in the game.
*						Any game script can call this audio manager and play a certain sound (as long as the prefab this is attached to is placed on the scene)
*
*   This script is supposed to be attached to an Audio Manager in Unity (Game object with an Audio Source).
*   
*	For each sound you can add a component 'Audio Source' and assign a sound file to the 'Audio Clip' of that component.
*	Then drag the 'Audio Source' component into the respective slot in the script vars.
*
*   Once this is done the coin should now be collectible and be attracted to player when in range.
*
*	Author: Daniel Sousa
*
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    // Audio players components.
	public AudioSource Coin_SoundFX;
	public AudioSource Diamond_SoundFX;
	public AudioSource Chest_SoundFX;

	// Play a single clip through the sound effects source.
	public void playCoinSound()
	{
		//gameObject.GetComponent<AudioSource>().Play();
		Coin_SoundFX.Play();
	}

	public void playDiamondSound()
	{
		Diamond_SoundFX.Play();
	}

	public void playChestSound()
	{
		Chest_SoundFX.Play();
	}
}