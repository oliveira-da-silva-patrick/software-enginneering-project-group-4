/**
    Script Description

    This script is supposed to be attached to the HealthBar in the HealthBar scene.
    The HealthBar contains the health of the main character.

        * speed: The value for the speed that the projectile moves.

        * rb: Projectile's Rigidbody2D.

        * damage: The value for the damage that the projectile does.

        * piercingEnabled: The boolean which tells the projectile if the piercing skill is enabled.

        * poison1Enabled: The boolean which tells the projectile if the poison1 skill is enabled.

        * poison2Enabled: The boolean which tells the projectile if the poison2 skill is enabled.      
**/

//----------------------------------------------------------




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_Player : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    private bool piercingEnabled;
    private bool poison1Enabled;
    private bool poison2Enabled;


    // Start is called before the first frame update
    //In this start methode the code goes trough the skills from the skill-tree to make sure that the projectile gets the player chosen upgrades
    void Start()
    {
        Load();
        rb.velocity = transform.right * speed;
        if (SkillTree.UnlockedAbilities != null)
        {
            piercingEnabled = SkillTree.UnlockedAbilities[6];
            if (SkillTree.UnlockedAbilities[12])
            {
                damage = damage + 15;
            }
            if (SkillTree.UnlockedAbilities[16])
            {
                damage = damage + 25;
            }
            if (SkillTree.UnlockedAbilities[17])
            {
                poison1Enabled = true;
            }
            if (SkillTree.UnlockedAbilities[19])
            {
                poison2Enabled = true;
            }
        }
    }

    //In this methode if the projectile collides with the enemy it does damage and can also active some skills if they are enabled
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Player"))
        {
            healthSystem opponent = hitInfo.GetComponent<healthSystem>();
            if(opponent != null)
            {
                opponent.TakeDamage(damage);
                if(poison2Enabled)
                {
                    opponent.poisonDamage2();
                }
                else if(poison1Enabled)
                {
                    opponent.poisonDamage1();
                }
            }
        }
        
        // if piercing shot is enabled
        if (piercingEnabled)
        {
            if (hitInfo.name == "Wall" || hitInfo.name == "Doors" || hitInfo.name == "Escalator" || hitInfo.CompareTag("Player") || hitInfo.CompareTag("Boundary")){
                if (!hitInfo.name.Contains("Camera"))
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Boundary"))
            {
                if (!hitInfo.name.Contains("Camera"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    
    public void Load()
    {
        SkillTreeData data = SaveLoadSystem.LoadSkillTree();

        if (data != null)
        {
            if (data.UnlockedAbilities != null)
            {
                SkillTree.UnlockedAbilities = (bool[])data.UnlockedAbilities.Clone();
            }
        }
        else
        {
            for (var i = 0; i < SkillTree.UnlockedAbilities.Length; i++)
            {
                SkillTree.UnlockedAbilities[i] = false;
            }
        }
    }


}
