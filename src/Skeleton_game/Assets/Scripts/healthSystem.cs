using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class healthSystem : MonoBehaviour
{

    public int health = 250;
    private int maxHealth;
    private bool isShootingStunned = false;
    private bool isMovementStunned = false;
    private float stunTime = 2;
    float PoisonDamageInterval = 1;

    PlayerMoney playerMoney;
    int hasMoney;

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
            if (health > maxHealth)
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
            if (health > maxHealth)
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
        Debug.Log(health);
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
        AI_Shooting shootingScript = GetComponent<AI_Shooting>();
        AIPath movementScript = GetComponent<AIPath>();

        if (shootingScript != null && movementScript != null && gameObject.CompareTag("Enemy"))
        {
            if (stunTime >= 0 && (isShootingStunned || isMovementStunned))
            {
                if (isShootingStunned)
                {
                    shootingScript.startTimeBtwShots = 2f;
                }
                if (isMovementStunned)
                {
                    movementScript.maxSpeed = 1;
                }
                stunTime -= Time.deltaTime;
            }
            else
            {
                movementScript.maxSpeed = 1;
                shootingScript.startTimeBtwShots = 0.8f;
                isShootingStunned = false;
                isMovementStunned = false;
            }

            // Debug.Log("Movement speed: " + movementScript.maxAcceleration + "Shooting Speed " + shootingScript.startTimeBtwShots);
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

    IEnumerator PoisonDamage2()
    {
        float PoisonCounter = 0;
        while (PoisonCounter < 5f)
        {
            health -= 5;
            yield return new WaitForSeconds(PoisonDamageInterval);
            PoisonCounter += PoisonDamageInterval;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    IEnumerator PoisonDamage1()
    {
        float PoisonCounter = 0;
        //$$anonymous$$eeps damaging player until the poison has "worn off"
        while (PoisonCounter < 5f)
        {
            health -= 2;
            yield return new WaitForSeconds(PoisonDamageInterval);
            PoisonCounter += PoisonDamageInterval;
            if (health <= 0)
            {
                Die();
            }
        }
    }
}