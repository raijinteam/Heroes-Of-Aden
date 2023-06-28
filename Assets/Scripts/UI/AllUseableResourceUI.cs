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

    private float gems;
    private float targetGames;

    private float energy;
    private float targetEnergy;



    private void Start()
    {
        coins = DataManager.Instance.totalCoins;
        gems = DataManager.Instance.totalGems;
        energy = DataManager.Instance.totalEnergy;

        txt_CoinAmount.text = coins.ToString("F0");
        txt_GamsAmount.text = gems.ToString("F0");
        txt_EnergyAmount.text = energy.ToString("F0");
    }

    private void Update()
    {
        if (coins != DataManager.Instance.totalCoins)
        {
            coins = Mathf.Lerp(coins, DataManager.Instance.totalCoins, lerpTimer);

            txt_CoinAmount.text = coins.ToString("F0");
        }


        if(gems != DataManager.Instance.totalGems)
        {
            gems = Mathf.Lerp(gems, DataManager.Instance.totalGems, lerpTimer);

            txt_GamsAmount.text = gems.ToString("F0");
        }

        if (energy != DataManager.Instance.totalEnergy)
        {
            energy = Mathf.Lerp(energy, DataManager.Instance.totalEnergy, lerpTimer);
            txt_EnergyAmount.text = energy.ToString("F0");
        }
    }
}
