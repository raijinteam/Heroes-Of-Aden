using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCooldownData : PowerUpHandler
{

	private float cooldownReductionPercentage;

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
		cooldownReductionPercentage = AbilityManager.Instance.powerupCooldownData.all_CooldownReductionPercentage[currentLevel];

		//GameManager.Instance.player.UpdateCooldownReductionPercentageValue(AbilityManager.Instance.powerupCooldownData.all_CooldownReductionPercentage[currentLevel]);

		GameManager.Instance.player.UpdateCooldownReductionPercentageValue(cooldownReductionPercentage);
	}

	public override void UpdateCooldownTime()
	{

	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.powerupCooldownData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.powerupCooldownData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.powerupCooldownData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		float powerUpCooldownNew = AbilityManager.Instance.powerupCooldownData.all_CooldownReductionPercentage[currentLevel + 1];
		AbilityManager.Instance.HandlePowerupCooldownIncrease(_panelIndex, 0, GameManager.Instance.player.cooldownReductionPercentage,
															powerUpCooldownNew);
	}
}
