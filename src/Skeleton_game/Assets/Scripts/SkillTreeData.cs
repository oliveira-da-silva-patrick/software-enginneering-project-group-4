using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillTreeData
{
    public int ECTS;
    public List<int> boughtSkills;

    public SkillTreeData(SkillTree skillTree)
    {
        for(int i = 0; i < skillTree.SkillList.Count; i++)
        {
            if(skillTree.SkillList[i] != null && skillTree.SkillList[i].isBought)
            {
                Debug.Log(i);
            }
        }
        ECTS = skillTree.ECTS;
    }
}
