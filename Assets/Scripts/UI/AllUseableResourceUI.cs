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
        coins = ServiceManager.Instance.dataManager.totalCoins;
        gems = ServiceManager.Instance.dataManager.totalGems;
        energy = ServiceManager.Instance.dataManager.totalEnergy;

        txt_CoinAmount.text = coins.ToString("F0");
        txt_GamsAmount.text = gems.ToString("F0");
        txt_EnergyAmount.text = energy.ToString("F0");
    }

    private void Update()
    {
        if (coins != ServiceManager.Instance.dataManager.totalCoins)
        {
            coins = Mathf.Lerp(coins, ServiceManager.Instance.dataManager.totalCoins, lerpTimer);

            txt_CoinAmount.text = coins.ToString("F0");
        }


        if(gems != ServiceManager.Instance.dataManager.totalGems)
        {
            gems = Mathf.Lerp(gems, ServiceManager.Instance.dataManager.totalGems, lerpTimer);

            txt_GamsAmount.text = gems.ToString("F0");
        }

        if (energy != ServiceManager.Instance.dataManager.totalEnergy)
        {
            energy = Mathf.Lerp(energy, ServiceManager.Instance.dataManager.totalEnergy, lerpTimer);
            txt_EnergyAmount.text = energy.ToString("F0");
        }
    }




    public void OnClick_BuyEnergy()
    {
        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        UIManager.Instance.ui_Navigation.OnClick_MenuActivate(0);

        UIManager.Instance.ui_Shop.gameObject.SetActive(true);
        Vector2 position = new Vector2(0, 2000);
        UIManager.Instance.ui_Shop.ScrollDownAnimation(position);
    }

    public void OnClick_BuyCoin()
    {
        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        UIManager.Instance.ui_Shop.gameObject.SetActive(true);

        UIManager.Instance.ui_Navigation.OnClick_MenuActivate(0);

        Vector2 position = new Vector2(0, 300);
        UIManager.Instance.ui_Shop.ScrollDownAnimation(position);
    }

    public void OnClick_BuyGems()
    {
        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        UIManager.Instance.ui_Shop.gameObject.SetActive(true);

        UIManager.Instance.ui_Navigation.OnClick_MenuActivate(0);

        Vector2 position = new Vector2(0, 1500);
        UIManager.Instance.ui_Shop.ScrollDownAnimation(position);
    }
}
