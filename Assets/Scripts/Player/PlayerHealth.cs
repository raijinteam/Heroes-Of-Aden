using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{


    [SerializeField] private float flt_CurrentHealth;
	public float flt_MaxHealth;
	[SerializeField] private GameObject playerDeathPartical;

	[Header("Sound")]
	[SerializeField] private AudioClip clip_PlayerDeath;

	[Header("Hp Regen")]
	private float hpAmountToRegen;
	private float timeToWaitBeforeRegening = 1f;
	private float currentTimeForRegenPassed = 0f;
	private bool isHPRegenActive = false;

	[Header("Health UI")]
	[SerializeField] private Slider slider_Health;


    private void OnEnable()
    {
		//Only For Testing
		slider_Health.maxValue = flt_MaxHealth;
		slider_Health.value = flt_MaxHealth;

		flt_CurrentHealth = flt_MaxHealth;
	}

    private void Start()
	{
		slider_Health.maxValue = flt_MaxHealth;
		slider_Health.value = flt_MaxHealth;

		flt_CurrentHealth = flt_MaxHealth;
	}

	private void Update()
	{
		if (isHPRegenActive)
		{
			HandleHPRegen();
		}
	}

	public void TakeDamage(int _damage)
	{
		flt_CurrentHealth -= _damage;

		MMTextSpawnerManager.Instance.SpawnAtTarget(transform, _damage);

		if (flt_CurrentHealth <= 0)
		{
			// player dead
			flt_CurrentHealth = 0f;
			//GameManager.Instance.isGameRunning = false;

			SoundManager.Instance.PlayPlayerDeathSound(clip_PlayerDeath);

			Instantiate(playerDeathPartical, transform.position, Quaternion.identity);

			GameManager.Instance.PlayerDied();
			//Destroy(gameObject);
			gameObject.SetActive(true);
		}

		slider_Health.value = flt_CurrentHealth;
	}

	public void AddHealth(float _amount)
	{
		flt_CurrentHealth += _amount;
		
		if(flt_CurrentHealth >= flt_MaxHealth)
		{
			flt_CurrentHealth = flt_MaxHealth;
		}

		slider_Health.value = flt_CurrentHealth;
	}

	public void PickedUpHealth()
	{
		float increaseAmount = 0.5f * flt_MaxHealth;
		SoundManager.Instance.PlayHealthPickupSound();
		AddHealth(increaseAmount);
	}

	private void HandleHPRegen()
	{
		currentTimeForRegenPassed += Time.deltaTime;
		if(currentTimeForRegenPassed >= timeToWaitBeforeRegening)
		{
			currentTimeForRegenPassed = 0f;

			AddHealth(hpAmountToRegen);
		}
	}

	public void IncreaseMaxHealth(float _percentValue)
	{
		flt_MaxHealth = flt_MaxHealth + (flt_MaxHealth * (_percentValue / 100));
		float oldMaxHealth = slider_Health.maxValue;
		slider_Health.maxValue = flt_MaxHealth;
		flt_CurrentHealth = (flt_MaxHealth * flt_CurrentHealth) / oldMaxHealth;
		slider_Health.value = flt_CurrentHealth;

		hpAmountToRegen = flt_MaxHealth * (_percentValue / 100);
	}

	public void SetHPRegen(float _percentValue)
	{
		isHPRegenActive = true;
		hpAmountToRegen = flt_MaxHealth * (_percentValue / 100);
	}
}
