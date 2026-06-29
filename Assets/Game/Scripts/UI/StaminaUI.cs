using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private GameObject _staminaBar;
    [SerializeField] private Image _staminaBG;
    [SerializeField] private Image _staminaFill;

    public Image StaminaBG
    {
        get => _staminaBG;
        set => _staminaBG = value;
    }

    public Image StaminaFill
    {
        get => _staminaFill;
        set => _staminaFill = value;
    }
    
    private void SetVisible(bool value)
    {
        _staminaBar?.SetActive(value);
    }
    public void CallSetVisible(bool value){SetVisible(value);}
    
    private void SetStaminaFill(float currentValue, float maxValue)
    {
        if (_staminaFill != null)
        {
            _staminaFill.fillAmount = currentValue / maxValue;
        }
    }
    public void CallSetStaminaFill(float currentValue, float maxValue){SetStaminaFill(currentValue, maxValue);}
}
