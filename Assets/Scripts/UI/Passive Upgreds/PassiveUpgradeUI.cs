using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgradeUI : MonoBehaviour
{
    public GameObject btn_Upgrade;
    public PassiveUpgradeSelectonUI ui_PassiveUpgradeSelection;
    public PassiveUpgradeSummaryUI ui_PassiveUpgradeSummary;


    private void OnEnable()
    {
        ui_PassiveUpgradeSelection.gameObject.SetActive(true);
        btn_Upgrade.SetActive(true);
        ui_PassiveUpgradeSummary.gameObject.SetActive(false);
    }

}
