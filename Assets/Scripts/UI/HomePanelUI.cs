using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HomePanelUI : MonoBehaviour
{
    public Image[] all_SelectedPlayerImg;
    [SerializeField] private TextMeshProUGUI txt_RequireEneries;
    [SerializeField] private int requireEnergyToStart = 10;
    private int activePlayerIndex;

    private void OnEnable()
    {
        activePlayerIndex = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX);

        txt_RequireEneries.text = requireEnergyToStart.ToString();
        SetActivePlayerImage(activePlayerIndex);

    }


    public void SetActivePlayerImage(int _activePlayerIndex)
    {
        for (int i = 0; i < all_SelectedPlayerImg.Length; i++)
        {
            all_SelectedPlayerImg[i].gameObject.SetActive(false);
            if (i == _activePlayerIndex)
            {
                all_SelectedPlayerImg[i].gameObject.SetActive(true);
            }
        }
    }

    public void OnClick_StartGame()
    {

        if(!(PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY) >= requireEnergyToStart))
        {
            UIManager.Instance.SpawnPopUpBox("Not Enough Energy");
            return;
        }

        //FOR SHOW RATE US BOX EVERY 2 GAMES
        ServiceManager.Instance.dataManager.gameCountForShowRateusBox++;
        ServiceManager.Instance.dataManager.IncreaseRateusGameCount();

        GameManager.Instance.InstantiateActivePlayer();
        this.gameObject.SetActive(false);
        ServiceManager.Instance.dataManager.SubstractEnergy(requireEnergyToStart);
        GameManager.Instance.StartGame();
    }
}
