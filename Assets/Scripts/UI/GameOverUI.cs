using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{

	[SerializeField] private TextMeshProUGUI txt_GameplayTimer;
	[SerializeField] private TextMeshProUGUI txt_Besttime;
	[SerializeField] private TextMeshProUGUI txt_collectedCoins;
	[SerializeField] private TextMeshProUGUI txt_killedCount;
	[SerializeField] private Button btn_2XReward;
	[SerializeField] private Button btn_Continue;

	[Header("For Animation")]
	[SerializeField] private GameObject BestTimePanel;
	[SerializeField] private GameObject killCountPanel;
	[SerializeField] private GameObject coinPanel;


	[SerializeField] private float flt_AnimaitonDuration = 0.3f;

    private void OnEnable()
    {
		btn_2XReward.gameObject.SetActive(true);
		Destroy(GameManager.Instance.player.gameObject);
		GameManager.Instance.DestoryAllEnemies();
		GameManager.Instance.DestoryAllPlayerBullet();

		SetTimerData();

		txt_collectedCoins.text = GameManager.Instance.coinsCollectedInThisRound.ToString();
		txt_killedCount.text = GameManager.Instance.killCountInThisRound.ToString();
		StartAnimation();
	}


    private void OnDisable()
    {
		ResetAnimation();
    }

    private void StartAnimation()
    {
		Sequence seq = DOTween.Sequence();

		seq.Append(txt_GameplayTimer.DOFade(1, flt_AnimaitonDuration)).
			Append(BestTimePanel.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).
			Append(killCountPanel.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).
			Append(coinPanel.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).
			Append(btn_2XReward.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).
			Append(btn_Continue.transform.DOScale(Vector3.one, flt_AnimaitonDuration));
    }

	private void ResetAnimation()
    {
		txt_GameplayTimer.DOFade(0, flt_AnimaitonDuration);
		BestTimePanel.transform.DOScale(Vector3.zero, flt_AnimaitonDuration);
		killCountPanel.transform.DOScale(Vector3.zero, flt_AnimaitonDuration);
		coinPanel.transform.DOScale(Vector3.zero, flt_AnimaitonDuration);
		btn_2XReward.transform.DOScale(Vector3.zero, flt_AnimaitonDuration);
		btn_Continue.transform.DOScale(Vector3.zero, flt_AnimaitonDuration);
	}

	private void SetTimerData()
    {
		float activeGameTime = GameManager.Instance.timeInThisRound; //Get Active Game Timer
		int minutes = Mathf.FloorToInt(activeGameTime / 60f); 
		int seconds = Mathf.FloorToInt(activeGameTime % 60f);
		txt_GameplayTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

		//Chech of best time
		if (activeGameTime >= PlayerPrefs.GetFloat(PlayerPrefsData.KEY_BESTTIME))
		{
			PlayerPrefs.SetFloat(PlayerPrefsData.KEY_BESTTIME, activeGameTime);
		}
		float bestTime = PlayerPrefs.GetFloat(PlayerPrefsData.KEY_BESTTIME);
		int minutesBest = Mathf.FloorToInt(bestTime / 60f);
		int secondsBest = Mathf.FloorToInt(bestTime % 60f);
		txt_Besttime.text = string.Format("{0:00}:{1:00}", minutesBest, secondsBest);
	}


    public void OnClick_Continue()
    {
        GameManager.Instance.isPlayerTakeRevive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void OnClick_2XCoin()
    {
		btn_2XReward.gameObject.SetActive(false);
		txt_collectedCoins.text = (GameManager.Instance.coinsCollectedInThisRound * 2).ToString();
		DataManager.Instance.IncreaseCoins(GameManager.Instance.coinsCollectedInThisRound * 2);
	}
}
