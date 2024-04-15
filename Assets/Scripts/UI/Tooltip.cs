using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _header;

    [SerializeField]
    private TextMeshProUGUI _content;

    [SerializeField]
    private LayoutElement _layoutElement;

    [SerializeField]
    private int _characterWrapLimit;

    [SerializeField]
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = mousePosition;
    }

    public void SetText(string header = "", string content = "")
    {
        _header.text = header;
        _content.text = content;

        int headerLength = _header.text.Length;
        int contentLength = _content.text.Length;

        _layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) ? true : false;
    }
}
