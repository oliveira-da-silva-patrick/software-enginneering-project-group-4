using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{


    public static void resetAbilities()
    {
        for (int i = 0; i< SkillTree.UnlockedAbilities.Length; i++)
        {
            
        }
    }

    public static void unlockAbility(int i)
    {
        switch (i)
        {
            // Engineering
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;

            // Comp. Sci
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;

            // Physics
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;

            // Medicine 
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            case 21:
                break;

            // Math
            case 22:
                break;
            case 23:
                break;
            case 24:
                break;
            case 25:
                break;
            default:
                Debug.Log("This should not have happened");
                break;
        }
    }


}
