using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
	[SerializeField] private GameObject body;
	[SerializeField] private Transform thrusterBody;
	[SerializeField] private GameObject explosionEffect;
	private Vector3 finalScale = new Vector3(0.75f, 0.75f, 0.75f);

	[Header("Sounds")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Explod;


	private int damage;
	[SerializeField] private float radius;
	[SerializeField] private LayerMask enemyLayer;


    private void OnEnable()
    {
		if (DataManager.Instance.IsSFXON() == true)
		{
			audioSource.PlayOneShot(clip_Shoot);
		}
	}


    public void SetData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		StartCoroutine(MoveToTargetPosition());
	}

	private IEnumerator MoveToTargetPosition()
	{
		yield return new WaitForSeconds(1f);
		thrusterBody.gameObject.SetActive(true);

		float currentTime = 0f;
		float maxTime = 1.5f;

		Vector3 startPosition = thrusterBody.localPosition;
		Vector3 startScale = thrusterBody.localScale;

		while (currentTime < 1f)
		{
			currentTime += Time.deltaTime / maxTime;

			thrusterBody.localPosition = Vector3.Lerp(startPosition, Vector3.zero, currentTime);
			thrusterBody.localScale = Vector3.Lerp(startScale, finalScale, currentTime);
			yield return null;
		}

		body.SetActive(false);
		explosionEffect.SetActive(true);

		//PLAY EXPLOD SOUND
		if (DataManager.Instance.IsSFXON() == true)
		{
			audioSource.PlayOneShot(clip_Explod);
		}

		DamagePlayerCaughtInThruster();
		Destroy(gameObject, 2f);
	}

	private void DamagePlayerCaughtInThruster()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

		// Check if a collision occurred
		foreach (Collider2D collider in colliders)
		{
			// Handle the overlap
			collider.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
		}
	}
}
