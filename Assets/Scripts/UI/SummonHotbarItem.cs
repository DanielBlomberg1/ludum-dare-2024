using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonHotbarItem : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private TextMeshProUGUI _hotkeyText;

    [SerializeField]
    private TextMeshProUGUI _amountText;

    private SummonItemSettings _summonItemSettings;

    private void Awake()
    {
        InventoryManager.OnInventoryChanged += UpdateAmount;   
    }

    private void OnDestroy()
    {
        InventoryManager.OnInventoryChanged -= UpdateAmount;
    }

    public void SetupSettings(SummonItemSettings settings)
    {
        _summonItemSettings = settings;
        _image.sprite = settings.PreviewSprite;
        _hotkeyText.text = settings.KeybindText;
        UpdateAmount();
    }

    private void UpdateAmount()
    {
        _amountText.text = InventoryManager.Instance.GetItemAmount(_summonItemSettings.SummonItemLabel).ToString();
    }
}
