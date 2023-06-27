using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingUI : MonoBehaviour
{
    public bool isMusicOn;
    public Image img_MusicTick;
    public bool isSFXOn;
    public Image img_SFXTick;


    private void OnEnable()
    {
        CheckStateOfMusic();
        CheckStateOfSFX();
    }

    public void CheckStateOfMusic()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_MUSIC) == 1)
        {
            isMusicOn = true;
            img_MusicTick.gameObject.SetActive(true);
        }
        else
        {
            isMusicOn = false;
            img_MusicTick.gameObject.SetActive(false);
        }
    }

    public void CheckStateOfSFX()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX) == 1)
        {
            isSFXOn = true;
            img_SFXTick.gameObject.SetActive(true);
        }
        else
        {
            isSFXOn = false;
            img_SFXTick.gameObject.SetActive(false);
        }
    }

    public void OnClick_Music()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_MUSIC) == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsData.KEY_MUSIC, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsData.KEY_MUSIC, 0);
        }
        CheckStateOfMusic();
    }

    public void OnClick_SFX()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX) == 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsData.KEY_SFX, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsData.KEY_SFX, 0);
        }
        Debug.Log("SFX : " + PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX));
        CheckStateOfSFX();
    }

    public void OnClick_Rateus()
    {
        Debug.Log("Rate US");
    }
}



