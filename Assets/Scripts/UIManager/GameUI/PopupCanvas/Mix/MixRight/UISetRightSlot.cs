using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetRightSlot : MonoBehaviour
{
    [SerializeField] Image unitImg;

    public void SetUnit(TableUnit.Info _unitdata)
    {
        unitImg.sprite = Resources.Load<Sprite>(_unitdata.ImgPath);
    }
}
