using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonHotbar : MonoBehaviour
{
    [SerializeField]
    private GameObject _hotbarItemPrefab;

    private List<GameObject> _hotbarItems = new List<GameObject>();

    private void Start()
    {
        SetupHotbar();
    }

    private void SetupHotbar()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var summonItemSettings = ItemSummoner.Instance.SummonItemSettings;

        if (summonItemSettings == null)
        {
            Debug.LogWarning("Could not get summon item settings");
            return;
        }

        foreach (var item in summonItemSettings)
        {
            var newHotbarItem = GameObject.Instantiate(_hotbarItemPrefab);
            newHotbarItem.transform.SetParent(transform, false);
            newHotbarItem.GetComponent<SummonHotbarItem>().SetupSettings(item);

            _hotbarItems.Add(newHotbarItem);
        }
    }
}
