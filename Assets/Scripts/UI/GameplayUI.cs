using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Slider slider_Level;
	[SerializeField] private TextMeshProUGUI txt_PlayerLevel;
	[SerializeField] private TextMeshProUGUI txt_GameplayTimer;
	[SerializeField] private TextMeshProUGUI txt_GameplayCoins;
	[SerializeField] private TextMeshProUGUI txt_GameplayKillCount;

	private int coinsCount = 0;
	private int killCount = 0;

	[Header("Abilities Selection")]
	public GameObject panel_AbilitySelection;
	public AbilityInfoUI[] all_AbilityInfo;


    private void OnEnable()
    {
		txt_GameplayCoins.text = coinsCount.ToString();
		txt_GameplayKillCount.text = killCount.ToString();
    }

    public void SetLevelSlider(float _maxValue, float _currentValue, int _level)
	{
		slider_Level.maxValue = _maxValue;
		slider_Level.value = _currentValue;

		int playerLevel = _level + 1;
		txt_PlayerLevel.text = playerLevel.ToString();
	}

	public void UpdateLevelSlider(float _currentValue)
	{
		slider_Level.value = _currentValue;
	}
	
	public void IncreasegameplayCoinCount(int coins)
    {
		txt_GameplayCoins.text = coins.ToString();
    }

	public void IncreaseGameplayKillCount()
    {
		killCount += 1;
		GameManager.Instance.killCountInThisRound = killCount;
		txt_GameplayKillCount.text = killCount.ToString();
    }

	public TextMeshProUGUI GetGameplayTimer()
    {
		return txt_GameplayTimer;
    }

	public void OnClick_Pause()
    {
		Time.timeScale = 0;
		UIManager.Instance.ui_Pause.gameObject.SetActive(true);
    }

}
