using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataMng
{
    public List<UnitData> unit = new List<UnitData>();

    public UnitDataMng()
    {
        unit = DataManager.instance.unit;
    }

    public void loadUnitState(PlayerData playerdata)
    {
        for (int i = 0; i < playerdata.units.Count; i++)
        {
            UnitState saveUnit = playerdata.units[i]; 
            UnitData baseUnit = unit.Find(u => u.index == saveUnit.index);

            if (baseUnit != null)
            {
                UnitState loadUnit = playerdata.units[i];
                loadUnit.index = saveUnit.index;
                loadUnit.unitName = saveUnit.unitName;
                loadUnit.unitDamage = saveUnit.unitDamage;
                loadUnit.attackSpeed = saveUnit.attackSpeed;
                loadUnit.attackRange = saveUnit.attackRange;
                loadUnit.unitLevel = saveUnit.unitLevel;
                loadUnit.unitCurExp = saveUnit.unitCurExp;
                loadUnit.unitMaxExp = saveUnit.unitMaxExp;
                loadUnit.unitUpgradeCost = saveUnit.unitUpgradeCost;
                loadUnit.unitStoreCost = saveUnit.unitStoreCost;
                loadUnit.skill1Name = saveUnit.skill1Name;
                loadUnit.unitGrade = saveUnit.unitGrade;
                loadUnit.unitImg = Resources.Load<Sprite>("Units/" + saveUnit.index.ToString());
                playerdata.units[i] = loadUnit;
            }
        }
    }

    public void InitializeUnits(PlayerData playerData)
    {
        if (playerData.units == null)
        {
            playerData.units = new List<UnitState>();
        }

        foreach (var unitData in DataManager.instance.unit)
        {
            UnitState unitState = new UnitState(unitData);
            playerData.units.Add(unitState);
        }
    }
}