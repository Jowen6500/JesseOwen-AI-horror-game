using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private CrosshairUI _crosshairUI;
    [SerializeField] private InteractionUI _interactionUI;
    [SerializeField] private StaminaUI _staminaUI;
    [SerializeField] private BatteryUI _batteryUI;
    private static HUDManager _instance;
    
    public CrosshairUI CrosshairUI => _crosshairUI;
    public InteractionUI InteractionUI => _interactionUI;
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
