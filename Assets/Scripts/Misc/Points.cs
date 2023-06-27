using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Points : MonoBehaviour
{
	public float pointsValue;

	private bool isAlreadyCollected = false;
	private float offSet = 1.2f;
	[SerializeField] private float playerAttractSpeed = 10f;
	[SerializeField] private CircleCollider2D col;
	public bool isMagnetOn = false;


	public void ActiveMagnet( )
    {
		isMagnetOn = true;
		Debug.Log("State of magnet :" + isMagnetOn);
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
			if(Vector2.Distance(transform.position , playerPosition) < 0.2f)
            {
				GameManager.Instance.PlayerCollectedPoint(pointsValue);
				Destroy(this.gameObject);
			}
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isAlreadyCollected)
		{
			return;
		}

		isAlreadyCollected = true;
		//DoCollectAnimation(collision.transform);
		StartCoroutine(CollectPointAnimation(collision.transform));
	}

	//private void DoCollectAnimation(Transform  _player)
	//{
	//	Vector3 finalDirection = (transform.position - _player.position).normalized;
	//	finalDirection = (transform.position + finalDirection) *offSet;

	//	Sequence seq = DOTween.Sequence();

	//	seq.Append(transform.DOMove(finalDirection, 1f)).Append(transform.DOMove(_player.position, 1f));
	//}

	private IEnumerator CollectPointAnimation(Transform _player)
	{
		float currentTime = 0f;
		float maxTime = 0.3f;

		Vector3 startPosition = transform.position;
		Vector3 finalDirection = transform.position + (transform.position - _player.position).normalized * offSet;

		while(currentTime < 1)
		{
			currentTime += Time.deltaTime / maxTime;

			transform.position = Vector3.Lerp(startPosition, finalDirection, currentTime);
			yield return null;
		}

		startPosition = finalDirection;
		currentTime = 0f;

		while(currentTime < 1)
		{
		
			currentTime += Time.deltaTime / maxTime;

			transform.position = Vector3.Lerp(startPosition, _player.position, currentTime);
			yield return null;
		}

		GameManager.Instance.PlayerCollectedPoint(pointsValue);
		Destroy(this.gameObject);
	}
}
