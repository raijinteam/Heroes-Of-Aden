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
	[SerializeField] private GameObject[] all_ObjectsForAnimate;
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

		StartCoroutine(DelayTimer());

		/*Sequence seq = DOTween.Sequence();


        Tween tween1 = txt_GameplayTimer.DOFade(1, 60);
        Tween tween2 = BestTimePanel.transform.DOScale(Vector3.one, 60);
        Tween tween3 = killCountPanel.transform.DOScale(Vector3.one, 60);
        Tween tween4 = coinPanel.transform.DOScale(Vector3.one, 60);
        Tween tween5 = btn_2XReward.transform.DOScale(Vector3.one, 60);
        Tween tween6 = btn_Continue.transform.DOScale(Vector3.one, 60);*/


        /*seq.Append(tween1);
		seq.InsertCallback(tween1.Duration() * 0.5f, () => { tween2.Play(); });
		seq.InsertCallback(tween2.Duration() * 0.5f, () => { tween3.Play(); });
		seq.InsertCallback(tween3.Duration() * 0.5f, () => { tween4.Play(); });
		seq.InsertCallback(tween4.Duration() * 0.5f, () => { tween5.Play(); });
		seq.InsertCallback(tween4.Duration() * 0.5f, () => { tween6.Play(); });*/

        //seq.Play();

        /*seq.Append(txt_GameplayTimer.DOFade(1, flt_AnimaitonDuration)).
            Append(BestTimePanel.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).SetDelay(flt_AnimaitonDuration / 2).
			Append(killCountPanel.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).SetDelay(flt_AnimaitonDuration).
			Append(coinPanel.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).SetDelay(flt_AnimaitonDuration + 1.5f).
			Append(coinPanel.transform.DOScale(Vector3.one, flt_AnimaitonDuration)).
			Append(btn_Continue.transform.DOScale(Vector3.one, flt_AnimaitonDuration));*/
    }


	private IEnumerator DelayTimer()
    {

		for(int i = 0; i < all_ObjectsForAnimate.Length; i++)
        {
			GameObject currentObjectToAnimate = all_ObjectsForAnimate[i];

			if( i == 0)
            {
				txt_GameplayTimer.DOFade(1f, flt_AnimaitonDuration);
            }
            else
            {
				currentObjectToAnimate.transform.DOScale(Vector3.one, flt_AnimaitonDuration);
            }

			yield return new WaitForSeconds(flt_AnimaitonDuration / 2);
		}


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
