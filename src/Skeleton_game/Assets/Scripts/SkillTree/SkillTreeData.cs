using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillTreeData
{
    public int ECTS;
    public bool[] UnlockedAbilities;

    public SkillTreeData(SkillTree skillTree)
    {
        UnlockedAbilities = (bool[]) SkillTree.UnlockedAbilities.Clone();
        ECTS = skillTree.ECTS;
    }
}