using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100;//var for max stamina
    [SerializeField] private float _sprintStaminaCost = 20;//var for stamina cost
    [SerializeField] private float _staminaRegenValue = 20;//var for stamina regen value
    private float _currentStamina;//stamina value state
    [SerializeField] private PlayerCharacterMovement _characterMovement;//is called to get the is sprinting bool value

    private void Awake()
    {
        _currentStamina = _maxStamina;//set current stamina to max
    }

    private void CalculateStamina()//calculates stamina
    {
        if (_characterMovement.IsSprinting)//if is sprinting
        {
            if (_currentStamina > 0)//if current stamina above 0
            {
                _currentStamina -= _sprintStaminaCost * Time.deltaTime;//decrease stamina overtime
            }
            else _characterMovement.CallSetSprinting(false);//out of stamina, set is sprinting to false
        }
        else _currentStamina += _staminaRegenValue * Time.deltaTime;//increase stamina overtime
        
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);//set current stamina cap
    }

    private void Update()
    {
        CalculateStamina();
    }
}
