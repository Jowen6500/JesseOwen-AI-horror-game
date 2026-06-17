using System.Collections;
using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100;//var for max stamina
    [SerializeField] private float _sprintStaminaCost = 20;//var for stamina cost
    [SerializeField] private float _staminaRegenValue = 20;//var for stamina regen value
    private float _currentStamina;//stamina value state
    [SerializeField] private PlayerCharacterMovement _characterMovement;//is called to get the is sprinting bool value
    private Coroutine _stopRegenStaminaCoroutine;
    private bool _isWaitingStaminaRegen;
    [SerializeField] private float _disableStaminaBarAfter = 1;
    
    private void Awake()
    {
        _currentStamina = _maxStamina;//set current stamina to max
        
        HUDManager.Instance.StaminaUI.CallSetStaminaFill(_currentStamina, _maxStamina);//initialize set stamina fill function from StaminUI
    }

    private void CalculateStamina()//calculates stamina
    {
        if (_characterMovement.IsSprinting)//if (is sprinting)
        {
            if (_stopRegenStaminaCoroutine != null)//if (sprinting) and (stopRegenStaminaCoroutine has value)
            {
                StopCoroutine(_stopRegenStaminaCoroutine);//stop coroutine of stopRegenStaminaCoroutine
                _stopRegenStaminaCoroutine = null;//set stopRegenStaminaCoroutine null
            }
            
            _isWaitingStaminaRegen = false;//set _isWaitingStaminaRegen false
            
            if (_currentStamina > 0)//if (sprinting) and (current stamina > 0)
            {
                _currentStamina -= _sprintStaminaCost * Time.deltaTime;//decrease stamina overtime
            }
            else _characterMovement.CallSetSprinting(false);//if (sprinting) and (out of stamina), set is sprinting to false
        }
        else//if not sprinting
        {
            if (_currentStamina < _maxStamina)//if (not sprinting) and (current stamina < max stamina)
            {
                _currentStamina += _staminaRegenValue * Time.deltaTime; //increase stamina overtime
            }
            else if (!_isWaitingStaminaRegen)//if (not sprinting) and (current stamina = max stamina) and (_isWaitingStaminaRegen is false)
            {
                _stopRegenStaminaCoroutine = StartCoroutine(StopRegenStaminaWait());//start coroutine of stopRegenStaminaCoroutine
                _isWaitingStaminaRegen = true;//set _isWaitingStaminaRegen true
            }
        }
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);//set current stamina cap
        
        HUDManager.Instance.StaminaUI.CallSetStaminaFill(_currentStamina, _maxStamina);//call set stamina fill function from StaminUI
    }

    private void Update()
    {
        CalculateStamina();
    }

    private IEnumerator StopRegenStaminaWait()
    {
        yield return new WaitForSeconds(_disableStaminaBarAfter);//wait for x seconds
        HUDManager.Instance.StaminaUI.StaminaBG.CrossFadeAlpha(0, 1, false);//invoke crossfade StaminaBG Alpha to 0
        HUDManager.Instance.StaminaUI.StaminaFill.CrossFadeAlpha(0, 1, false);//invoke crossfade StaminaFill Alpha to 0
        //HUDManager.Instance.StaminaUI.CallSetVisible(false);//deactivate stamina bar
    }
}
