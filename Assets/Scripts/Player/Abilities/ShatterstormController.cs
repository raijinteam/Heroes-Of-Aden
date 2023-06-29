using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterstormController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col_Box;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float flt_Speed;
    [SerializeField] private int damage;
    [SerializeField] private int splashDamage;
	[SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip_ShootSound;
    [SerializeField] private AudioClip clip_ExplodSound;
 
    private bool hasCollided = false;
    private string tag_Enemy = "Enemy";


    private void OnEnable()
    {
        if (DataManager.Instance.IsSFXON() == true)
        {
            audioSource.PlayOneShot(clip_ShootSound);
        }
    }

	private void Start()
	{
        Destroy(gameObject, 7f);
	}

	public void SetData(int _damage, int _areaDamage)
	{
        damage = _damage;
        splashDamage = _areaDamage;
	}

	private void Update()
	{
		if (hasCollided)
		{
            return;
		}

        transform.Translate(transform.right * flt_Speed * Time.deltaTime, Space.World);

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        hasCollided = true;
        body.SetActive(false);
        explosion.SetActive(true);

        //PLAY EXPLOD SOUND
        if (DataManager.Instance.IsSFXON() == true)
        {
            audioSource.PlayOneShot(clip_ExplodSound);
        }

        if (collision.gameObject.tag.Equals(tag_Enemy))
		{
            collision.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
        }
   
        // do splash damage around the collision point
        SplashDamageAroundCollision();


        Destroy(this.gameObject,2f);      
	}

    private void SplashDamageAroundCollision()
	{
		// Perform the overlap circle
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

		// Check if a collision occurred
		foreach (Collider2D collider in colliders)
		{
            // Handle the overlap      
            if (collider.gameObject.tag.Equals(tag_Enemy))
            {
                collider.GetComponent<CollisionControllerEnemy>().TakeDamage(splashDamage);
            }
          
        }
	}
}
