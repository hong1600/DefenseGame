using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeBtn : MonoBehaviour
{
    public GameObject upgradePanel;

    public void clickUpgradeBtn()
    {
        BtnManager.instance.openPanel(upgradePanel);
    }
}