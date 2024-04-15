using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem>
{
    [SerializeField]
    private Tooltip _tooltip;

    [SerializeField]
    private float _tooltipDelay = 0.5f;

    public IEnumerator Show(string header = "", string content = "")
    {
        _tooltip.SetText(header, content);
        yield return new WaitForSeconds(_tooltipDelay);
        _tooltip.gameObject.SetActive(true);
    }

    public void Hide() 
    { 
        _tooltip.gameObject.SetActive(false);
    }
}
