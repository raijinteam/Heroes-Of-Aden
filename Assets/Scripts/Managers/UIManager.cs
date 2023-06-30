using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	public bool canChangeMenus;

	private void Awake()
	{
		Instance = this;
	}

	[Header("UI SCRIPTS")]
	public HomePanelUI ui_HomePanel;
	public ShopUI ui_Shop;
	public NavigationbarUI ui_Navigation;
	public PassiveUpgradeUI ui_PassiveUpgrade;
	public GameplayUI ui_Gameplay;
	public GameOverUI ui_GameOver;
	public PauseUI ui_Pause;
	public PlayerSelectorUI ui_PlayerSelector;
	public AllUseableResourceUI ui_useableResource;
	public ReviveUI ui_Revive;
	public SettingUI ui_Setting;


	[HideInInspector]
	public bool canPopupBoxSpawn = true; // CHECK IF POPUP BOX SPAWN OR NOT

	[Space]
	[Header("Popup Box")]
	[SerializeField] private GameObject pf_PopupBox; // POPUP BOX TO SHOW NOT ENOUGH RESOURCE
	[SerializeField] private Transform tf_PopupBoxParent; // POPUP BOX PARENT 


	//SPAWN POPUP BOX WHICH DISPLAY MESSAGE NOT ENOUGH VALUES
	public void SpawnPopUpBox(string _message)
	{
		if (canPopupBoxSpawn)
		{
			canPopupBoxSpawn = false;
			GameObject popUp = Instantiate(pf_PopupBox, tf_PopupBoxParent.position, Quaternion.identity, tf_PopupBoxParent);
			popUp.GetComponent<PopupBoxUI>().txt_Value.text = _message;
		}
	}
}
