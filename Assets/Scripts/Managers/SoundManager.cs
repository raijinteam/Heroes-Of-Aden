using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //public static SoundManager Instance;
    [SerializeField] private AudioSource bgSound;
    [SerializeField] private AudioSource magnetPickupSound;
    [SerializeField] private AudioClip clip_MagnetCollect;
    [SerializeField] private AudioSource coinPickUp;
    [SerializeField] private AudioClip clip_CoinCollect;
    [SerializeField] private AudioSource healthPickUp;
    [SerializeField] private AudioClip clip_HealthCollect;
    [SerializeField] private AudioSource enemyDeath;
    [SerializeField] private AudioSource playerDeath;
    [SerializeField] private AudioSource pointCollectSound;
    [SerializeField] private AudioClip clip_PointCollect;
    [SerializeField] private AudioSource playerShootingSound;

    //private void Awake()
    //{
    //    if(Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}

    public void PlayBGSound()
    {
        if (ServiceManager.Instance.dataManager.isMusicOn)
        {
            bgSound.Play();
        }
    }

    public void PlayPlayerShootingSound(AudioClip _clip)
    {
        if(ServiceManager.Instance.dataManager.isSFXOn)
        {
            playerShootingSound.PlayOneShot(_clip);
        }
    }

    public void PlayPlayerDeathSound(AudioClip _clip)
    {
        if(ServiceManager.Instance.dataManager.isSFXOn)
        {
            playerDeath.PlayOneShot(_clip);
        }
    }

    public void PlayEnemyDeathSound(AudioClip _clip)
    {
        if (ServiceManager.Instance.dataManager.isSFXOn)
        {
            enemyDeath.PlayOneShot(_clip);
        }
    }


    public void PlayCoinPickupSound()
    {
        if (ServiceManager.Instance.dataManager.isSFXOn)
        {
            coinPickUp.PlayOneShot(clip_CoinCollect);
        }
    }

    public void PlayPointCollectSound()
    {
        if (ServiceManager.Instance.dataManager.isSFXOn)
        {
            //Debug.Log("Sound Play");
            pointCollectSound.PlayOneShot(clip_PointCollect);
        }
    }

    public void PlayMagnetPickupSound()
    {
        if (ServiceManager.Instance.dataManager.isSFXOn)
        {
            magnetPickupSound.PlayOneShot(clip_MagnetCollect);
        }
    }

    public void PlayHealthPickupSound()
    {
        if (ServiceManager.Instance.dataManager.IsSFXON())
        {
            healthPickUp.PlayOneShot(clip_HealthCollect);
        }
    }

}
