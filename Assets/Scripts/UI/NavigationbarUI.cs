using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NavigationbarUI : MonoBehaviour
{
    [SerializeField] private GameObject[] all_MenuPanel; // ALL MENUS PANEL IN GAME
    [SerializeField] private RectTransform[] all_MenusBG; // ALL BUTTONS RECT TRANSFORM
    [SerializeField] private RectTransform[] all_MenuIcons; // ALL MENUS ICONS

    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private float xPositionOffset = 0.2f;



    private void Start()
    {
        OnClick_MenuActivate(2);
    }

    public void OnClick_MenuActivate(int index)
    {

        float startingPosition = 0;

        if (UIManager.Instance.canChangeMenus == true)
        {
            for (int i = 0; i < all_MenusBG.Length; i++)
            {
                //I IS EQULS TO IDEX INCREASE SIZE OF BUTTON AND SET ACTIVE THAT BUTTON
                if (i == index)
                {
                    ServiceManager.Instance.soundManager.PlayButtonClickSound();
                    all_MenusBG[i].DOAnchorMin(new Vector2(startingPosition, all_MenusBG[i].anchorMin.y), animationDuration);
                    startingPosition += xPositionOffset;
                    all_MenusBG[i].DOAnchorMax(new Vector2(startingPosition, 1f), animationDuration);

                    all_MenuPanel[i].SetActive(true);

                }
                //DECEREASE SIZE OF ALL OTHER BUTTONS
                else
                {
                    all_MenusBG[i].DOAnchorMin(new Vector2(startingPosition, all_MenusBG[i].anchorMin.y), animationDuration);
                    startingPosition += xPositionOffset - 0.1f;
                    all_MenusBG[i].DOAnchorMax(new Vector2(startingPosition, .9f), animationDuration);

                    all_MenuPanel[i].SetActive(false);
                }
            }
        }





    }
}
