using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform currentTarget;

    [Header("Normal Power")]
    [SerializeField] private PlayerNormalPower power;
    [SerializeField] private Transform normalPowerSpawnPoint;
    [SerializeField] private float maxFireRate;
    public float currentFireRate;
    public int normalPowerDamage;
    private float currentTimePassedForNormalPower = 0f;
    private bool shouldShootTowardsTarget = false;


    public void SetBaseData(float flt_PercentageDecrease)
	{
        maxFireRate = maxFireRate - (maxFireRate * (flt_PercentageDecrease / 100));
        currentFireRate = maxFireRate;
	}

	private void Update()
	{
        HandleFireRate();
       

		if (shouldShootTowardsTarget)
        {
            FindAndAssignTarget(); // find the closest enemy
           

            if(currentTarget != null)
			{
                // only shoot if there is an active target

                RotateShootingPointTowardsTarget();
                ShootPower();
            }

        }
     
    }

    private void HandleFireRate()
	{
        currentTimePassedForNormalPower += Time.deltaTime;
        if (currentTimePassedForNormalPower >= currentFireRate)
        {
            shouldShootTowardsTarget = true;
        }
    }


    private void RotateShootingPointTowardsTarget()
	{
     
        Vector3 directionToTarget = currentTarget.position - transform.position;

        // Calculate the angle in degrees for the rotation towards the target
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Apply the rotation to the child object
        normalPowerSpawnPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private void FindAndAssignTarget()
	{
        
        float currentClosestDistance = 0f;

        for(int i = 0; i < GameManager.Instance.list_ActiveEnemies.Count; i++)
		{
            float distanceWithPlayer = Vector3.Distance(transform.position, GameManager.Instance.list_ActiveEnemies[i].position);

            if(currentClosestDistance == 0)
			{
                currentClosestDistance = distanceWithPlayer;
                currentTarget = GameManager.Instance.list_ActiveEnemies[i];
			}
			else
			{
                if(currentClosestDistance > distanceWithPlayer)
				{
                    currentClosestDistance = distanceWithPlayer;
                    currentTarget = GameManager.Instance.list_ActiveEnemies[i];
				}
			}
		}
	}

    private void ShootPower()
	{

        // check for damage boost;
        int DamageToGive = (int)(normalPowerDamage + (normalPowerDamage * (GameManager.Instance.player.damageBoostPercent / 100)));
        

        // apply critical Chance
        int randomCritIndex = Random.Range(0, 100);

        if(randomCritIndex < GameManager.Instance.player.criticalChancePercent)
		{
            // crit
            DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
        }

        PlayerNormalPower pwr = Instantiate(power, normalPowerSpawnPoint.position, normalPowerSpawnPoint.rotation, GameManager.Instance.playerBulletSpawnParent);

        pwr.SetPowerData(DamageToGive);

        shouldShootTowardsTarget = false;
        currentTimePassedForNormalPower = 0f;

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //          Instantiate(power, normalPowerSpawnPoint.position, normalPowerSpawnPoint.rotation);
        //      }  
    }

    public void ReduceFireRate(float _fltPercentageValue)
	{
        currentFireRate = maxFireRate - ( maxFireRate * (_fltPercentageValue / 100));
	}
}
