using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamageData : PowerUpHandler
{

	private float criticalDamagePercentage;

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
		criticalDamagePercentage = AbilityManager.Instance.criticalDamageData.all_CriticalDamageBoostPercentage[currentLevel];

		//GameManager.Instance.player.criticalDamagePercent = AbilityManager.Instance.criticalDamageData.all_CriticalDamageBoostPercentage[currentLevel];

		GameManager.Instance.player.UpdateCriticalDamageForPlayer((int)criticalDamagePercentage);
	}

	public override void UpdateCooldownTime()
	{

	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.criticalDamageData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.criticalDamageData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.criticalDamageData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		float criticalStrikeDamageNew = AbilityManager.Instance.criticalDamageData.all_CriticalDamageBoostPercentage[currentLevel + 1];
		AbilityManager.Instance.HandleCriticalDamageIncrease(_panelIndex, 0, GameManager.Instance.player.criticalDamagePercent,
															criticalStrikeDamageNew);
	}
}
