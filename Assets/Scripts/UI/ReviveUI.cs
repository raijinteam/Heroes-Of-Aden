using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ReviveUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI txt_Counter;
    [SerializeField] private Slider slider_Counter;
    [SerializeField] private float reviveScreenTimer = 5;

    private float currentReviveScreenTimer;

    private void OnEnable()
    {
        currentReviveScreenTimer = reviveScreenTimer;
        GameManager.Instance.player.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RevivePenalCounterStart();        
    }

    private void RevivePenalCounterStart()
    {
        currentReviveScreenTimer -= Time.deltaTime;
        txt_Counter.text = currentReviveScreenTimer.ToString("F0");
        slider_Counter.value -= Time.deltaTime / reviveScreenTimer;
        if(slider_Counter.value <= 0)
        {
            Debug.Log("Show Game over Screen");
            this.gameObject.SetActive(false);
            GameManager.Instance.PlayerDied();
        }
    }

    public void OnClick_Revive()
    {
        GameManager.Instance.isGameRunning = true;
        GameManager.Instance.player.gameObject.SetActive(true);
        currentReviveScreenTimer = reviveScreenTimer;
        this.gameObject.SetActive(false);
        GameManager.Instance.GiveAllPointsAndCoinOnRevive();
        GameManager.Instance.DestoryAllEnemies();
        GameManager.Instance.DestoryAllPlayerBullet();
    }


}
