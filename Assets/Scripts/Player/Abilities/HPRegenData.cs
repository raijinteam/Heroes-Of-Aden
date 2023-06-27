using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRegenData : PowerUpHandler
{

	private float hpRegenPercentValue;


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
		hpRegenPercentValue = AbilityManager.Instance.hpRegenData.all_HPRegenOverTimePercentage[currentLevel];
		GameManager.Instance.player.health.SetHPRegen(hpRegenPercentValue);
	}

	public override void UpdateCooldownTime()
	{

	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.hpRegenData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.hpRegenData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.hpRegenData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		float hpRegenPercentValueNew = AbilityManager.Instance.hpRegenData.all_HPRegenOverTimePercentage[currentLevel + 1];
		AbilityManager.Instance.HandleRegenPercentIncrease(_panelIndex, 0, hpRegenPercentValue, hpRegenPercentValueNew);
	}
}
