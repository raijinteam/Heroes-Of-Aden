using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource magnetPickupSound;
    [SerializeField] private AudioSource coinPickUp;
    [SerializeField] private AudioSource healthPickUp;
    [SerializeField] private AudioSource enemyDeath;
    [SerializeField] private AudioSource playerDeath;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayPlayerDeathSound(AudioClip _clip)
    {
        if(DataManager.Instance.IsSFXON() == true)
        {
            playerDeath.PlayOneShot(_clip);
        }
    }

    public void PlayEnemyDeathSound(AudioClip _clip)
    {
        if (DataManager.Instance.IsSFXON() == true)
        {
            enemyDeath.PlayOneShot(_clip);
        }
    }


    public void PlayCoinPickupSound()
    {
        if (DataManager.Instance.IsSFXON() == true)
        {
            coinPickUp.Play();
        }
    }

    public void PlayMagnetPickupSound()
    {
        if (DataManager.Instance.IsSFXON() == true)
        {
            magnetPickupSound.Play();
        }
    }

    public void PlayHealthPickupSound()
    {
        if (DataManager.Instance.IsSFXON() == true)
        {
            healthPickUp.Play();
        }
    }

}
