using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTrigger : MonoBehaviour
{
	[SerializeField] private DemonController demon;
	private string tag_player = "Player";

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!demon.isFlyAttack)
		{
			return;
		}

		if (collision.gameObject.tag.Equals(tag_player))
		{

			GameManager.Instance.player.TakeDamageFromEnemy(demon.flt_FlyDamage);
		}
	}
}
