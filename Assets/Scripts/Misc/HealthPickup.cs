using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameManager.Instance.player.health.PickedUpHealth();
		Destroy(gameObject);
	}
}
