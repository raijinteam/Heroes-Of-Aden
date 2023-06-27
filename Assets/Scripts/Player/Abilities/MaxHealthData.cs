using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthData : PowerUpHandler
{
	private float flt_MaxHealthIncreasePercent;

	private void Start()
	{
		SetData();
	}

	public override void LevelUp()
	{
		currentLevel += 1;
		SetData();
	}

	private void SetData()
	{
		flt_MaxHealthIncreasePercent = AbilityManager.Instance.maxHealthData.all_MaxHealthIncreasePercentage[currentLevel];
		//GameManager.Instance.player.health.IncreaseMaxHealth(flt_MaxHealthIncreasePercent);

		GameManager.Instance.player.UpdateMaxHealthForPlayer(flt_MaxHealthIncreasePercent);
	}

	public override void UpdateCooldownTime()
	{

	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.maxHealthData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.maxHealthData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.maxHealthData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		float flt_MaxHealthIncreasePercentNew = AbilityManager.Instance.maxHealthData.all_MaxHealthIncreasePercentage[currentLevel + 1];
		AbilityManager.Instance.HandleMaxHPPercentIncrease(_panelIndex, 0, flt_MaxHealthIncreasePercent, flt_MaxHealthIncreasePercentNew);
	}
}
