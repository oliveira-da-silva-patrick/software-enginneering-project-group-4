using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An instance of this class will be handed over to the save-load-system so that only part of the data will be saved.

[System.Serializable]
public class SkillTreeData // "copy" of skill tree with only its most important information
{
    public int ECTS;
    public bool[] UnlockedAbilities;

    public SkillTreeData(SkillTree skillTree)
    {
        UnlockedAbilities = (bool[]) SkillTree.UnlockedAbilities.Clone();
        ECTS = skillTree.ECTS;
    }
}
