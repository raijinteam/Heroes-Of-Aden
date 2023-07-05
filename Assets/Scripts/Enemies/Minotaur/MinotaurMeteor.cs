using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMeteor : MonoBehaviour
{
	private int damage;
	[SerializeField] private GameObject missile;
	[SerializeField] private Transform missileBody;
	[SerializeField] private GameObject explosion;
	[SerializeField] private float radius;
	[SerializeField] private LayerMask playerLayer;

	[Space]
	[Header("Sounds")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Exploid;

	public void SetData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		audioSource.PlayOneShot(clip_Shoot);
		StartCoroutine(MissileLanding());
	}

	private IEnumerator MissileLanding()
	{
		yield return new WaitForSeconds(0.5f);
		missileBody.gameObject.SetActive(true);

		float currentTime = 0f;
		float maxTime = 0.8f;

		Vector3 currentPosition = missileBody.position;
		Vector3 finalPosition = missile.transform.position;

		Vector3 currentScale = missileBody.localScale;
		Vector3 finalScale = new Vector3(2, 2, 2);

		while (currentTime < 1f)
		{
			currentTime += Time.deltaTime/ maxTime;

			missileBody.position = Vector3.Lerp(currentPosition, finalPosition, currentTime);
			missileBody.localScale = Vector3.Lerp(currentScale, finalScale, currentTime);
			yield return null;
		}

		missile.SetActive(false);
		explosion.SetActive(true);

		Collider2D colliders = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

		if (colliders != null)
		{
			GameManager.Instance.player.TakeDamageFromEnemy(damage);
		}
		audioSource.PlayOneShot(clip_Exploid);
		Destroy(gameObject, 2f);
	}
}
