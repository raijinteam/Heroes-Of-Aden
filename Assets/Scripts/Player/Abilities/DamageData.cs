using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : PowerUpHandler
{

	private float damageIncreasePercentage;

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
		damageIncreasePercentage = AbilityManager.Instance.damageData.all_DamageIncreasePercentage[currentLevel];

		//GameManager.Instance.player.damageBoostPercent = AbilityManager.Instance.damageData.all_DamageIncreasePercentage[currentLevel];

		GameManager.Instance.player.UpdateDamageForPlayer(damageIncreasePercentage);
	}
	public override void UpdateCooldownTime()
    {

    }

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.damageData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.damageData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.damageData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		float damageBoostPercentNew = AbilityManager.Instance.damageData.all_DamageIncreasePercentage[currentLevel + 1];
		AbilityManager.Instance.HandleDamagePercentIncrease(_panelIndex, 0, GameManager.Instance.player.damageBoostPercent, damageBoostPercentNew);
	}
}
