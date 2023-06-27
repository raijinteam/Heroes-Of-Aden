using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoNadeController : MonoBehaviour
{
	[SerializeField] private InfernoNadeDamage explosionAndFire;
	[SerializeField] private Transform body;
	private Vector3 scaleUpValue = new Vector3(3, 3, 3);
	private Vector3 scaleDownValue = new Vector3(1.5f, 1.5f, 1.5f);
	private int damageOverTime;


	[Header("Sound")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Explod;

	private float minXDistanceFromPlayer = 0.5f;
	private float maxXDistanceFromPlayer = 3f;
	private float minYDistanceFromPlayer = 0.5f;
	private float maxYDistanceFromPlayer = 3f;
	private Vector3 finalDestination;

	public void SetDamageOverTime(int _damage)
	{
		damageOverTime = _damage;
	}

	private void Start()
	{
		SetRandomDirection();
		StartCoroutine(MoveToTarget());
		StartCoroutine(IncreaseAndDecreaseSizeOverTime());
	}

	private void SetRandomDirection()
	{
		float randomXPos = Random.Range(minXDistanceFromPlayer, maxXDistanceFromPlayer);
		int randomSideIndex = Random.Range(0, 2);
		if(randomSideIndex == 1)
		{
			randomXPos = -randomXPos;
		}

		float randomYPos = Random.Range(minYDistanceFromPlayer, maxYDistanceFromPlayer);
		randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			randomYPos = -randomYPos;
		}

		finalDestination = new Vector3(transform.position.x + randomXPos, transform.position.y + randomYPos, transform.position.z);

		transform.LookAt(finalDestination);
	}

	private IEnumerator MoveToTarget()
	{
		float currentTime = 0f;
		float maxTimeToReachTarget = 1f;

		Vector3 startPosition = transform.position;

		while(currentTime < 1)
		{
			currentTime += Time.deltaTime / maxTimeToReachTarget;

			transform.position = Vector3.Lerp(startPosition, finalDestination, currentTime);
			yield return null;
		}
	}

	private IEnumerator IncreaseAndDecreaseSizeOverTime()
	{
		float currentTime = 0f;
		float maxTime = 0.5f;

		Vector3 startScale = body.localScale;
		Vector3 targetScale = scaleUpValue;

		while(currentTime < 1f)
		{
			currentTime += Time.deltaTime/ maxTime;

			body.localScale = Vector3.Lerp(startScale, targetScale, currentTime);
			yield return null;
		}

		currentTime = 0f;
		
		startScale = body.localScale;
		targetScale = scaleDownValue;

		while (currentTime < 1f)
		{
			currentTime += Time.deltaTime / maxTime;

			body.localScale = Vector3.Lerp(startScale, targetScale, currentTime);
			yield return null;
		}

		//body.gameObject.SetActive(false);
		//explosionAndFire.SetActive(true);
		//col_Box.enabled = true;

		InfernoNadeDamage fire = Instantiate(explosionAndFire, transform.position, explosionAndFire.transform.rotation);
		fire.SetDamageOverTime(damageOverTime);
		Destroy(this.gameObject);
	}
}
