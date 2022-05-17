using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyText : MonoBehaviour
{

    TMPro.TextMeshProUGUI textBox;
    GameObject player;
    int moneycount;
    
    void Start()
    {
        textBox = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        moneycount = PlayerMoney.money;
        textBox.text = "Money = " + moneycount; 
    }
}