using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : Defender
{
    TableTurret.Info info;

    private void Awake()
    {
        info = DataManager.instance.TableTurret.GetTurretData(402);

        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath, true);
    }
}
