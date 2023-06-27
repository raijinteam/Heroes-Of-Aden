using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateData : PowerUpHandler
{
	
	private float fireRateDecreasePercent;

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
		fireRateDecreasePercent = AbilityManager.Instance.fireRateData.all_FireRateDecreasePercentageValue[currentLevel];
		//GameManager.Instance.player.shooting.ReduceFireRate(fireRateDecreasePercent);
		GameManager.Instance.player.UpdateFireRateForPlayer(fireRateDecreasePercent);
	}

	public override void UpdateCooldownTime()
	{

	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.fireRateData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.fireRateData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.fireRateData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		float fireRateDecreasePercentNew = AbilityManager.Instance.fireRateData.all_FireRateDecreasePercentageValue[currentLevel + 1];
		AbilityManager.Instance.HandleFireRatePercentIncrease(_panelIndex, 0, fireRateDecreasePercent, fireRateDecreasePercentNew);
	
	}
}
