using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class healthSystem : MonoBehaviour
{
    public int health = 250;
    private bool isShootingStunned = false;
    private bool isMovementStunned = false;
    private float stunTime = 2;

    private void Start()
    {
        Load();
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
}
