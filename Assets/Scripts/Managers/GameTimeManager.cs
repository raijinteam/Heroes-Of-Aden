using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{

    public float gameStartTime;

    public int minutesForIncreaseEnergyOverTime = 1;

    private void Start()
    {
        
        if(PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) < 30)
        {
            string dateQuitString = PlayerPrefs.GetString(PlayerPrefsData.KEY_QUIT_TIME);

            if (!dateQuitString.Equals(""))
            {
                DateTime dateQuit = DateTime.Parse(dateQuitString);
                DateTime dateNow = DateTime.Now;

                if (dateNow > dateQuit)
                {
                    TimeSpan timeSpan = dateNow - dateQuit;

                    float totalSeconds = (float)timeSpan.TotalSeconds;

                    float perviousActiveGameSeconds = PlayerPrefs.GetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME);

                    float totalTime = perviousActiveGameSeconds + totalSeconds;

                    int tempEnergy = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) + AddEnergyCount((int)totalTime / 60);
                    
                    float energyCount = AddEnergyCount((int)totalTime / 60);
                    if(tempEnergy > 30)
                    {
                        float requiredEnergy = 30 - PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY);
                        energyCount = Mathf.Min(energyCount, requiredEnergy);
                    }


                    Debug.Log("Total Energy TO add : " + energyCount);

                    IncreaseEnergyOverTime((int)energyCount);

                    Debug.Log("Quit For " + timeSpan.TotalSeconds + " Seconds");
                    Debug.Log("Total Time : " + totalTime);
                }

                PlayerPrefs.SetString(PlayerPrefsData.KEY_QUIT_TIME, "");
            }

            Debug.Log("Previous Game Active Time : " + PlayerPrefs.GetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME));
            PlayerPrefs.SetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME, gameStartTime);
        }

        
    }




    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) < 30)
        {
            gameStartTime += Time.deltaTime;
            int gameMinutes = Mathf.FloorToInt(gameStartTime / 60f);
            if (gameMinutes == minutesForIncreaseEnergyOverTime)
            {
                IncreaseEnergyOverTime(AddEnergyCount((int)gameMinutes));
                UIManager.Instance.ui_useableResource.CalcEnergy();
                gameStartTime = 0;
            }
        }
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME, gameStartTime);
    }

    private int AddEnergyCount(int _totalMinites)
    {
        return _totalMinites / minutesForIncreaseEnergyOverTime;
    }

    private void IncreaseEnergyInBG(int _countOfEnergy)
    {
        DataManager.Instance.totalEnergy = _countOfEnergy;
        PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, DataManager.Instance.totalEnergy);
    }

    //INCREASE ENERGY OVER TIME 
    private void IncreaseEnergyOverTime(int _countsOfEnergy)
    {

        DataManager.Instance.totalEnergy += _countsOfEnergy;
        PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, DataManager.Instance.totalEnergy);

    }
}
