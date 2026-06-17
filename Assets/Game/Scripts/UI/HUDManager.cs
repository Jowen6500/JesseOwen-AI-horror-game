using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private StaminaUI _staminaUI;
    [SerializeField] private BatteryUI _batteryUI;
    private static HUDManager _instance;
    
    public StaminaUI StaminaUI => _staminaUI;
    public BatteryUI BatteryUI => _batteryUI;
    public static HUDManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
}
