using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TickingTerrorController : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private Transform bombBody;
    [SerializeField] private GameObject explosionEffect;

    private Vector3 finalBodyScaleValue = new Vector3(2.2f, 2.2f, 2.2f);
    private float doTweenDuration = 0.3f;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip_Shoot;
    [SerializeField] private AudioClip clip_Explod;


    private int damage;
    private float timeBeforeExplode = 4f;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask enemyLayer;


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
        Invoke("Explode", timeBeforeExplode);
        bombBody.DOScale(finalBodyScaleValue, doTweenDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}

    private void Explode()
	{
        body.gameObject.SetActive(false);
        explosionEffect.SetActive(true);

        if (DataManager.Instance.IsSFXON() == true)
        {
            audioSource.PlayOneShot(clip_Explod);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        // Check if a collision occurred
        foreach (Collider2D collider in colliders)
        {
            // Handle the overlap
            collider.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
        }


        Destroy(gameObject, 2f);
    }
}
