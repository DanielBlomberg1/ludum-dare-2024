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

    public void SetupSettings(SummonItemSettings settings)
    {
        _summonItemSettings = settings;
        _image.sprite = settings.PreviewSprite;
        _hotkeyText.text = settings.KeybindText;

        int amountText;
        
        if (InventoryManager.Instance.Items.TryGetValue(settings.SummonItemLabel, out amountText))
        {
            _amountText.text = amountText.ToString();
        }
        else
        {
            _amountText.text = "0";
        }
    }
}
