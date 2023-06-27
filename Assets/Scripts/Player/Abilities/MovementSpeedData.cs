using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedData : PowerUpHandler
{
    private float movementSpeedIncrease;


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
        movementSpeedIncrease = AbilityManager.Instance.movementSpeed.all_MovementSpeedIncreaseValue[currentLevel];

        GameManager.Instance.player.UpdateMovementSpeedForPlayer(movementSpeedIncrease);
    }

    public override void UpdateCooldownTime()
    {

    }

    public override string GetMyPowerName()
    {
        return AbilityManager.Instance.movementSpeed.powerUpName;
    }


    public override int GetMyCurrentLevel()
    {
        return currentLevel;
    }

    public override Sprite GetMyIcon()
    {
        return AbilityManager.Instance.movementSpeed.powerUpIcon;
    }

    public override string GetMyPowerInfo()
    {
        return AbilityManager.Instance.movementSpeed.powerUpInfo;
    }

    

    public override void SetUpdateInfoPanel(int _panelIndex)
    {
        float movementSpeedNew = AbilityManager.Instance.movementSpeed.all_MovementSpeedIncreaseValue[currentLevel + 1];

        AbilityManager.Instance.HandleMovementSpeedIncrease(_panelIndex, 0, GameManager.Instance.player.movement.flt_MoveSpeed, movementSpeedNew);
    }
}
