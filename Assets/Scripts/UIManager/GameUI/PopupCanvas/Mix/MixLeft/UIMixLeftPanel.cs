using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixLeftPanel : MonoBehaviour
{
    public Transform mixLeftContent;
    public GameObject MixBtnPre;

    private void Awake()
    {
        LoadLeftPanel();
    }

    private void LoadLeftPanel()
    {
        for (int i = 0; i < DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS).Count; i++)
        {
            GameObject newBtn = Instantiate(MixBtnPre, mixLeftContent);
            UISetLeftSlot mixslot = newBtn.GetComponent<UISetLeftSlot>();
            mixslot.SetUnit(DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS)[i], i);
        }
    }
}