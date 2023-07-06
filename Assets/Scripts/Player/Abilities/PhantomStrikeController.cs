using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomStrikeController : MonoBehaviour
{
    [SerializeField] private GameObject body;
	[SerializeField] private BoxCollider2D col_Phantom;
    [SerializeField] private float flt_Speed;
    private int damage;

	[Header("Sounds")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Shoot;
	[SerializeField] private AudioClip clip_Explod;


	private bool hasReachedEnd = false;
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private GameObject hitEffect;

	private string tag_Enemy = "Enemy";


    private void OnEnable()
    {
		if (ServiceManager.Instance.dataManager.IsSFXON() == true)
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
		Invoke("StopMotion", 8f);
	}

	private void Update()
	{
		if (hasReachedEnd)
		{
			return;
		}

		transform.Translate(transform.right * flt_Speed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (ServiceManager.Instance.dataManager.IsSFXON())
		{
			audioSource.PlayOneShot(clip_Explod);
		}


		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
		}
		//collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
		Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
	}

	private void StopMotion()
	{
		hasReachedEnd = true;
		col_Phantom.enabled = false;

		body.SetActive(false);
		explosionEffect.SetActive(true);

		Destroy(gameObject, 2f);
	}
}
