using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenieMissileController : MonoBehaviour
{
	[SerializeField] private GameObject body;
	[SerializeField] private GameObject hitEffect;
	private bool hasCollided;

    private int damage;
	[SerializeField] private float flt_Speed;

	private string tag_Player = "Player";

    public void SetData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		Destroy(gameObject, 8f);
	}

	private void Update()
	{
		if (hasCollided)
		{
			return;
		}
		transform.Translate(transform.right * flt_Speed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (hasCollided)
		{
			return;
		}
		hasCollided = true;

		if (collision.gameObject.tag.Equals(tag_Player))
		{
			GameManager.Instance.player.TakeDamageFromEnemy(damage);
		}

		body.SetActive(false);
		hitEffect.SetActive(true);
	}
}
