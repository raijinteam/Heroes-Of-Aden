using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalPower : MonoBehaviour
{
	[SerializeField] private float flt_MoveSpeed;
	[SerializeField] private int damage;

	[Header("Sound")]
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Destory;

	private string tag_Enemy = "Enemy";

	private void Start()
	{
		SoundManager.Instance.PlayPlayerShootingSound(clip_Shoot);
		Destroy(gameObject, 10f);
	}

	private void Update()
	{
		transform.Translate(transform.right * flt_MoveSpeed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		SoundManager.Instance.PlayPlayerShootingSound(clip_Destory);
		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
			Destroy(gameObject);
		}

	}

	public void SetPowerData(int _damage)
	{
		damage = _damage;
	}
}
