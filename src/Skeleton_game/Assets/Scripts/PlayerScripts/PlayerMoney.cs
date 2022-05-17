using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [HideInInspector]
    public int money;

    void Start() {
        money = 0;
    }
    
}
