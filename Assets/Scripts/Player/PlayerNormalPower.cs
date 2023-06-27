using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalPower : MonoBehaviour
{
	[SerializeField] private float flt_MoveSpeed;
	[SerializeField] private int damage;

	private string tag_Enemy = "Enemy";

	private void Start()
	{
		Destroy(gameObject, 10f);
	}

	private void Update()
	{
		transform.Translate(transform.right * flt_MoveSpeed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
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
