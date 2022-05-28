/**
    Script Description

    This script is supposed to be attached to the HealthBar in the HealthBar scene.
    The HealthBar contains the health of the main character.

        * Slider: An slider which can modify the shield amount of the shield bar.

        * setHealth: Set the current player shield into the shield bar.

        * setMaxHealth: This methode lets the slider know what the max shield is an also set the current player shield into the shield bar.        
**/




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxShield(int shield)
    {
        slider.maxValue = shield;
        slider.value = 0;
    }
    public void setShield(int shield)
    {
        slider.value = shield;
    }
}