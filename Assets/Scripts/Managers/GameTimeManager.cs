using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{

    //public static GameTimeManager Instance;

    public float gameStartTime;

    public int minutesForIncreaseEnergyOverTime = 1;
    private TimeSpan timeSpan;

    //private void Awake()
    //{
    //    if (FindObjectsOfType(GetType()).Length > 1)
    //    {
    //        Destroy(gameObject);
    //    }

    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}


    

    private void Start()
    {

        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) < 30)
        {
            string dateQuitString = PlayerPrefs.GetString(PlayerPrefsData.KEY_QUIT_TIME);
            float perviousActiveGameSeconds = PlayerPrefs.GetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME);
            if (!dateQuitString.Equals(""))
            {
                DateTime dateQuit = DateTime.Parse(dateQuitString);
                DateTime dateNow = DateTime.Now;

                if (dateNow > dateQuit)
                {
                    timeSpan = dateNow - dateQuit;

                    float totalSeconds = (float)timeSpan.TotalSeconds;

                   

                    float totalTime = perviousActiveGameSeconds + totalSeconds;

                    Debug.Log("Total Time for energy give : " + (int)totalTime / 180);

                    int tempEnergy = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) + (int)totalTime / (minutesForIncreaseEnergyOverTime * 60);

                    float energyCount = (int)totalTime / (minutesForIncreaseEnergyOverTime * 60);
                    Debug.Log("Energy Count For adding : " + energyCount);
                    if (tempEnergy > 30)
                    {
                        float requiredEnergy = 30 - PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY);
                        energyCount = Mathf.Min(energyCount, requiredEnergy);
                    }


                    Debug.Log("Total Energy TO add : " + energyCount);

                    ServiceManager.Instance.dataManager.IncreaseEnergy((int)energyCount);

                    Debug.Log("Quit For " + timeSpan.TotalSeconds + " Seconds");
                   // Debug.Log("Total Time : " + timeSpan);
                }

                PlayerPrefs.SetString(PlayerPrefsData.KEY_QUIT_TIME, "");
            }

            Debug.Log("Previous Game Active Time : " + PlayerPrefs.GetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME));
          
            PlayerPrefs.SetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME, gameStartTime);
            gameStartTime = perviousActiveGameSeconds + timeSpan.Seconds;
            if(gameStartTime >= 180)
            {
                gameStartTime -= 180;
            }
        }

    }




    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) < 30)
        {
            gameStartTime += Time.deltaTime;
            int gameMinutes = Mathf.FloorToInt(gameStartTime / (minutesForIncreaseEnergyOverTime * 60));
            if (gameMinutes == minutesForIncreaseEnergyOverTime)
            {
                ServiceManager.Instance.dataManager.IncreaseEnergy(1);
                gameStartTime = 0;
            }
        }
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME, gameStartTime);
        DateTime currentTime = DateTime.Now;
        PlayerPrefs.SetString(PlayerPrefsData.KEY_QUIT_TIME, currentTime.ToString());
    }

    private int AddEnergyCount(int _totalMinites)
    {
        return _totalMinites / minutesForIncreaseEnergyOverTime;
    }
}
