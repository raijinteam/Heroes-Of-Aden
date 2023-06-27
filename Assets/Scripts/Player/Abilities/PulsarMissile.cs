using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsarMissile : MonoBehaviour
{
	[SerializeField] private GameObject body;
	[SerializeField] private GameObject explosionEffect;

	[Header("Sound")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Explod;

	private bool hasCollided = false;

	[SerializeField] private float flt_MoveSpeed;
	private int damage;
	//private Vector3 colPosition;

	private string tag_Enemy = "Enemy";


    private void OnEnable()
    {
		if (DataManager.Instance.IsSFXON() == true)
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
			//transform.position = colPosition;
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

		if (DataManager.Instance.IsSFXON() == true)
		{
			audioSource.PlayOneShot(clip_Explod);
		}

		//colPosition = transform.position;
		hasCollided = true;
		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
		}
		//collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);

		body.SetActive(false);
		explosionEffect.SetActive(true);

		Destroy(gameObject, 2f);
	}
}
