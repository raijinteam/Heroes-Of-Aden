using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharchterData : MonoBehaviour
{
    public Sprite playerIcon;
    public string name;
    public int level;
    public bool isLocked;
    [TextArea(2,6)]
    public string description;
    public bool isCoinBased;
    public int[] upgradeAmount;
    public float[] moveSpeed;
    public float[] health;
    public float[] damage;
    public float[] firerate;
    public float[] criticalDamage;
    public float[] criticalChance;



    public float GetPlayerMovementSpeed()
    {
        return moveSpeed[level - 1];
    }

    public float GetPlayerHealth()
    {
        return health[level];
    }

    public float GetPlayerDamage()
    {
        return damage[level - 1];
    }

    public float GetPlayerFirerate()
    {
        Debug.Log("Level : " + level);
        return firerate[level - 1];
    }

    public float GetCriticalDamage()
    {
        float percentage = criticalDamage[level - 1];
        return percentage;
    }

    public float GetCriticalChance()
    {
        float percentage = criticalChance[level - 1] ;
        return percentage;
    }
}
