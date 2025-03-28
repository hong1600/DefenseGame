using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : DefenderAI
{
    protected Unit unit;

    public void Init(Unit _unit)
    {
        unit = _unit;
    }

    public override void Attack()
    {
        if (unit.isSkill)
        {
            aiState = EDefenderAI.SKILL;
            return;
        }

        base.Attack();
    }

    public override void Skill()
    {
        if (unit.isSkill == false)
        {
            aiState = EDefenderAI.SEARCH;
            return;
        }

        base.Skill();
    }
}
