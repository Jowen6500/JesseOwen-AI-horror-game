using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private PlayerCharacterMovement _movement;//var to ref "PlayerCharacterMovement" script/module/class
    public PlayerCharacterMovement Movement => _movement;//to get "_movement" value
    
    [SerializeField] private PlayerCharacterStamina _stamina;//var to ref PlayerCharacterStamina script/module/class
    public PlayerCharacterStamina Stamina => _stamina;//to get "_stamina" value
    
    [SerializeField] private InventoryManager _inventory;//var to ref InventoryManager script/module/class
    public InventoryManager Inventory => _inventory;//to get "_inventory" value
}
