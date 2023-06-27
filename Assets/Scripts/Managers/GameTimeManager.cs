using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{

    public static GameTimeManager Instance;

    public float gameStartTime;

    public int minutesForIncreaseEnergyOverTime = 1;
    private TimeSpan timeSpan;

    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    

    private void Start()
    {

        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) < 30)
        {
            string dateQuitString = PlayerPrefs.GetString(PlayerPrefsData.KEY_QUIT_TIME);

            if (!dateQuitString.Equals(""))
            {
                DateTime dateQuit = DateTime.Parse(dateQuitString);
                DateTime dateNow = DateTime.Now;

                if (dateNow > dateQuit)
                {
                    timeSpan = dateNow - dateQuit;

                    float totalSeconds = (float)timeSpan.TotalSeconds;

                    float perviousActiveGameSeconds = PlayerPrefs.GetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME);

                    float totalTime = perviousActiveGameSeconds + totalSeconds;

                    int tempEnergy = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) + AddEnergyCount((int)totalTime / 60);

                    float energyCount = AddEnergyCount((int)totalTime / 60);
                    if (tempEnergy > 30)
                    {
                        float requiredEnergy = 30 - PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY);
                        energyCount = Mathf.Min(energyCount, requiredEnergy);
                    }


                    Debug.Log("Total Energy TO add : " + energyCount);

                    DataManager.Instance.IncreaseEnergy((int)energyCount);

                    Debug.Log("Quit For " + timeSpan.TotalSeconds + " Seconds");
                    Debug.Log("Total Time : " + totalTime);
                }

                PlayerPrefs.SetString(PlayerPrefsData.KEY_QUIT_TIME, "");
            }

            Debug.Log("Previous Game Active Time : " + PlayerPrefs.GetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME));
            PlayerPrefs.SetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME, gameStartTime);
            gameStartTime = timeSpan.Seconds;
        }


    }




    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) < 30)
        {
            gameStartTime += Time.deltaTime;
            int gameMinutes = Mathf.FloorToInt(gameStartTime / 60f);
            if (gameMinutes == minutesForIncreaseEnergyOverTime)
            {
                DataManager.Instance.IncreaseEnergy((int)gameMinutes);
                IncreaseEnergyOverTime(AddEnergyCount((int)gameMinutes));
                UIManager.Instance.ui_useableResource.CalcEnergy();
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


    //INCREASE ENERGY OVER TIME 
    private void IncreaseEnergyOverTime(int _countsOfEnergy)
    {
        //DataManager.Instance.IncreaseEnergy(_countsOfEnergy);
    }























    //Not Working Code

    /*  private void Start()
      {
          string dateQuitString = PlayerPrefs.GetString(PlayerPrefsData.KEY_GAME_CURRENT_TIME);

          DateTime dateQuit = DateTime.Parse(dateQuitString);
          DateTime timeNow = DateTime.Now;

          if (timeNow > dateQuit)
          {
              timeSpan = timeNow - dateQuit;

              Debug.Log("Quit Time : " + timeSpan);
              Debug.Log("Quit Seconds : " + timeSpan.TotalMinutes);

              if (timeSpan.TotalMinutes >= minutesToGiveEnergy)
              {
                  int totalEnergyToGive = (int)timeSpan.TotalMinutes / minutesToGiveEnergy;
                  Debug.Log("Total Energy to give : " + totalEnergyToGive);
                  GiveEnergyOverTimeBG(totalEnergyToGive);
              }
              Debug.Log("Count of Give Energy : " + timeSpan.TotalMinutes / minutesToGiveEnergy);
          }

          startGameTime = timeSpan.Seconds;
          Debug.Log("Start Game time when on : " + startGameTime);

      }

      // Update is called once per frame
      void Update()
      {
          startGameTime += Time.deltaTime;

          if(startGameTime >= (minutesToGiveEnergy * 60))
          {
              Debug.Log("Increase Energy");
              DataManager.Instance.IncreaseEnergy(1);
              startGameTime = 0;
          }
      }

      public void GiveEnergyOverTimeBG(int _energyCount)
      {
          DataManager.Instance.totalEnergy += _energyCount;
          PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, DataManager.Instance.totalEnergy);
          Debug.Log(PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY));
      }



      private void OnDisable()
      {
          PlayerPrefs.SetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME, startGameTime);
          DateTime currentTime = DateTime.Now;
          PlayerPrefs.SetString(PlayerPrefsData.KEY_GAME_CURRENT_TIME, currentTime.ToString());
      }


      //SUDO
      *//*
       * gameactive ne save kare 
       * 
       * 
       * 
       * 
       * 
       * 
       */

}
