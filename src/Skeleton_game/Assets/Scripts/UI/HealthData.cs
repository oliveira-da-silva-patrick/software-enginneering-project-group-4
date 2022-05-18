using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
