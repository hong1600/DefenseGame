using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit
{
    public UnitData unitData;
    public bool i;

    private void Awake()
    {
        Init(unitData);
    }
}
