using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMixSpawnBtn : MonoBehaviour
{
    public void ClickMixSpawn()
    {
        StartCoroutine(Shared.unitMng.iUnitMixer.StartUnitMixSpawn());
    }
}