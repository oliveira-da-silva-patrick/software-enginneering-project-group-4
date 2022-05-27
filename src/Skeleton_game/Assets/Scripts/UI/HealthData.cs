using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the health information that is going to be stored in and loaded from savefiles

[System.Serializable]
public class HealthData
{
    public int lostHealth;
    public int lostShield;

    public HealthData()
    {
        lostHealth = Damage.lostHealth;
        lostShield = Damage.lostShield;
    }
}
