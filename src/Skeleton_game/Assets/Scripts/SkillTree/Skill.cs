/**
    Script Description

    This script is supposed to be attached to a single 'Skill' in the SkillTree scene. There are 4 serialized fields that are dynamically filled at runtime.
    The Skill is clickable, which will lead to its connected skills, as well as the corresponding connectors, to appear.

        * Id: A unique identifier for each skill, also the position of each skill in the various SkillTree arrays.

        * Title Text: The title text provides the skills cost, as well as it's name.
        
        * Description Text: The description text provides a small description or explanation of the skills properties.

        * ConnectedSkill: This int array is filled for each skill in the SkillTree Script at runtime and holds the id's of the connected skills.
**/

//----------------------------------------------------------


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
        TitleText.text = $"{skillTree.SkillCosts[id]} ECTS\n{skillTree.SkillNames[id]}";
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
        if (SkillTree.ECTS < skillTree.SkillCosts[id] || SkillTree.UnlockedAbilities[id]) return;
        SkillTree.ECTS -= skillTree.SkillCosts[id];
        SkillTree.UnlockedAbilities[id] = true;
        Abilities.unlockAbility(id);
        skillTree.UpdateAllSkillUI();
    }

    public void refund()
    {
        if (!SkillTree.UnlockedAbilities[id]) return;
        SkillTree.ECTS += skillTree.SkillCosts[id];
        SkillTree.UnlockedAbilities[id] = false;
        UpdateUI();
    }

}
