using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerDataMng playerDataMng;
    public SaveLoadMng saveLoadMng;
    public UnitDataMng unitDataMng;
    public ItemDataMng itemDataMng;

    public PlayerData playerdata = new PlayerData();
    public List<ItemData> item = new List<ItemData>();
    public List<UnitData> unit = new List<UnitData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        playerDataMng = new PlayerDataMng();
        saveLoadMng = new SaveLoadMng();
        unitDataMng = new UnitDataMng();
        itemDataMng = new ItemDataMng();

        saveLoadMng.loadData();
        playerDataMng.initialized();
    }
}

