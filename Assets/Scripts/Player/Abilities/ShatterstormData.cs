using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterstormData : PowerUpHandler
{
   
    [SerializeField] private ShatterstormController shatterstorm;
    [SerializeField] private float fireRate;
    private float currentTimePassed = 0f;
    [SerializeField] private int numberOfBullets;
    [SerializeField] private int damage;
    [SerializeField] private int areaDamage;
    [SerializeField] private Vector3[] all_Targets;

	private void Start()
	{
        SetData();

        currentTimePassed = fireRate;
    }

	private void Update()
	{
        currentTimePassed += Time.deltaTime;

        if(currentTimePassed >= fireRate)
		{
            currentTimePassed = 0f;
            AssignRandomTargets();     
            StartCoroutine(ShootShatterStormsInInterval());
		}
	}

	private void OnDisable()
	{
        StopAllCoroutines();
	}

    public override void UpdateCooldownTime()
	{
        fireRate = fireRate - (fireRate * (GameManager.Instance.player.cooldownReductionPercentage / 100)); 
	}

    public override void LevelUp()
	{
        currentLevel += 1;
        SetData();
    }

    private void SetData()
	{
       
        damage = AbilityManager.Instance.shatterStormData.all_DamageValues[currentLevel];
        areaDamage = AbilityManager.Instance.shatterStormData.all_SplashDamageValues[currentLevel];
        numberOfBullets = AbilityManager.Instance.shatterStormData.all_TotalMissileCount[currentLevel];
        fireRate = AbilityManager.Instance.shatterStormData.all_SpawnRate[currentLevel];

        UpdateCooldownTime();

        all_Targets = new Vector3[numberOfBullets];
    }



    private void AssignRandomTargets()
	{
        if (GameManager.Instance.list_ActiveEnemies.Count == 0)
        {
            return; // no enemies found

        }
        for (int i = 0; i < all_Targets.Length; i++)
		{
            int randomTargetIndex = Random.Range(0, GameManager.Instance.list_ActiveEnemies.Count);
            all_Targets[i] = GameManager.Instance.list_ActiveEnemies[randomTargetIndex].position;
		}
	}

    private IEnumerator ShootShatterStormsInInterval()
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

            ShatterstormController shatter = Instantiate(shatterstorm, transform.position, transform.rotation , GameManager.Instance.playerBulletSpawnParent);          

            // check for damage boost;
            int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
            // apply critical Chance
            int randomCritIndex = Random.Range(0, 100);

            if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
            {
                // crit
                DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
            }

            shatter.SetData(DamageToGive, areaDamage);
            yield return new WaitForSeconds(0.1f);
        }
	}

	public override string GetMyPowerName()
	{
        return AbilityManager.Instance.shatterStormData.powerUpName;
	}

    public override int GetMyCurrentLevel()
    {
        return currentLevel;
    }

	public override Sprite GetMyIcon()
	{
        return AbilityManager.Instance.shatterStormData.powerUpIcon;
    }

	public override string GetMyPowerInfo()
	{
        return AbilityManager.Instance.shatterStormData.powerUpInfo;
    }

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
        int count = 0;
      
        int damageNew = AbilityManager.Instance.shatterStormData.all_DamageValues[currentLevel + 1];
        int damageOld = AbilityManager.Instance.shatterStormData.all_DamageValues[currentLevel];
        int numberOfBulletsNew = AbilityManager.Instance.shatterStormData.all_TotalMissileCount[currentLevel + 1];
        float fireRateNew = AbilityManager.Instance.shatterStormData.all_SpawnRate[currentLevel + 1];
        float fireRateOld = AbilityManager.Instance.shatterStormData.all_SpawnRate[currentLevel];

        if(damageOld != damageNew)
		{
            AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damageOld, damageNew);
            count += 1;
		}

        if (numberOfBullets != numberOfBulletsNew)
        {
            AbilityManager.Instance.HandleProjectileIncrease(_panelIndex, count, numberOfBullets, numberOfBulletsNew);
            count += 1;
        }

        if (fireRateOld != fireRateNew)
        {
            AbilityManager.Instance.HandleFireRateDecrease(_panelIndex, count, fireRateOld, fireRateNew);
            // count += 1;
        }

		
    }
}
