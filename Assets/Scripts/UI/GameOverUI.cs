using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

	[SerializeField] private TextMeshProUGUI txt_GameplayTimer;
	[SerializeField] private TextMeshProUGUI txt_Besttime;
	[SerializeField] private TextMeshProUGUI txt_collectedCoins;
	[SerializeField] private TextMeshProUGUI txt_killedCount;


    private void OnEnable()
    {
		float activeGameTime = GameManager.Instance.timeInThisRound;
		int minutes = Mathf.FloorToInt(activeGameTime / 60f);
		int seconds = Mathf.FloorToInt(activeGameTime % 60f);
		txt_GameplayTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

		Debug.Log("Best time");
		if (activeGameTime >= PlayerPrefs.GetFloat(PlayerPrefsData.KEY_BESTTIME))
        {
			Debug.Log("Set Best time");
			PlayerPrefs.SetFloat(PlayerPrefsData.KEY_BESTTIME, activeGameTime);
        }
		float bestTime = PlayerPrefs.GetFloat(PlayerPrefsData.KEY_BESTTIME);
		int minutesBest = Mathf.FloorToInt(bestTime / 60f);
		int secondsBest = Mathf.FloorToInt(bestTime % 60f);
		txt_Besttime.text = string.Format("{0:00}:{1:00}", minutesBest, secondsBest);

		txt_collectedCoins.text = GameManager.Instance.coinsCollectedInThisRound.ToString();
		txt_killedCount.text = GameManager.Instance.killCountInThisRound.ToString();
	}


    public void OnClick_Continue()
    {
        GameManager.Instance.isPlayerTakeRevive = false;
        GameManager.Instance.DestoryAllEnemies();
        GameManager.Instance.DestoryAllPlayerBullet();
        DataManager.Instance.ResetRoundDataWhenGameover();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
