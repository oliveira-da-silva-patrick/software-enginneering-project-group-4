
/**
    Script Description

    This script is supposed to be attached to an Audio Manager in Unity (Game object with an Audio Source). There is 1 serialized field that needs to be filled.

        Note: For this script to work the player should be tagged with a "Player" tag. (More info on 'Tags' in the 'Creator's Guide")
    
        * Audio Manager: In the scene's hierarchy you will find a game object 'AudioManager'. Assign it to this variable.
                        This  takes cares of the coin soundFX.

    Once this is done the coin should now be collectible and be attracted to player when in range.
**/

//----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    // Audio players components.
	public AudioSource Coin_SoundFX;
	public AudioSource Chest_SoundFX;

	// Play a single clip through the sound effects source.
	public void playCoinSound()
	{
		//gameObject.GetComponent<AudioSource>().Play();
		Coin_SoundFX.Play();
	}

	public void playChestSound()
	{
		Chest_SoundFX.Play();
	}
}