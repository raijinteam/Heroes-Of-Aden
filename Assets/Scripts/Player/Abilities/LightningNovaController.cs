using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningNovaController : MonoBehaviour
{
    private int damage;

    [SerializeField] private ParticleSystem ps_NovaEffect;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    public void SetData(int _damage)
	{
        damage = _damage;
        gameObject.SetActive(true);

        DoDamageInRadius();
    }

    private void DoDamageInRadius()
	{
        ps_NovaEffect.Play();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        // Check if a collision occurred
        foreach (Collider2D collider in colliders)
        {
            // Handle the overlap
            collider.GetComponent<CollisionControllerEnemy>().TakeDamage(damage);
        }       
    }

  

}
