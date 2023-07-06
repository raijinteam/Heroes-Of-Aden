using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimsonMissileController : MonoBehaviour
{
	[SerializeField] private GameObject body;
	[SerializeField] private GameObject explosion;

	[Header("Sounds")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Explod;

	private int damage;
	[SerializeField] private float flt_MoveSpeed;
	private bool hasCollided = false;

	private string tag_Enemy = "Enemy";



	private void OnEnable()
	{
		if (ServiceManager.Instance.dataManager.IsSFXON())
		{
			audioSource.PlayOneShot(clip_Shoot);
		}
	}


	public void SetData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		Destroy(gameObject, 10f);
	}

	private void Update()
	{
	

		if (hasCollided)
		{
			return;
		}

		transform.Translate(transform.right * flt_MoveSpeed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (hasCollided)
		{
			return;
		}


		hasCollided = true;

        if (ServiceManager.Instance.dataManager.IsSFXON() == true)
        {
			audioSource.PlayOneShot(clip_Explod);
        }


		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
		}
		
		body.SetActive(false);
		explosion.SetActive(true);

		Destroy(gameObject, 2f);
	}
}
