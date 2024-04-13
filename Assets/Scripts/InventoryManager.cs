using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : PersistentSingleton<InventoryManager>
{
    public Dictionary<SummonItem, int> Items { get; private set; } = new Dictionary<SummonItem, int>();

    protected override void Awake()
    {
        GameManager.OnLevelLoaded += LoadInventory;

        base.Awake();
    }

    private void OnDestroy()
    {
        GameManager.OnLevelLoaded -= LoadInventory;
    }

    public void AddItem(SummonItem item, int amount = 1)
    {
        Items[item] += amount;
    }
    
    public void RemoveItem(SummonItem item, int amount = 1)
    {
        Items[item] -= amount;
    }

    public void Clear()
    {
        Items = new Dictionary<SummonItem, int>();
    }

    private void LoadInventory()
    {
        Clear();

        var currentSettings = GameManager.Instance.GetCurrentLevelSettings();

        if (currentSettings.SpringAmount > 0)
        {
            Items.Add(SummonItem.Spring, currentSettings.SpringAmount);
        }

        if (currentSettings.DrillAmount > 0)
        {
            Items.Add(SummonItem.Drill, currentSettings.DrillAmount);
        }

        if (currentSettings.PickaxeAmount > 0)
        {
            Items.Add(SummonItem.Pickaxe, currentSettings.PickaxeAmount);
        }

        if (currentSettings.BridgeAmount > 0)
        {
            Items.Add(SummonItem.Bridge, currentSettings.BridgeAmount);
        }

        if (currentSettings.WaterAmount > 0)
        {
            Items.Add(SummonItem.Water, currentSettings.WaterAmount);
        }
    }
}