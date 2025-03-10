using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixBtn : MonoBehaviour
{
    [SerializeField] GameObject mixPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject randomPanel;


    public void ClickMixPanelBtn()
    {
        if(upgradePanel.activeSelf) upgradePanel.SetActive(false);
        if (randomPanel.activeSelf) randomPanel.SetActive(false);

        UIManager.instance.OpenPanel(mixPanel, null, null, null, false);
    }

    public void ClickMixSpawnBtn()
    {
        StartCoroutine(Shared.unitManager.UnitMixer.StartUnitMixSpawn());
    }
}
