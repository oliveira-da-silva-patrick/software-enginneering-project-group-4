using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class healthSystem : MonoBehaviour
{
    private float initialShootingSpeed;
    private float initialMovementSpeed;
    public int health = 50;
    private int maxHealth;
    private bool isShootingStunned = false;
    private bool isMovementStunned = false;
    private float stunTime = 2;
    float PoisonDamageInterval = 1;

    PlayerMoney playerMoney;
    int hasMoney;
    // Loot
    public GameObject coin;
    public GameObject diamond;

    private void Start()
    {
        Load();
        if (SkillTree.UnlockedAbilities[18])
        {
            health = health + 20;
        }
        if (SkillTree.UnlockedAbilities[20])
        {
            health = health + 30;
        }
        if (SkillTree.UnlockedAbilities[21])    
        {
            health = health + 50;
        }
        maxHealth = health;

        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
    }

    public void smallPotion()
    {
        hasMoney = playerMoney.money;
        if (hasMoney >= 50)
        {
            playerMoney.money -= 50;
            health += 25;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

    }

    public void bigPotion()
    {
        hasMoney = playerMoney.money;
        if (hasMoney >= 80)
        {
            playerMoney.money -= 80;
            health += 50;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DropLoot();
            Die();
        }
        if (gameObject.CompareTag("Enemy"))
        {
            // shooting stun
            if (SkillTree.UnlockedAbilities[8])
            {
                isShootingStunned = true;
                stunTime = 2;
            }
            // movement stun
            else if (SkillTree.UnlockedAbilities[9])
            {
                isMovementStunned = true;
                stunTime = 2;
            }
        }
        // Debug.Log(health);
    }

    public void poisonDamage1()
    {
        StartCoroutine("PoisonDamage1");
    }

    public void poisonDamage2()
    {
        StartCoroutine("PoisonDamage2");
    }
    private void Update()
    {

        AI_Shooting AI_shootingScript = GetComponent<AI_Shooting>();
        AIPath AI_movementScript = GetComponent<AIPath>();

        Shooting shootingScript = GetComponent<Shooting>();
        FieldOfView movementScript = GetComponent<FieldOfView>();

        if (AI_shootingScript != null && AI_movementScript != null && gameObject.CompareTag("Enemy"))
        {
            initialMovementSpeed = AI_movementScript.maxSpeed;
            initialShootingSpeed = AI_shootingScript.startTimeBtwShots;
            if (stunTime >= 0 && (isShootingStunned || isMovementStunned))
            {
                if (isShootingStunned)
                {
                    AI_shootingScript.startTimeBtwShots = 2f;
                }
                if (isMovementStunned)
                {
                    AI_movementScript.maxSpeed = 1;
                }
                stunTime -= Time.deltaTime;
            }
            else
            {
                // TODO
                AI_movementScript.maxSpeed = initialMovementSpeed;
                AI_shootingScript.startTimeBtwShots = initialShootingSpeed;
                isShootingStunned = false;
                isMovementStunned = false;
            }

            // Debug.Log("Movement speed: " + movementScript.maxAcceleration + "Shooting Speed " + shootingScript.startTimeBtwShots);
        } else if(shootingScript != null && movementScript != null && gameObject.CompareTag("Enemy"))
        {
            initialMovementSpeed = movementScript.speed;
            initialShootingSpeed = shootingScript.startTimeBtwShots;
            if (stunTime >= 0 && (isShootingStunned || isMovementStunned))
            {
                if (isShootingStunned)
                {
                    shootingScript.startTimeBtwShots = 2f;
                }
                if (isMovementStunned)
                {
                    movementScript.speed = 1;
                }
                stunTime -= Time.deltaTime;
            }
            else
            {
                // TODO
                movementScript.speed = initialMovementSpeed;
                shootingScript.startTimeBtwShots = initialShootingSpeed;
                isShootingStunned = false;
                isMovementStunned = false;
            }
        }
    }
    public void Load()
    {
        SkillTreeData data = SaveLoadSystem.LoadSkillTree();

        if (data != null)
        {
            SkillTree.UnlockedAbilities = (bool[])data.UnlockedAbilities.Clone();
        }
        else
        {
            for (var i = 0; i < SkillTree.UnlockedAbilities.Length; i++)
            {
                SkillTree.UnlockedAbilities[i] = false;
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator PoisonDamage2(){
        float PoisonCounter = 0;
     while(PoisonCounter < 5f){
         health -= 5;
         yield return new WaitForSeconds(PoisonDamageInterval);
         PoisonCounter += PoisonDamageInterval;
         if (health <= 0)
        {
            Die();
        }
     }
    }

    IEnumerator PoisonDamage1(){
        float PoisonCounter = 0;
     //$$anonymous$$eeps damaging player until the poison has "worn off"
     while(PoisonCounter < 5f){
         health -= 2;
         yield return new WaitForSeconds(PoisonDamageInterval);
         PoisonCounter += PoisonDamageInterval;
         if (health <= 0)
        {
            Die();
        }
     }
    }

    public void DropLoot()
    {
        var cn = Random.Range(0,4);
        var dn = Random.Range(0,5);
        //Spawns coins
        for (int i = 0; i < cn; i++)
        {
        //Defines explosion radius for the collectibles dropped by the enemy
        var x = Random.Range(-1.5f, 1.5f); 
        var y = Random.Range(-1.5f, 1.5f);
        //Creates Coin game object in scene
        GameObject CoinObject = (GameObject)Instantiate(coin, transform.position, Quaternion.identity);

        //Adds velocity to each instantiated coin in a random direction to create explosion effect
        CoinObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x*10,y*10);
        
        }
        //Spawns Diamonds
        switch (dn)
        {
            case 1:
                var x = Random.Range(-1.5f, 1.5f); 
                var y = Random.Range(-1.5f, 1.5f);
                
                GameObject DiamondObject = (GameObject)Instantiate(diamond, transform.position, Quaternion.identity);

                DiamondObject.GetComponent<Rigidbody2D>().velocity = new Vector2(x*10,y*10);
                break;
            default:
                //Do nothing
                break;
        }
    }
}

