using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    [SerializeField] private GameObject _BatteryBar;
    [SerializeField] private Image _batteryBG;
    [SerializeField] private Image _batteryFill;
    [SerializeField] private Color _highColor = Color.green;
    [SerializeField] private Color _mediumColor = Color.yellow;
    [SerializeField] private Color _lowColor = Color.red;

    public Image BatteryBG
    {
        get => _batteryBG;
        set => _batteryBG = value;
    }

    public Image BatteryFill
    {
        get => _batteryFill;
        set => _batteryFill = value;
    }
    
    private void SetVisible(bool value)
    {
        _BatteryBar?.SetActive(value);
    }
    public void CallSetVisible(bool value){SetVisible(value);}
    
    private void UpdateBatteryFill(float currentValue, float maxValue)
    {
        if (_batteryFill != null)
        {
            float fillAmount = currentValue / maxValue;
            _batteryFill.fillAmount = fillAmount;
            Color color = _highColor;
            
            if (fillAmount > 0.25f && fillAmount < 0.5f)
            {
                color = _mediumColor;
            }
            else if (fillAmount < 0.25f)
            {
                color = _lowColor;
            }
            _batteryFill.color = color;
        }
    }
    public void CallUpdateBatteryFill(float currentValue, float maxValue){UpdateBatteryFill(currentValue, maxValue);}
}
