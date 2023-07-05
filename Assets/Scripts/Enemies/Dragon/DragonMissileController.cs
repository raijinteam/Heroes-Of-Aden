using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMissileController : MonoBehaviour
{
	[SerializeField] private GameObject body;
	[SerializeField] private GameObject hitEffect;
	private bool hasCollided;

	private int damage;
	[SerializeField] private float flt_Speed;

	[Space]
	[Header("Sounds")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Exploid;

	private string tag_Player = "Player";

	public void SetData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		Destroy(gameObject, 8f);
		audioSource.PlayOneShot(clip_Shoot);
	}

	private void Update()
	{
		if (hasCollided)
		{
			return;
		}
		transform.Translate(transform.right * flt_Speed * Time.deltaTime, Space.World);

		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		// Determine the direction from the current position to the target
		Vector3 directionToTarget = GameManager.Instance.GetPlayerCurrentPosition() - transform.position;

		float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

		// Create the target rotation from the calculated angle
		Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

		// Interpolate from the current rotation towards the target rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (hasCollided)
		{
			return;
		}
		hasCollided = true;

		audioSource.PlayOneShot(clip_Exploid);

		if (collision.gameObject.tag.Equals(tag_Player))
		{
			GameManager.Instance.player.TakeDamageFromEnemy(damage);
		}

		body.SetActive(false);
		hitEffect.SetActive(true);
	}
}
