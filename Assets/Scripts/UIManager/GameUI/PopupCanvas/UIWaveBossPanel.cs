using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveBossPanel : MonoBehaviour
{
    public TextMeshProUGUI waveBossLevelNameText;

    public void waveBossPanel()
    {
        waveBossLevelNameText.text = $"LV.{Shared.enemyMng.iWaveBossSpawner.GetWaveBossLevel()} �� ��";
    }
}