using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake()
    {
        skillTree = this;
    }

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorsList;
    public GameObject ConnectorsHolder;

    public int ECTS;

    private void Start()
    {
        ECTS = 20;
        SkillLevels = new int[6];
        SkillCaps = new[] { 1, 5, 5, 2, 10, 10 };
        SkillNames = new[] { "Upgrade 1", "Upgrade 2", "Upgrade 3", "Upgrade 4", "Upgrade 5", "Upgrade 6" };
        SkillDescriptions = new[]
        {
            "Does sth 1",
            "Does sth 2",
            "Does sth 3",
            "Does sth 4",
            "Does sth 5",
            "Does sth 6"
        };

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

        SkillList[0].ConnectedSkill = new[] { 1, 2, 3 };
        SkillList[2].ConnectedSkill = new[] { 4, 5 };

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach(var skill in SkillList)
        {
            skill.UpdateUI();
        }
    }
}
