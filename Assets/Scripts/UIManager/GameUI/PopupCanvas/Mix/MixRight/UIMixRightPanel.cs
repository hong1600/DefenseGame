using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMixRightPanel : MonoBehaviour
{
    public List<UnitData> mixUnitDataList = new List<UnitData>();

    private void Start()
    {
        mixUnitDataList = DataMng.instance.unitDataLoader.GetUnitData(EUnitGrade.SS);

        UnitData curUnit = mixUnitDataList[0];
    }

    public void CurMixPanel(int _index)
    {
        UnitData curUnit = mixUnitDataList[_index];
    }
}