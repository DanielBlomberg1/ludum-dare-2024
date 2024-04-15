using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string _header;
    private string _content;

    private IEnumerator _coroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _coroutine = TooltipSystem.Instance.Show(_header, _content);
        StartCoroutine(_coroutine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.Hide();
        StopCoroutine(_coroutine);
    }

    public void SetTooltipData(string header = "", string content = "")
    {
        _header = header;
        _content = content;
    }
}
