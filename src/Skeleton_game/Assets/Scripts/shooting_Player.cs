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
