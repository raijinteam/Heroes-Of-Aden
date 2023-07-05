using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
	[SerializeField] private float flt_MoveSpeed;
	[SerializeField] private int damage;
	[SerializeField] private GameObject body;
	[SerializeField] private GameObject hitEffect;

	[Space]
	[Header("Sounds")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Exploid;

	private bool hasCollided = false;

	public void SetPowerData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		audioSource.PlayOneShot(clip_Shoot);
		Destroy(gameObject, 8f);
	}

	private void Update()
	{
		if (hasCollided)
		{
			return;
		}
		transform.Translate(transform.right * flt_MoveSpeed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (hasCollided)
		{
			return;
		}

		hasCollided = true;
		body.SetActive(false);
		hitEffect.SetActive(true);

		audioSource.PlayOneShot(clip_Exploid);

		GameManager.Instance.player.TakeDamageFromEnemy(damage);
	}

}
