/**
    Script Description

    This script is supposed to be attached to the SkillTree in the SkillTree scene. There are 9 serialized fields that are mostly dynamically filled at runtime, with the exception for the SkillHolder and ConnectorsHolder.
    The SkillTree contains all Skills and connectors and configures them in correlation to the ECTS currency.

        * SkillCosts: An array containing the corresponding value of the cost for each skill, each Skill can be found at the position of its id.

        * SkillNames: An array containing the corresponding skill names for each skill, each Skill can be found at the position of its id.
        
        * SkillDescription: An array containing the corresponding skill description for each skill, each Skill can be found at the position of its id.

        * UnlockedAbilities: A bool array containing the corresponding value if a skill has been bought by the user, each Skill can be found at the position of its id.
        
        * SkillList: A list containing all skills, initialized in the start method.
        
        * SkillHolder: An empty game object containing all skills.
        
        * ConnectorsList: A list containing all connectors, initialized in the start method.
        
        * ConnectorsHolder: An empty game object containing all connectors.
        
        * ECTS: The currency value, it is updated after a purchase or a tree reset.

        * ECTS_Text: A text field containing the ECTS value, showing it to the player.
**/

//----------------------------------------------------------


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
    public static bool[] UnlockedAbilities = null;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorsList;
    public GameObject ConnectorsHolder;

    public static int ECTS = 0;
    public Text ECTS_Text;

    private void Start()
    {
        ECTS_Text = GameObject.Find("ECTSInfoText").GetComponent<Text>();

        SkillCosts = new[] { 
                        // Engineering
                        4, 6, 8, 8, 10, 20
                        // Comp Sci
                        , 4, 4, 12, 12
                        // Physics
                        , 6, 4, 8, 8, 6, 10, 12
                        // Medicine
                        , 4, 8, 8, 10, 10
                        // Maths
                        , 6, 8, 8, 10};
        SkillNames = new[] {
        "Laser",
        "Damage increase",
        "2 enemies",
        "3 enemies",
        "Damage increase",
        "Super Laser",
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
        "Laser damage is increased to 20",
        "Laser attacks 2 closest enemies in range",
        "Laser attacks 3 closest enemies in range",
        "Laser damage is increased to 30",
        "Laser strikes all enemies in range",
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
        Debug.Log(ECTS);
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
        SaveLoadSystem.SaveSkillTree();
    }

    public void Load()
    {
        SkillTreeData data = SaveLoadSystem.LoadSkillTree();

        if (data != null)
        {
            ECTS = data.ECTS;
            // ECTS = 0;
            if (data.UnlockedAbilities != null)
            {
                UnlockedAbilities = (bool[])data.UnlockedAbilities.Clone();
            }
        }
    }

    public void HardReset()
    {
        resetSkillTree();
        ECTS = 0;
        GameInfo.fillEmpty();
        UpdateAllSkillUI();
    }
}
