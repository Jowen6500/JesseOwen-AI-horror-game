using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private PlayerCharacterMovement _movement;//var to ref "PlayerCharacterMovement" script/module/class
    [SerializeField] private PlayerCharacterStamina _stamina;//var to ref PlayerCharacterStamina script/module/class
    [SerializeField] private InventoryManager _inventoryManager;//var to ref InventoryManager script/module/class
    [SerializeField] private InteractDetector _interactDetector;//var to ref "InteractDetector" script/module/class
    [SerializeField] private CameraManager _cameraManager;//var to ref "CameraManager" script/module/class
    [SerializeField] private InputManager _inputManager;//var to ref "InputManager" script/module/class
    [SerializeField] private Flashlight _flashlight;
    public PlayerCharacterMovement Movement => _movement;//to get "_movement" value/data
    public PlayerCharacterStamina Stamina => _stamina;//to get "_stamina" value/data
    public InventoryManager Inventory => _inventoryManager;//to get "_inventoryManager" value/data
    public InteractDetector InteractDetector => _interactDetector;//to get "_interactDetector" value/data
    public CameraManager Camera => _cameraManager;//to get "_cameraManager" value/data
    public InputManager Input => _inputManager;//to get "_inputManager" value/data
    public Flashlight Flashlight => _flashlight;
    
    public bool IsHiding { get; private set; }//create property to determine player's hiding status
    private void SetIsHiding(bool isHiding)//method to change player's hiding status
    {
        IsHiding = isHiding;
    }
    public void CallSetIsHiding(bool isHiding){ SetIsHiding(isHiding); }//called on HidingCloset module/class
    
    private void Awake()
    {
        Cursor.visible = false;//set cursor visibility to false
        Cursor.lockState = CursorLockMode.Locked;//set cursor state to locked(to the middle of the screen)
    }
}
