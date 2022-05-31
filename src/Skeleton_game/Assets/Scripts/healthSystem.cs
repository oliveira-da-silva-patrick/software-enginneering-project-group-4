/**
    Script Description

    This script is supposed to be attached to the HealthBar in the HealthBar scene.
    This script is also attached to each enemy and player, the differenciation is made inside each method.
    The HealthBar contains the health of the main character.

        * Health: The current health amount of the attached object.

        * shield: The current shield amount of the attached object.

        * maxHealth: The maximum health amount that the attached object has.

        * maxShield: The maximum shield amount that the attached object has.

        * healthBar: An reference to the players health bar. This only available to the player.
        
        * enemyHealthBar: An reference to the final boss health bar. This only available to the final boss.

        * shieldBar: An reference to the players shield bar. This only available to the player.

        * PoisonDamageInterval: The time interval between each time the enemy receives poison damage.      
**/

//----------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class healthSystem : MonoBehaviour
{
    private float initialShootingSpeed;
    private float initialMovementSpeed;
    public int health = 50;
    public int shield = 0;
    public int maxShield = 250;
    public int maxHealth;
    public HealthBar healthBar;
    public HealthBar enemyHealthBar;
    public ShieldBar shieldBar;
    private bool isShootingStunned = false;
    private bool isMovementStunned = false;
    private float stunTime = 2;
    float PoisonDamageInterval = 1;

    PlayerMoney playerMoney;
    int hasMoney;
    // Loot
    public GameObject coin;
    public GameObject diamond;


    //      At the beginning of every start of a the game, this class sets the amount of health and shield to their respective bars and it also checks if the user has upgraded the max health
    
    private void Start()
    {
        // The following checks if the player has any health-based abilities unlocked and sets the maximum health accordingly
        Load();
        if (SkillTree.UnlockedAbilities != null)
        {
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
        maxHealth = health;
        // whenever a new scene is loaded, the player health needs to be updated accordingly
        if (gameObject.tag == "Player")
        {
            LoadHealth();
            healthBar.setMaxHealth(maxHealth);
            shieldBar.setMaxShield(maxHealth);
            health -= Damage.lostHealth;
            shield = maxShield - Damage.lostShield;
            healthBar.setHealth(health);
            shieldBar.setShield(shield);
        }

        if (gameObject.name == "AI_FinalBoss")
        {
            enemyHealthBar.setMaxHealth(maxHealth-100);
        }

        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
    }

    /*
     * smallPotion() is called whenever a small potion is bought at the vending machine, and the player health is updated
     * Update value: 25
     */
    public void smallPotion()
    {
        hasMoney = PlayerMoney.money;
        if (hasMoney >= 50)
        {
            PlayerMoney.money -= 50;
            if(health + 25 >=  maxHealth)
            {
                health = maxHealth;
                Damage.lostHealth = 0;
            }
            else
            {
                health += 25;
                Damage.lostHealth = Damage.lostHealth - 25;
            }
            healthBar.setHealth(health);
        }
        healthBar.setHealth(health);
    }

    /*
    * bigPotion() is called whenever a large potion is bought at the vending machine, and the player health is updated
    * Update value: 50
    */
    public void bigPotion()
    {
        hasMoney = PlayerMoney.money;
        if (hasMoney >= 80)
        {
            PlayerMoney.money -= 80;
            if(health + 50 >=  maxHealth)
            {
                health = maxHealth;
                Damage.lostHealth = 0; 
            }
            else
            {
                health += 50;
                Damage.lostHealth = Damage.lostHealth - 50; 
            }
            healthBar.setHealth(health);
        }
        healthBar.setHealth(health);
    }

    /*
    * TakeDamage() reduces the health to the cuorresponding damage received
    * it also resets the health/shield to the healthbar/shieldbar. ...enemy part...
    */
    public void TakeDamage(int damage)
    {
        int storeDamage = damage;
        if (shield > 0)
        {
            if (damage > shield)
            {
                shield = 0;
                storeDamage-=shield;
            }
            else
            {
                shield-=damage;
                storeDamage = 0;
            }
        }
        health -= storeDamage;
        if (gameObject.tag == "Player")
        {
            Damage.lostHealth += storeDamage;
            Damage.lostShield = maxShield - shield;
            healthBar.setHealth(health);
            shieldBar.setShield(shield);
        }
        if (gameObject.name == "AI_FinalBoss")
        {
            if(health > 100)
            {
                enemyHealthBar.setHealth(health-100);
            }
            else
            {
                enemyHealthBar.setHealth(0);
            }
        }
        if (health <= 0)
        {
            DropLoot();
            Die();
        }
        // if an enemy is shot
        if (gameObject.CompareTag("Enemy"))
        {
            // shooting stun
            if (SkillTree.UnlockedAbilities != null)
            {
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
        // Debug.Log(health);
    }

    //activates the poison damage
    public void poisonDamage1()
    {
        StartCoroutine("PoisonDamage1");
    }

    //activates the larger poison damage
    public void poisonDamage2()
    {
        StartCoroutine("PoisonDamage2");
    }
    private void Update()
    {
        checkStuns();
    }


    /*
     * checkStuns() checks if the player has abilities unlocked, and handles the consequences accordingly
     * The shooting stun & movement stun are the focus of this method
     * This method is calles in Update
     */
    private void checkStuns()
    {
        AI_Shooting AI_shootingScript = GetComponent<AI_Shooting>();
        AIPath AI_movementScript = GetComponent<AIPath>();

        Shooting shootingScript = GetComponent<Shooting>();
        FieldOfView movementScript = GetComponent<FieldOfView>();

        // checks if the hit entity is a normal type enemy, if so, the stun is placed for a period of time
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
                AI_movementScript.maxSpeed = initialMovementSpeed;
                AI_shootingScript.startTimeBtwShots = initialShootingSpeed;
                isShootingStunned = false;
                isMovementStunned = false;
            }

        }
        // for the intelligent enemies, as they use different scriptnames, but should also be affected by the stuns
        else if (shootingScript != null && movementScript != null && gameObject.CompareTag("Enemy"))
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
                movementScript.speed = initialMovementSpeed;
                shootingScript.startTimeBtwShots = initialShootingSpeed;
                isShootingStunned = false;
                isMovementStunned = false;
            }
        }
    }

    // Load() allows the Skilltree data to be regained, if the game has been closed
    public void Load()
    {
        SkillTreeData data = SaveLoadSystem.LoadSkillTree();

        if (data != null)
        {
            if (SkillTree.UnlockedAbilities != null)
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

    //Destroys the the player if the health is 0 or lower.
    void Die()
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("DeathScene");
        }
        SaveLoadSystem.deleteSaveFile();
    }

    //This methode gives the enemy 5 poison damage during a certain period of time
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

    //This method gives the enemy 2 poison damage during a certain period of time
    IEnumerator PoisonDamage1() {
         float PoisonCounter = 0;
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

    // LoadHealth() reloads the correct health value whenever the game was closed
    public void LoadHealth()
    {
        HealthData data = SaveLoadSystem.LoadHealth();

        if(data != null)
        {
            Damage.lostHealth = data.lostHealth;
            Damage.lostShield = data.lostShield;
        }
    }
}

