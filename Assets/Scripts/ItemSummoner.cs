using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemSummoner : Singleton<ItemSummoner>
{
    [field: SerializeField]
    public List<SummonItemSettings> SummonItemSettings { get; private set; } = new List<SummonItemSettings>();

    private SummonItemSettings _selectedSummon = null;
    private GameObject _previewObject = null;

    private void Update()
    {
        HandleInput();

        if (_previewObject != null)
        {
            var mousePosition = GetMouseWordPosition();

            _previewObject.transform.position =  mousePosition;
        }
    }

    private void HandleInput()
    {
        if (GameManager.Instance.CurrentState != GameState.Play)
        {
            return;
        }

        SummonItem? itemLableWithKeybind = null;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            itemLableWithKeybind = GetItemLabelWithKey(KeyCode.Alpha1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            itemLableWithKeybind = GetItemLabelWithKey(KeyCode.Alpha2);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            itemLableWithKeybind = GetItemLabelWithKey(KeyCode.Alpha3);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            itemLableWithKeybind = GetItemLabelWithKey(KeyCode.Alpha4);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            itemLableWithKeybind = GetItemLabelWithKey(KeyCode.Alpha5);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlaceSelectedSummon();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            UnselectToBeSummoned();
        }

        if (itemLableWithKeybind != null)
        {
            SelectToBeSummoned(itemLableWithKeybind.Value);
        }
    }

    public void SelectToBeSummoned(SummonItem item)
    {
        ClearPreview();

        if (InventoryManager.Instance.CanUseItem(item) == false)
        {
            return;
        }

        _selectedSummon = SummonItemSettings.FirstOrDefault(itemSettings => itemSettings.SummonItemLabel == item);

        CreatePreview(_selectedSummon);
    }

    public void UnselectToBeSummoned()
    {
        _selectedSummon = null;
        ClearPreview();
    }

    private SummonItem GetItemLabelWithKey(KeyCode keyCode)
    {
        var itemLabel = SummonItemSettings.FirstOrDefault(itemSettings => itemSettings.KeyBind == keyCode).SummonItemLabel;

        return itemLabel;
    }

    private void CreatePreview(SummonItemSettings itemSettings)
    {
        var previewObject = new GameObject();
        var spriteComponent = previewObject.AddComponent<SpriteRenderer>();
        spriteComponent.sprite = itemSettings.PreviewSprite;
        spriteComponent.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.4f);

        _previewObject = previewObject;
    }

    private void ClearPreview()
    {
        if (_previewObject != null)
        {
            Destroy(_previewObject);
            _previewObject = null;
        }
    }

    private void PlaceSelectedSummon()
    {
        if (_selectedSummon == null || InventoryManager.Instance.CanUseItem(_selectedSummon.SummonItemLabel) == false)
        {
            return;
        }

        GameObject.Instantiate(_selectedSummon.ItemWorldPrefab, GetMouseWordPosition(), Quaternion.identity);
        InventoryManager.Instance.RemoveItem(_selectedSummon.SummonItemLabel);

        UnselectToBeSummoned();
    }

    private Vector3 GetMouseWordPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
