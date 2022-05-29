/**
    Script Description

    This script is supposed to be attached to the HealthBar in the HealthBar scene.
    The HealthBar contains the health of the main character.

        * speed: The value for the speed that the projectile moves.

        * rb: Projectile's Rigidbody2D.

        * damage: The value for the damage that the projectile does.

        * piercingEnabled: The boolean which tells the projectile if the piercing skill is enabled.      
**/

//----------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_Player_Axis : MonoBehaviour
{
   public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 10;
    private bool piercingEnabled;


    // Start is called before the first frame update
    //In this start methode the code goes trough the skills from the skill-tree to make sure that the projectile gets the player chosen upgrades
    void Start()
    {
        Load();
        rb.velocity = transform.right * speed;
        piercingEnabled = SkillTree.UnlockedAbilities[6];
        if (SkillTree.UnlockedAbilities[25])
        {
            damage = damage + 20;
        }
    }


    //In this methode if the projectile collides with the enemy it does damage and can also active some skills if they are enabled
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        healthSystem opponent = hitInfo.GetComponent<healthSystem>();
        if(opponent != null)
        {
            opponent.TakeDamage(damage);
        }
        Debug.Log(hitInfo.name);
        // if piercing shot is enabled
        if (piercingEnabled)
        {
            if (hitInfo.name == "Wall" || hitInfo.name == "Doors" || hitInfo.name == "Escalator" || hitInfo.CompareTag("Player") || hitInfo.CompareTag("Boundary")){
                Destroy(gameObject);
            }
        }
        else
        {
            if (hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Boundary"))
            {
                    Destroy(gameObject);
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
}
