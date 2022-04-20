using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;
    public bool isBought = false;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkill;

    public void UpdateUI()
    {
        TitleText.text = $"Cost: {skillTree.SkillCosts[id]} ECTS\n{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}";

        GetComponent<Image>().color = isBought ? Color.yellow : Color.white;
        
        foreach (var connectedSkill in ConnectedSkill)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(isBought);
            skillTree.ConnectorsList[connectedSkill].SetActive(isBought);
        }
    }

    public void Buy()
    {
        if (skillTree.ECTS < skillTree.SkillCosts[id] || isBought) return;
        skillTree.ECTS -= skillTree.SkillCosts[id];
        isBought = true;
        skillTree.UpdateAllSkillUI();
    }

    public void refund()
    {
        if (!isBought) return;
        skillTree.ECTS += skillTree.SkillCosts[id];
        isBought = false;
        skillTree.UpdateAllSkillUI();
    }

}
