using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static GameInputAction;

//Call Interface "IPlayerActions" to detect action input from action map "Player"
//"IPlayerAction" Interface is declared in "GameInputAction" class generated from its "input action" file
public class InputManager : MonoBehaviour, IPlayerActions
{
    private GameInputAction _inputAction;//declare _inputAction as GameInputAction
    public UnityEvent<Vector2> OnMoveInput;//declare OnMoveInput as UnityEvent with Vector2 data type
    public UnityEvent<bool> OnSprintInput;//declare OnSprintInput as UnityEvent with bool data type

    private void Awake()//runs before Start and before any GameObject is active
    {
        _inputAction = new GameInputAction();//creating object for "GameInputAction"
        _inputAction.Enable();//activating "GameInputAction"
        _inputAction.Player.Enable();//activating action map "Player"
        _inputAction.Player.SetCallbacks(this);//telling that "InputManager" class will detect an input from action map "Player"
        
    }

    public void OnInteract(InputAction.CallbackContext context)//when "Interact" input is triggered
    {
        if (context.performed)//".performed" -> a bool var to detect whether the context is performed or not
        {
            Debug.Log("Interacting");
        }
    }

    public void OnMove(InputAction.CallbackContext context)//when "Move" input is triggered
    {
        //Debug.Log(context.ReadValue<Vector2>());//display Vector2 value based on input
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());//invoke and send Vector2 data to listener
    }

    public void OnSprint(InputAction.CallbackContext context)//when "Sprint" input is triggered
    {
        if (context.performed)//pressed
        {
            OnSprintInput?.Invoke(true);//invoke and send bool data to listener
        }
        if (context.canceled)//released
        {
            OnSprintInput?.Invoke(false);//invoke and send bool data to listener
        }
    }
}
