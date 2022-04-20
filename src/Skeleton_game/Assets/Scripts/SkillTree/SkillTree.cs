using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake()
    {
        skillTree = this;
    }

    public int[] SkillCosts;
    public string[] SkillNames;
    public string[] SkillDescriptions;
    public static bool[] UnlockedAbilities;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorsList;
    public GameObject ConnectorsHolder;

    public int ECTS;
    public Text ECTS_Text;

    private void Start()
    {        
        ECTS = 20;
        ECTS_Text = GameObject.Find("ECTSInfoText").GetComponent<Text>();

        SkillCosts = new[] { 
                        // Engineering
                        1, 2, 2, 2, 2, 2
                        // Comp Sci
                        , 2, 2, 2, 2
                        // Physics
                        , 2, 2, 2, 2, 2, 2, 2
                        // Medicine
                        , 2, 2, 2, 2, 2
                        // Maths
                        , 2, 2, 2, 2};
        SkillNames = new[] {
        "Lightning",
        "Damage increase",
        "2 enemies",
        "3 enemies",
        "Damage increase",
        "Lightning Bolt",
        "Piercing Shot",
        "Hacking",
        "Shooting hack",
        "Movement hack",
        "Fast shot",
        "Damage magnet",
        "Damage increase ",
        "Faster shot",
        "2 magnets",
        "3 magnets",
        "Ultimate damage increase",
        "Poison Bullet",
        "Health Buff",
        "Poison Damage",
        "Health Buff",
        "Health Buff",
        "X-Shot",
        "Y-Shot",
        "Timer decrease",
        "Damage increase"
        };
        SkillDescriptions = new[]
        {
        "10 damage per second to closest enemy in range",
        "Lightning damage is increased to 20",
        "Lightning attacks 2 closest enemies in range",
        "Lightning attacks 3 closest enemies in range",
        "Lightning damage is increased to 30",
        "Lightning strikes all enemies in range",
        "Bullets go through enemies",
        "Player learns the ability to hack",
        "When enemies are hit, they are hacked and shoot at a slower rate for 2 seconds",
        "When enemies are hit, they are hacked and move at a slower rate for 2 seconds",
        "Shooting cooldown decreased by 0.2 seconds",
        "A magnet circling around the player that deals 15 damage",
        "Conventional bullets deal 15 more damage",
        "Shooting cooldown decreased by 0.3 seconds",
        "2 magnets that circle around the player",
        "3 magnets that circle around the player",
        "Conventional bullets deal 25 more damage",
        "Conventional bullets are laced with poison, dealing 2 damage per second for 5 seconds",
        "Player has 20 more HP",
        "Poison bullets damage is increased to 5 damage per second",
        "Player gains another 30 HP",
        "Player HP is increased by 50",
        "Player shoots 2 bullets on the X-axis every 2 seconds, dealing 10 damage",
        "Player shoots 2 bullets on the Y-axis every 2 seconds, dealing 10 damage",
        "Axis bullets cooldown is decreased to 1 second",
        "Axis bullets damage is increased to 20"
        };

        UnlockedAbilities = new bool[SkillCosts.Length];

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>())
        {
            SkillList.Add(skill);
        }

        foreach (var connector in ConnectorsHolder.GetComponentsInChildren<RectTransform>())
        {
            ConnectorsList.Add(connector.gameObject);
        }

        for (var i = 0; i < SkillList.Count; i++)
        {
            SkillList[i].id = i;
        }

        for (var i = 0; i < UnlockedAbilities.Length; i++)
        {
            UnlockedAbilities[i] = false;
        }

        Load();


        // Engineering
        SkillList[0].ConnectedSkill = new[] { 1, 2 };
        SkillList[1].ConnectedSkill = new[] { 4 };
        SkillList[2].ConnectedSkill = new[] { 3 };
        SkillList[3].ConnectedSkill = new[] { 5 };

        // Comp.Sci
        SkillList[7].ConnectedSkill = new[] { 8, 9 };

        // Physics
        SkillList[10].ConnectedSkill = new[] { 13 };
        SkillList[11].ConnectedSkill = new[] { 14 };
        SkillList[14].ConnectedSkill = new[] { 15 };
        SkillList[12].ConnectedSkill = new[] { 16 };

        // Medicine
        SkillList[17].ConnectedSkill = new[] { 19 };
        SkillList[20].ConnectedSkill = new[] { 21 };
        SkillList[18].ConnectedSkill = new[] { 20 };

        // Maths
        SkillList[22].ConnectedSkill = new[] { 23 };
        SkillList[23].ConnectedSkill = new[] { 24 };
        SkillList[24].ConnectedSkill = new[] { 25 };    

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach(var skill in SkillList)
        {
            skill.UpdateUI();
        }
        ECTS_Text.text = "ECTS: " + ECTS;
        Save();
    }

    public void resetSkillTree()
    {
        foreach(var skill in SkillList)
        {
            skill.refund();
        }
        UpdateAllSkillUI();
    }

    public void Save()
    {
        SaveScript.SaveSkillTree(skillTree);
    }

    public void Load()
    {
        SkillTreeData data = SaveScript.LoadSkillTree();

        if (data != null)
        {
            ECTS = data.ECTS;
            //UnlockedAbilities = data.UnlockedAbilities;
        }
    }

}
