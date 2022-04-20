using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescriptionText;

    public int[] ConnectedSkill;

    public void UpdateUI()
    {
        TitleText.text = $"Cost: {skillTree.SkillCosts[id]} ECTS\n{skillTree.SkillNames[id]}";
        DescriptionText.text = $"{skillTree.SkillDescriptions[id]}";

        GetComponent<Image>().color = SkillTree.UnlockedAbilities[id] ? Color.yellow : Color.white;
        
        foreach (var connectedSkill in ConnectedSkill)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(SkillTree.UnlockedAbilities[id]);
            skillTree.ConnectorsList[connectedSkill].SetActive(SkillTree.UnlockedAbilities[id]);
        }
    }

    public void Buy()
    {
        if (skillTree.ECTS < skillTree.SkillCosts[id] || SkillTree.UnlockedAbilities[id]) return;
        skillTree.ECTS -= skillTree.SkillCosts[id];
        SkillTree.UnlockedAbilities[id] = true;
        Abilities.unlockAbility(id);
        skillTree.UpdateAllSkillUI();
    }

    public void refund()
    {
        if (!SkillTree.UnlockedAbilities[id]) return;
        skillTree.ECTS += skillTree.SkillCosts[id];
        SkillTree.UnlockedAbilities[id] = false;
        UpdateUI();
    }

}
