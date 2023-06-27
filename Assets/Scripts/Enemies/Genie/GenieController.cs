using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenieController : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private GenieMissileController missile; 
    [SerializeField] private Transform all_SpawnPointParent;
    [SerializeField] private Transform[] all_SpawnPoints;
    [SerializeField] private float flt_FireRate;
    private float currentTimePassed = 0f;
    [SerializeField] private int damage;

    private Vector3 leftSideRotationValues = new Vector3(0, 0, 0);
    private Vector3 rightSideRotationValues = new Vector3(0, 180, 0);

    private bool isAttacking = false;

    [SerializeField] private Animator anim;
    private string str_AttackAnim = "Attack";
    private string str_IdleAnim = "Idle";

    public void SetData(int _damage)
	{
        damage = _damage;
	}

	private void Update()
	{
		if (!GameManager.Instance.isGameRunning)
		{
            return;
		}

        FacePlayer();
        HandleAttacking();
      
	}

    private void FacePlayer()
    {
        if (transform.position.x < GameManager.Instance.player.transform.position.x)
        {
            spriteTransform.localEulerAngles = leftSideRotationValues;
        }
        else
        {
            spriteTransform.localEulerAngles = rightSideRotationValues;
        }
    }

    private void HandleAttacking()
    {
        if (isAttacking)
        {
            return;
        }

        currentTimePassed += Time.deltaTime;

        if (currentTimePassed >= flt_FireRate)
        {
            currentTimePassed = 0f;

            int attackIndex = Random.Range(0, 2);

          
            if(attackIndex == 0)
			{
                StartCoroutine(ShootMissileTowardsPlayerInIntervals());
            }
			else
			{
                StartCoroutine(ShootAllMissilesTogether());
			}
            
        }
    }

    private IEnumerator ShootMissileTowardsPlayerInIntervals()
	{
        isAttacking = true;

        for(int i = 0; i < 5; i++)
		{
            anim.SetTrigger(str_AttackAnim);
            yield return new WaitForSeconds(0.2f);
            Vector3 directionToTarget = GameManager.Instance.GetPlayerCurrentPosition() - transform.position;
            // Calculate the angle in degrees for the rotation towards the target
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Apply the rotation to the child object
            all_SpawnPointParent.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            GenieMissileController mis = Instantiate(missile, all_SpawnPointParent.position, all_SpawnPointParent.rotation);
            mis.SetData(damage);
            yield return new WaitForSeconds(0.4f);
            anim.SetTrigger(str_IdleAnim);
        }

        anim.SetTrigger(str_IdleAnim);
        isAttacking = false;
	}

    private IEnumerator ShootAllMissilesTogether()
	{
        isAttacking = true;

        Vector3 directionToTarget = GameManager.Instance.GetPlayerCurrentPosition() - transform.position;
        // Calculate the angle in degrees for the rotation towards the target
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        // Apply the rotation to the child object
        all_SpawnPointParent.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        anim.SetTrigger(str_AttackAnim);
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < all_SpawnPoints.Length; i++)
		{         
            GenieMissileController mis = Instantiate(missile, all_SpawnPoints[i].position, all_SpawnPoints[i].rotation);
            mis.SetData(damage);
        }
        anim.SetTrigger(str_IdleAnim);

        yield return new WaitForSeconds(1f);
        anim.SetTrigger(str_AttackAnim);

        directionToTarget = GameManager.Instance.GetPlayerCurrentPosition() - transform.position;
        // Calculate the angle in degrees for the rotation towards the target
        angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        // Apply the rotation to the child object
        all_SpawnPointParent.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        for (int i = 0; i < all_SpawnPoints.Length; i++)
        {
            GenieMissileController mis = Instantiate(missile, all_SpawnPoints[i].position, all_SpawnPoints[i].rotation);
            mis.SetData(damage);
        }

        anim.SetTrigger(str_IdleAnim);
        isAttacking = false;
    }
}
