using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpHandler : MonoBehaviour
{
	public bool isPassivePowerUp;
	public int currentLevel;

	public abstract string GetMyPowerName();

	public abstract int GetMyCurrentLevel();

	public abstract Sprite GetMyIcon();

	public abstract string GetMyPowerInfo();

	public abstract void SetUpdateInfoPanel(int _infoPanelIndex);

	public abstract void LevelUp();

	public abstract void UpdateCooldownTime();

}
