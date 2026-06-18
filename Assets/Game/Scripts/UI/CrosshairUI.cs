using System;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] Color _normalColor =  Color.white;
    [SerializeField] Color _highlightColor =  Color.white;
    [SerializeField] private Image _crosshairImage;

    private void Awake()
    {
        SetHighlight(false);
    }

    private void SetHighlight(bool value)
    {
        if (value)
        {
            _crosshairImage.color = _highlightColor;
        }
        else
        {
            _crosshairImage.color = _normalColor;
        }
    }
    public void CallSetHighlight(bool value){SetHighlight(value);}
}
