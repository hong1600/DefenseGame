using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Canvas worldCanvas;

    public UIEnemyCounter uiEnemyCounter;
    public IUIEnemyCounter iUIEnemyCounter;
    public UIRoundPanel uiRoundPanel;
    public IUIRoundPanel iUIRoundPanel;
    public UITimePanel uiTimePanel;
    public IUITimePanel iUITimePanel;
    public UIMixRightSlot uiMixRightSlot;
    public IUIMixRightSlot iUIMixRightSlot;
    public UIFusionBtn uiFusionBtn;
    public IUIFusionBtn iUIFusionBtn;

    private void Awake()
    {
        if (Shared.gameUI == null)
        {
            Shared.gameUI = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        iUIEnemyCounter = uiEnemyCounter;
        iUIRoundPanel = uiRoundPanel;
        iUITimePanel = uiTimePanel;
        iUIMixRightSlot = uiMixRightSlot;
        iUIFusionBtn = uiFusionBtn;
    }
}
