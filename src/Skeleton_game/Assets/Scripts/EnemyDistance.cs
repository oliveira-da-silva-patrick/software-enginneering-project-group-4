using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance
{

    public float distance;
    public GameObject enemy;
    
    public EnemyDistance(float distance, GameObject enemy)
    {
        this.enemy = enemy;
        this.distance = distance;
    }

}
