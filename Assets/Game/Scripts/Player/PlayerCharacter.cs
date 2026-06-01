using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private PlayerCharacterMovement _movement;//var to ref "PlayerCharacterMovement" script/module/class
    public PlayerCharacterMovement Movement => _movement;//to get "_movement" value/data
    
    [SerializeField] private PlayerCharacterStamina _stamina;//var to ref PlayerCharacterStamina script/module/class
    public PlayerCharacterStamina Stamina => _stamina;//to get "_stamina" value/data
    
    [SerializeField] private InventoryManager _inventory;//var to ref InventoryManager script/module/class
    public InventoryManager Inventory => _inventory;//to get "_inventory" value/data
    
    [SerializeField] private InteractDetector _interactDetector;//var to ref "InteractDetector" script/module/class
    public InteractDetector InteractDetector => _interactDetector;//to get "_interactDetector" value/data
    
    private void Awake()
    {
        Cursor.visible = false;//set cursor visibility to false
        Cursor.lockState = CursorLockMode.Locked;//set cursor state to locked(to the middle of the screen)
    }
}
