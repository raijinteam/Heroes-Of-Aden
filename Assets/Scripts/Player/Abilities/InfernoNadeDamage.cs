using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoNadeDamage : MonoBehaviour
{
	[SerializeField] private BoxCollider2D col_Box;
	private int damageOverTime;
	private float destroyTimer = 8f;
	private float duration = 7f;

	

	private string tag_Enemy = "Enemy";


	public void SetDamageOverTime(int _damageOverTime)
	{
		damageOverTime = _damageOverTime;
	}

	private void Start()
	{
		Destroy(this.gameObject, destroyTimer);
		Invoke("DisableColliderBeforeDestoying", duration);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().AffectedByInfernoFlames(damageOverTime);
		}
		//collision.GetComponent<CollisionControllerEnemy>().AffectedByInfernoFlames(damageOverTime);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().NoLongerStandingOnInfernoFlames();
		}
		//collision.GetComponent<CollisionControllerEnemy>().NoLongerStandingOnInfernoFlames();
	}

	private void DisableColliderBeforeDestoying()
	{
		col_Box.enabled = false;
	}


}
