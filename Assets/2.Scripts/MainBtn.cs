using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainBtn : MonoBehaviour
{
    [SerializeField] GameObject heroPanel;
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject treasurePanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] Image bgmImg;
    [SerializeField] Image sfxImg;
    [SerializeField] MainUI mainUI;

    bool option;
    bool bgm;
    bool sfx;

    private void Start()
    {
        option = false;
        bgm = false;
        sfx = false;
    }

    public void StartBtn()
    {
        LoadScene.loadScene(2);
    }

    public void closeBtn(GameObject button)
    {
        Transform buttonParent = button.transform.parent;

        if (buttonParent != null)
        {
            buttonParent.gameObject.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                buttonParent.gameObject.SetActive(false);
            });
        }
    }

    public void mainBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(false);
        treasurePanel.SetActive(false);
    }

    public void heroBtn()
    {
        heroPanel.SetActive(true);
        storePanel.SetActive(false);
        treasurePanel.SetActive(false);
    }

    public void storeBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(true);
        treasurePanel.SetActive(false);
    }

    public void treasureBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(false);
        treasurePanel.SetActive(true);
    }

    public void treasureDcBtn(int index)
    {
        mainUI.treasureDc(index);
    }

    public void treasureUpgradeBtn()
    {
        mainUI.treasureUpgrade();
    }

    public void optionBtn() 
    {
        if (option == false)
        {
            optionPanel.SetActive(true);
            option = true;
        }
        else
        {
            optionPanel.SetActive(false);
            option = false;
        }
    }

    public void settingBtn()
    {
        settingPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void bgmBtn()
    {
        if (bgm == false)
        {
            bgmImg.color = new Color(1, 0, 0, 1);
            bgm = true;
        }
        else
        {
            bgmImg.color = new Color(1, 0, 0, 0);
            bgm = false;
        }
    }

    public void sfxBtn()
    {
        if (sfx == false)
        {
            sfxImg.color = new Color(1, 0, 0, 1);
            sfx = true;
        }
        else
        {
            sfxImg.color = new Color(1, 0, 0, 0);
            sfx = false;
        }
    }

    public void heroUpgradeBtn()
    {
        mainUI.HeroUpgrade();
    }

    public void storeUnitBtn(int index, string Name)
    {
        mainUI.storeSlotClick(index, Name);
    }

    public void storeResetBtn()
    {
        if (DataManager.instance.playerdata.gold >= 1000)
        {
            mainUI.storeSlotReset();
            DataManager.instance.playerdata.gold -= 1000;
        }
    }

    public void storeBuyBtn()
    {
        mainUI.storeBuy();
    }
}
