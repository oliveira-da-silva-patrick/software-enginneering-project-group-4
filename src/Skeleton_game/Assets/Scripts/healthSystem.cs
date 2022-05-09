using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class healthSystem : MonoBehaviour
{
    private int currHealth;
    public int health  = 250;
    public HealthBar healthBar = null;
    private bool isShootingStunned = false;
    private bool isMovementStunned = false;
    private float stunTime = 2;
    float PoisonDamageInterval = 1;

    private void Start()
    {
        currHealth = health;
        if(healthBar != null)
        {
            healthBar.setMaxHealth(health);
        }
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
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;

        if(healthBar != null)
        {
            healthBar.setHealth(currHealth);
        }

        if (currHealth <= 0)
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

    IEnumerator PoisonDamage2(){
        float PoisonCounter = 0;
     while(PoisonCounter < 5f){
         currHealth -= 5;
         yield return new WaitForSeconds(PoisonDamageInterval);
         PoisonCounter += PoisonDamageInterval;
         if (currHealth <= 0)
        {
            Die();
        }
     }
    }

    IEnumerator PoisonDamage1(){
        float PoisonCounter = 0;
     //$$anonymous$$eeps damaging player until the poison has "worn off"
     while(PoisonCounter < 5f){
         currHealth -= 2;
         yield return new WaitForSeconds(PoisonDamageInterval);
         PoisonCounter += PoisonDamageInterval;
         if (currHealth <= 0)
        {
            Die();
        }
     }
    }
}

