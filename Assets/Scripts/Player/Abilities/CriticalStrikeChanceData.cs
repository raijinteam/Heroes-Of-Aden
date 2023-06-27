using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalStrikeChanceData : PowerUpHandler
{

	private int criticalChancePercentage;

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
		criticalChancePercentage = AbilityManager.Instance.criticalStrikeChanceData.all_CriticalChancePercentage[currentLevel];

		//GameManager.Instance.player.criticalChancePercent = AbilityManager.Instance.criticalStrikeChanceData.all_CriticalChancePercentage[currentLevel];

		GameManager.Instance.player.UpdateCriticalChance(criticalChancePercentage);

	}

	public override void UpdateCooldownTime()
	{

	}
	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.criticalStrikeChanceData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.criticalStrikeChanceData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.criticalStrikeChanceData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		float criticalStrikeChanceNew = AbilityManager.Instance.criticalStrikeChanceData.all_CriticalChancePercentage[currentLevel + 1];
		AbilityManager.Instance.HandleCriticalChanceIncrease(_panelIndex, 0, GameManager.Instance.player.criticalChancePercent, 
															criticalStrikeChanceNew);
	}
}
