using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{


	public bool isMagnetOn = false;
	[SerializeField] private float playerAttractSpeed = 10f;
	[SerializeField] private CircleCollider2D col;


	public void ActiveMagnet()
    {
		isMagnetOn = true;
    }

    private void Update()
    {
		if (isMagnetOn)
		{
			if (!GameManager.Instance.isGameRunning)
			{
				Destroy(this.gameObject);
			}

			// turn collider off
			col.enabled = false;
			// move towards player
			Vector2 playerPosition = GameManager.Instance.player.transform.position;
			transform.position = Vector2.MoveTowards(transform.position, playerPosition, playerAttractSpeed * Time.deltaTime);

			// check distance, if reached ( GameManager.Instance.PlayerCollectedPoint(pointsValue);
			//Destroy(this.gameObject);
			if (Vector2.Distance(transform.position , playerPosition) < 0.2f)
            {
				GameManager.Instance.CoinCollected();
				Destroy(gameObject);
            }
		}
	}

	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameManager.Instance.CoinCollected();
		Destroy(gameObject);
	}
}
