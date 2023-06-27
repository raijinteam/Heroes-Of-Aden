using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomStrikeData : PowerUpHandler
{
    [SerializeField] private PhantomStrikeController phantom;
    private int damage;
    private int countOfBullets;
    [SerializeField] private float fireRate;
    private float currentTimePassed = 0f;
    [SerializeField] private Vector3[] all_Targets;


	private void Start()
	{
        SetData();
        currentTimePassed = fireRate;
    }

	private void Update()
	{
        currentTimePassed += Time.deltaTime;

        if (currentTimePassed >= fireRate)
		{
            currentTimePassed = 0f;
            AssignRandomTargets();
            StartCoroutine(ShootPhantomStikeTowardsRandomEnemies());
		}
    }

    public override void UpdateCooldownTime()
    {
        fireRate = fireRate - (fireRate * (GameManager.Instance.player.cooldownReductionPercentage / 100));
    }

    private void SetData()
	{
        damage = AbilityManager.Instance.phantomStrikeData.all_Damage[currentLevel];
        countOfBullets = AbilityManager.Instance.phantomStrikeData.all_Count[currentLevel];
        fireRate = AbilityManager.Instance.phantomStrikeData.all_Firerate[currentLevel];
        UpdateCooldownTime();

        all_Targets = new Vector3[countOfBullets];
        
	}

    private void AssignRandomTargets()
	{
        if(GameManager.Instance.list_ActiveEnemies.Count == 0)
		{
            return; // no enemies found
		}

        for (int i = 0; i < all_Targets.Length; i++)
        {
            int randomTargetIndex = Random.Range(0, GameManager.Instance.list_ActiveEnemies.Count);
            all_Targets[i] = GameManager.Instance.list_ActiveEnemies[randomTargetIndex].position;
        }
    }

    public override void LevelUp()
    {
        currentLevel += 1;
        SetData();
    }

    private IEnumerator ShootPhantomStikeTowardsRandomEnemies()
	{
        for (int i = 0; i < all_Targets.Length; i++)
        {
            if(all_Targets[i] == null)
			{
                continue;
			}

            Vector3 directionToTarget = all_Targets[i] - transform.position;

            // Calculate the angle in degrees for the rotation towards the target
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Apply the rotation to the object
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            PhantomStrikeController phntm = Instantiate(phantom, transform.position, transform.rotation);

            int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
            // apply critical Chance
            int randomCritIndex = Random.Range(0, 100);

            if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
            {
                // crit
                DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
            }
            phntm.SetData(DamageToGive);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override string GetMyPowerName()
    {
        return AbilityManager.Instance.phantomStrikeData.powerUpName;
    }

    public override int GetMyCurrentLevel()
    {
        return currentLevel;
    }

    public override Sprite GetMyIcon()
    {
        return AbilityManager.Instance.phantomStrikeData.powerUpIcon;
    }

    public override string GetMyPowerInfo()
    {
        return AbilityManager.Instance.phantomStrikeData.powerUpInfo;
    }

    public override void SetUpdateInfoPanel(int _panelIndex)
	{
        int count = 0;

        int damageNew = AbilityManager.Instance.phantomStrikeData.all_Damage[currentLevel + 1];
        int countOfBulletsNew = AbilityManager.Instance.phantomStrikeData.all_Count[currentLevel + 1];
        float fireRateNew = AbilityManager.Instance.phantomStrikeData.all_Firerate[currentLevel + 1];
        float fireRateOld = AbilityManager.Instance.phantomStrikeData.all_Firerate[currentLevel];

        if (damage != damageNew)
        {
            AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damage, damageNew);
            count += 1;
        }

        if (countOfBullets != countOfBulletsNew)
        {
            AbilityManager.Instance.HandleProjectileIncrease(_panelIndex, count, countOfBullets, countOfBulletsNew);
            count += 1;
        }

        if (fireRateOld != fireRateNew)
        {
            AbilityManager.Instance.HandleFireRateDecrease(_panelIndex, count, fireRateOld, fireRateNew);
            // count += 1;
        }
    }
}
