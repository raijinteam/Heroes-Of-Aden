using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgradeData : MonoBehaviour
{
    public string upgradeName;
    public int currentLevel;
    public string description;
    public float[] all_Values;


    public float GetMyPercentage()
    {
        float percentage = all_Values[currentLevel - 1] / 100;
        return percentage;
    }
}
