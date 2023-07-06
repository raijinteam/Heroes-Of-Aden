using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerDetailsUI : MonoBehaviour
{

    [SerializeField] private GameObject playerDetailsPanel;

    [SerializeField] private TextMeshProUGUI txt_PlayerName;
    [SerializeField] private Image img_PlayerIcon;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    [SerializeField] private TextMeshProUGUI txt_PlayerDesctiption;
    [SerializeField] private TextMeshProUGUI txt_PlayerHealth;
    [SerializeField] private TextMeshProUGUI txt_Damage;
    [SerializeField] private TextMeshProUGUI txt_Firerate;

    [SerializeField] private Image img_UpgradeResource;

    [SerializeField] private Button btn_Select;
    [SerializeField] private Button btn_Upgrade;

    [SerializeField] private TextMeshProUGUI txt_UpgradeButton;

    [SerializeField] private Sprite sprite_Coin;
    [SerializeField] private Sprite sprite_Gems;

    [SerializeField] private float flt_ScaleUpAnimationDuration = 0.2f;

    private int selectedIndex;

    [SerializeField] private float flt_PlayerStateAnimationDuration = 0.5f;
    private float currentHealth;
    private int targetHealth;
    private bool canIncreaseHealth;
    private float currentDamage;
    private int targetDamage;
    private bool canIncreaseDamage;
    private float currentFirerate;
    private float targetFirerate;
    private bool canIncreaseFirerate;

    private void OnEnable()
    {
        playerDetailsPanel.transform.DOScale(new Vector3(1, 1, 1), flt_ScaleUpAnimationDuration);

        int currentPlayerLevel = PlayerDataManager.Instance.all_CharchterData[selectedIndex].level;


        currentHealth = (int)PlayerDataManager.Instance.all_CharchterData[selectedIndex].health[currentPlayerLevel];

        currentDamage = (int)PlayerDataManager.Instance.all_CharchterData[selectedIndex].damage[currentPlayerLevel];

        currentFirerate = (int)PlayerDataManager.Instance.all_CharchterData[selectedIndex].firerate[currentPlayerLevel];
    }


    private void Update()
    {
        if(currentHealth != targetHealth && canIncreaseHealth)
        {
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, flt_PlayerStateAnimationDuration);
            txt_PlayerHealth.text = ((int)currentHealth).ToString();
            canIncreaseDamage = true;
        }
        if(currentDamage != targetDamage && canIncreaseDamage)
        {
            currentDamage = Mathf.Lerp(currentDamage, targetDamage, flt_PlayerStateAnimationDuration);
            txt_Damage.text = ((int)currentDamage).ToString();
            canIncreaseFirerate = true;
            canIncreaseHealth = false;
        }
        if(currentFirerate != targetFirerate && canIncreaseFirerate)
        {
            currentFirerate = Mathf.Lerp(currentFirerate, targetFirerate, flt_PlayerStateAnimationDuration);
            txt_Firerate.text = ((int)currentFirerate).ToString();
            canIncreaseDamage = false;
        }

    }

    public void SetSelectedPlayerData(int _selectedIndex)
    {
        int selectedPlayerLevel = PlayerDataManager.Instance.GetPlayerLevel(_selectedIndex);
        selectedIndex = _selectedIndex;

        //IF PLAYER LOCKED SHOW LOCKED IN LEVEL AND IF NOT SHOW PLAYER LEVEL
        if (PlayerDataManager.Instance.IsPlayerLocked(_selectedIndex))
        {
            btn_Select.gameObject.SetActive(false);
            txt_PlayerLevel.text = "Locked";
            Debug.Log("Plyaer Locked");
        }
        else
        {
            btn_Select.gameObject.SetActive(true);
            Debug.Log("Plyaer Unlocked");
            txt_PlayerLevel.text = "LV." + (PlayerDataManager.Instance.GetPlayerLevel(_selectedIndex)).ToString();
        }

        img_UpgradeResource.sprite = sprite_Gems;
        if (PlayerDataManager.Instance.all_CharchterData[_selectedIndex].isCoinBased)
        {
            img_UpgradeResource.sprite = sprite_Coin;
        }


        txt_PlayerName.text = PlayerDataManager.Instance.GetPlayerName(_selectedIndex);
        img_PlayerIcon.sprite = UIManager.Instance.ui_PlayerSelector.all_Player[_selectedIndex].img_PlayerIcon.sprite;

        txt_PlayerDesctiption.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].description;

        txt_PlayerHealth.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].health[selectedPlayerLevel].ToString();

        txt_Damage.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].damage[selectedPlayerLevel].ToString();

        txt_Firerate.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].firerate[selectedPlayerLevel].ToString() + "s";

        txt_UpgradeButton.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].upgradeAmount[PlayerDataManager.Instance.GetPlayerLevel(_selectedIndex)].ToString();


        Debug.Log("Selected index " + _selectedIndex);
        if (PlayerDataManager.Instance.IsPlayerReachMaxLevel(_selectedIndex))
        {
            Debug.Log("Hide Btn Upgrade ");
            txt_PlayerLevel.text = "Max";
            btn_Upgrade.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Show Btn Upgrade ");
            btn_Upgrade.gameObject.SetActive(true);
        }
    }


    private IEnumerator IncreasePlayerStartAnimation()
    {

        float timer = 0;
        float startingValue = currentHealth;

        //this is for health

        txt_PlayerHealth.color = Color.white;
        txt_Damage.color = Color.white;
        txt_Firerate.color = Color.white;
        Sequence seq = DOTween.Sequence();
        seq.Append(txt_PlayerHealth.transform.DOScale(1.2f, 0.2f).SetEase(Ease.Linear)).Append(txt_PlayerHealth.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear)).SetLoops(3);
        while (timer <= 1)
        {

            timer += Time.deltaTime / flt_PlayerStateAnimationDuration;
            
            currentHealth = Mathf.Lerp(startingValue, targetHealth, timer);
            txt_PlayerHealth.text = (currentHealth).ToString("F0");
            txt_PlayerHealth.color = Color.green;
            yield return null;
        }
        txt_PlayerHealth.color = Color.white;
        txt_PlayerHealth.transform.DOScale(Vector3.one, 0.1f);;

        //this is for damage

        timer = 0;
        startingValue = currentDamage;
        Sequence seq1 = DOTween.Sequence();
        seq1.Append(txt_Damage.transform.DOScale(1.2f, 0.2f).SetEase(Ease.Linear)).Append(txt_Damage.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear)).SetLoops(3);
        while (timer <= 1)
        {

            timer += Time.deltaTime / flt_PlayerStateAnimationDuration;

            currentDamage = Mathf.Lerp(startingValue, targetDamage, timer);
            txt_Damage.text = (currentDamage).ToString("F0");
            txt_Damage.color = Color.green;
            yield return null;
        }

        txt_Damage.color = Color.white;
        txt_Damage.transform.DOScale(Vector3.one, 0.1f);
        //Debug.Log("Start third loop");

        //this is for firerate

        timer = 0;
        startingValue = currentFirerate;
        Sequence seq2 = DOTween.Sequence();
        seq2.Append(txt_Firerate.transform.DOScale(1.2f, 0.2f).SetEase(Ease.Linear)).Append(txt_Firerate.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear)).SetLoops(3);
        while (timer <= 1)
        {

            timer += Time.deltaTime / flt_PlayerStateAnimationDuration;
            
            currentFirerate = Mathf.Lerp(startingValue, targetFirerate, timer);
            txt_Firerate.text = (currentFirerate).ToString("0.00") + " s";
            txt_Firerate.color = Color.green;
            yield return null;
        }
        txt_Firerate.color = Color.white;
        txt_Firerate.transform.DOScale(Vector3.one, 0.1f);
    }

    private void SetActiveFalseMenu()
    {
        this.gameObject.SetActive(false);
    }

    //BUTTON CLICK WHEN USER SELECT PLAYER
    public void OnClick_SelectPlayer()
    {
        this.gameObject.SetActive(false);
        PlayerPrefs.SetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX, selectedIndex);
        UIManager.Instance.ui_HomePanel.SetActivePlayerImage(selectedIndex);
    }

    //BUTTON EVENT WHEN USER CLICK FOR UPGRADE BUTTON 
    public void OnClick_UpgradePlayer()
    {
        int selectedPlayerLevel = PlayerDataManager.Instance.GetPlayerLevel(selectedIndex) + 1;

        if (!PlayerDataManager.Instance.all_CharchterData[selectedIndex].isCoinBased)
        {
            if (!PlayerDataManager.Instance.HasEnoughGemsForUpgradePlayer(selectedIndex))
            {
                UIManager.Instance.SpawnPopUpBox("Not Enough Gems");
                return;
            }
            ServiceManager.Instance.dataManager.SubstractGames(PlayerDataManager.Instance.all_CharchterData[selectedIndex].upgradeAmount[selectedPlayerLevel]);
            
        }
        else
        {
            if (!PlayerDataManager.Instance.hasEnoughCoinsForUpgradePlayer(selectedIndex))
            {
                UIManager.Instance.SpawnPopUpBox("Not Enough Coins");
                return;
            }
            ServiceManager.Instance.dataManager.SubstractCoins(PlayerDataManager.Instance.all_CharchterData[selectedIndex].upgradeAmount[selectedPlayerLevel]);
            
        }

      
       


        if (PlayerDataManager.Instance.IsPlayerLocked(selectedIndex))
        {
            PlayerDataManager.Instance.SetUnlockPlayer(selectedIndex);
            btn_Select.gameObject.SetActive(true);
            PlayerDataManager.Instance.SetPlayerLevel(selectedIndex);
            UIManager.Instance.ui_PlayerSelector.SetPlayerLevelText(selectedIndex);
            UIManager.Instance.ui_PlayerSelector.CheckForPlayerUnlocked();
            txt_PlayerLevel.text = "LV." + (PlayerDataManager.Instance.GetPlayerLevel(selectedIndex)).ToString();
        }
        else
        {
            PlayerDataManager.Instance.SetPlayerLevel(selectedIndex);

            UIManager.Instance.ui_PlayerSelector.SetPlayerLevelText(selectedIndex);
            txt_PlayerLevel.text = "LV." + (PlayerDataManager.Instance.GetPlayerLevel(selectedIndex)).ToString();

          //  txt_PlayerHealth.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].health[selectedPlayerLevel].ToString();
            targetHealth = (int)PlayerDataManager.Instance.all_CharchterData[selectedIndex].health[selectedPlayerLevel];

          //  txt_Damage.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].damage[selectedPlayerLevel].ToString();
            targetDamage = (int)PlayerDataManager.Instance.all_CharchterData[selectedIndex].damage[selectedPlayerLevel];

           // txt_Firerate.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].firerate[selectedPlayerLevel].ToString();
            targetFirerate = PlayerDataManager.Instance.all_CharchterData[selectedIndex].firerate[selectedPlayerLevel];

            txt_UpgradeButton.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].upgradeAmount[PlayerDataManager.Instance.GetPlayerLevel(selectedIndex)].ToString();

            /*canIncreaseHealth = true;
            canIncreaseFirerate = false;*/
            StartCoroutine(IncreasePlayerStartAnimation());
        }


        if (PlayerDataManager.Instance.IsPlayerReachMaxLevel(selectedIndex))
        {
            txt_PlayerLevel.text = "Max";
            btn_Upgrade.gameObject.SetActive(false);
            return;
        }
    }

    public void OnClick_CloseMenu()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(playerDetailsPanel.transform.DOScale(Vector3.zero, flt_ScaleUpAnimationDuration).OnComplete(SetActiveFalseMenu));
    }
}
