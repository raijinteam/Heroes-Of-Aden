using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelDanceController : MonoBehaviour
{
	[SerializeField] private GameObject swordHitEffect;
	[SerializeField] private Transform body;
    [SerializeField] private float flt_Speed;
    [SerializeField] private float flt_RotateSpeed;
    private int damage;

	private Transform originalParent;
	private bool shouldReturnToPlayer = false;

	private Vector3 startPosition;
	[SerializeField] private float flt_MaxDistance;

	private string tag_Enemy = "Enemy";
	public void SetData(int _damage, Transform _myParent)
	{
		Debug.Log("target name : " + _myParent.name);
        damage = _damage;
		originalParent = _myParent;
		startPosition = transform.position;
	}

	private void Update()
	{
		body.Rotate(-Vector3.right * flt_RotateSpeed * Time.deltaTime);

		if(originalParent == null)
		{
			Destroy(gameObject);
		}

		if (!shouldReturnToPlayer)
		{
			// is moving forward currently
			transform.Translate(transform.right * flt_Speed * Time.deltaTime, Space.World);

			float distanceCovered = Vector3.Distance(transform.position, startPosition);

			if (distanceCovered >= flt_MaxDistance)
			{
				// reached its maximum distance
				shouldReturnToPlayer = true;
			}
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, originalParent.position, Time.deltaTime * flt_Speed * 1.5f);

			float gapLeft = Vector3.Distance(transform.position, originalParent.position);

			if (gapLeft < 0.2f)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
		}
		//collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
		Instantiate(swordHitEffect, transform.position, swordHitEffect.transform.rotation);
	}
}
