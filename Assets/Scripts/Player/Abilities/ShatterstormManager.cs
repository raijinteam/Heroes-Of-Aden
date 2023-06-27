using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterstormManager : MonoBehaviour
{
    // EXPLODES ON HIT

    public string powerUpName;
    public Sprite powerUpIcon;
    public string powerUpInfo;

    public int[] all_DamageValues;
    public int[] all_SplashDamageValues;
    public int[] all_TotalMissileCount;
    public float[] all_SpawnRate;

    public string[] all_LevelInfo;
}
