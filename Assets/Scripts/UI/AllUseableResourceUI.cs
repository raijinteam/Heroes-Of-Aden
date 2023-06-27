using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllUseableResourceUI : MonoBehaviour
{
    public TextMeshProUGUI txt_EnergyAmount;
    public TextMeshProUGUI txt_CoinAmount;
    public TextMeshProUGUI txt_GamsAmount;


    private float coins;
    private float targetCoins;
    [SerializeField] private float lerpTimer = 0.5f;

    private float gams;
    private float targetGames;

    private float energy;
    private float targetEnergy;

    private void Start()
    {
        coins = PlayerPrefs.GetInt(PlayerPrefsData.KEY_COINS);
        targetCoins = PlayerPrefs.GetInt(PlayerPrefsData.KEY_COINS);
        txt_CoinAmount.text = coins.ToString("F0");

        gams = PlayerPrefs.GetInt(PlayerPrefsData.KEY_GAMES);
        targetGames = PlayerPrefs.GetInt(PlayerPrefsData.KEY_GAMES);
        txt_GamsAmount.text = gams.ToString("F0");

        energy = DataManager.Instance.totalEnergy;
        targetEnergy = DataManager.Instance.totalEnergy;
        txt_EnergyAmount.text = energy.ToString("F0");
    }


    private void Update()
    {
        if (coins != targetCoins)
        {
            coins = Mathf.Lerp(coins, targetCoins, lerpTimer);

            txt_CoinAmount.text = coins.ToString("F0");
        }


        if(gams != targetGames)
        {
            gams = Mathf.Lerp(gams, targetGames, lerpTimer);

            txt_GamsAmount.text = gams.ToString("F0");
        }

        if (energy != targetEnergy)
        {
            energy = Mathf.Lerp(energy, targetEnergy, lerpTimer);
            txt_EnergyAmount.text = energy.ToString("F0");
        }
    }

    public void CalcCoins()
    {
        targetCoins = PlayerPrefs.GetInt(PlayerPrefsData.KEY_COINS);
    }

    public void CalcGames()
    {
        targetGames = PlayerPrefs.GetInt(PlayerPrefsData.KEY_GAMES);
    }

    public void CalcEnergy()
    {
        targetEnergy = DataManager.Instance.totalEnergy;
       // Debug.Log("Total Energy : " + targetEnergy);
    }


}
