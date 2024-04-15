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
    private Button _button;

    [SerializeField]
    private TextMeshProUGUI _hotkeyText;

    [SerializeField]
    private TooltipTrigger _tooltipTrigger;

    [SerializeField]
    private TextMeshProUGUI _amountText;

    private SummonItemSettings _summonItemSettings;

    private void Awake()
    {
        InventoryManager.OnInventoryChanged += UpdateAmount;
        ItemSummoner.OnSummonSelectChange += HandleSummonSelectChange;
    }

    private void OnDestroy()
    {
        InventoryManager.OnInventoryChanged -= UpdateAmount;
        ItemSummoner.OnSummonSelectChange -= HandleSummonSelectChange;
    }

    public void HandleClickFromUI()
    {
        ItemSummoner.Instance.SelectToBeSummoned(_summonItemSettings.SummonItemLabel);
    }

    public void SetupSettings(SummonItemSettings settings)
    {
        _summonItemSettings = settings;
        _image.sprite = settings.PreviewSprite;
        _hotkeyText.text = settings.KeybindText;
        _tooltipTrigger.SetTooltipData(settings.TooltipHeader, settings.TooltipContent);
        UpdateAmount();
    }

    private void UpdateAmount()
    {
        _amountText.text = InventoryManager.Instance.GetItemAmount(_summonItemSettings.SummonItemLabel).ToString();
    }

    private void HandleSummonSelectChange(SummonItem? item)
    {
        if (item == _summonItemSettings.SummonItemLabel)
        {
            _button.Select();
        }
    }
}
