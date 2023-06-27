using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetectorEnemy : MonoBehaviour
{
	[SerializeField] private ChasingEnemyHandler enemy;

	private string tag_Player = "Player";

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals(tag_Player))
		{
			enemy.PlayerInRange();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals(tag_Player))
		{
			enemy.PlayerOutOfRange();
		}
	}
}
