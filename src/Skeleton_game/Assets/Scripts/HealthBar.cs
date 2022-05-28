/**
    Script Description

    This script is supposed to be attached to the HealthBar in the HealthBar scene.
    The HealthBar contains the health of the main character.

        * Slider: An slider which can modify the health amount of the healthbar.

        * setHealth: Set the current player health into the healthbar.

        * setMaxHealth: This methode lets the slider know what the max health is an also set the current player health into the healthbar.        
**/

//----------------------------------------------------------




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void setHealth(int health)
    {
        slider.value = health;
    }
}
