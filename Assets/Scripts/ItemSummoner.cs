using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSummoner : Singleton<ItemSummoner>
{
    public static event Action<SummonItem?> OnSummonSelectChange;

    [field: SerializeField]
    public List<SummonItemSettings> SummonItemSettings { get; private set; } = new List<SummonItemSettings>();

    [SerializeField]
    private LayerMask _terrainMask; 

    private SummonItemSettings _selectedSummon = null;
    private GameObject _previewObject = null;
    private BoxCollider2D _previewCollider = null;
    private SpriteRenderer _previewSpriteRenderer = null;

    private void Update()
    {
        HandleInput();

        if (_previewObject != null)
        {
            var mousePosition = GetMouseWordPosition();

            _previewObject.transform.position =  mousePosition;

            bool canPlace = CanPlaceSummon();

            if (canPlace)
            {
                SetPreviewColor(Color.green);
            }
            else
            {
                SetPreviewColor(Color.red);
            }
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
        OnSummonSelectChange?.Invoke(item);
    }

    public void UnselectToBeSummoned()
    {
        _selectedSummon = null;
        ClearPreview();
        OnSummonSelectChange?.Invoke(null);
    }

    private SummonItem GetItemLabelWithKey(KeyCode keyCode)
    {
        var itemLabel = SummonItemSettings.FirstOrDefault(itemSettings => itemSettings.KeyBind == keyCode).SummonItemLabel;

        return itemLabel;
    }

    private void CreatePreview(SummonItemSettings itemSettings)
    {
        var previewObject = new GameObject();
        _previewSpriteRenderer = previewObject.AddComponent<SpriteRenderer>();
        _previewSpriteRenderer.sprite = itemSettings.PreviewSprite;
        _previewSpriteRenderer.sortingOrder = 100;
        SetPreviewColor(Color.green);
        _previewCollider = previewObject.AddComponent<BoxCollider2D>();
        _previewCollider.isTrigger = true;
        _previewCollider.size = new Vector2(0.8f, 0.8f);

        _previewObject = previewObject;
    }

    private void SetPreviewColor(Color color)
    {
        _previewSpriteRenderer.color = new Color(color.r, color.g, color.b, 0.55f);
    }

    private void ClearPreview()
    {
        if (_previewObject != null)
        {
            Destroy(_previewObject);
            _previewObject = null;
        }
    }

    private bool CanPlaceSummon()
    {
        var terrainCheckbounds = _previewCollider.bounds;

        bool _isHittingTerrain =
            Physics2D.OverlapAreaAll(
                terrainCheckbounds.min,
                terrainCheckbounds.max,
                _terrainMask
            ).Length > 0;

        return !_isHittingTerrain;
    }

    private void PlaceSelectedSummon()
    {
        if (_selectedSummon == null 
            || InventoryManager.Instance.CanUseItem(_selectedSummon.SummonItemLabel) == false
            || CanPlaceSummon() == false)
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
