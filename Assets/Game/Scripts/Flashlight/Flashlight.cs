using System;
using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light _light;//var to ref light component
    [SerializeField] private PlayerCharacter _playerCharacter;//var to ref PlayerCharacter
    [SerializeField] private float _initialBatteryLevel = 100;//var to set battery initial max charge
    [SerializeField] private float _batteryDrainRate = 1;//var to set battery drain rate
    private float _currentBatteryLevel;//var to ref current battery level
    private Coroutine _afterFlashlightOffWaitCoroutine;
    private bool _isWaitingBatteryOff;
    [SerializeField] private float _disableBatteryBarAfter = 1;
    
    public bool HasFlashlight => _playerCharacter.Inventory.CheckItem("flashlight_01");//property to get check flashlight from inventory
    public bool HasBattery => _currentBatteryLevel > 0;//property to check battery state

    private void Awake()
    {
        _currentBatteryLevel = _initialBatteryLevel;//initialize current battery
        HUDManager.Instance.BatteryUI.CallUpdateBatteryFill(_currentBatteryLevel, _initialBatteryLevel);
    }

    private void UseFlashlight()//method to turn on/off flashlight
    {
        if (HasFlashlight && _light != null)//if has flashlight and light component not null
        {
            if (HasBattery)//if has battery
            {
                _light.enabled = !_light.enabled;//change flashlight state on/off

                if (_light.enabled)//if (has flashlight) and (light component has value) and (has battery) and (lights on)
                {
                    if (_afterFlashlightOffWaitCoroutine != null)//if (sprinting) and (stopRegenStaminaCoroutine has value)
                    {
                        StopCoroutine(_afterFlashlightOffWaitCoroutine);//stop coroutine of stopRegenStaminaCoroutine
                        _afterFlashlightOffWaitCoroutine = null;//set stopRegenStaminaCoroutine null
                    }
                    _isWaitingBatteryOff = false;
                    HUDManager.Instance.BatteryUI.BatteryBG.CrossFadeAlpha(1, 0.5f, false);//invoke crossfade StaminaBG Alpha to 1
                    HUDManager.Instance.BatteryUI.BatteryFill.CrossFadeAlpha(1, 0.5f, false);//invoke crossfade StaminaFill Alpha to 1
                }
                else//if (has flashlight) and (light component has value) and (has battery) and (lights off)
                {
                    if (!_isWaitingBatteryOff)
                    {
                        _afterFlashlightOffWaitCoroutine = StartCoroutine(AfterFlashlightOffWait());//start coroutine of stopRegenStaminaCoroutine
                        _isWaitingBatteryOff = true;
                    }
                }
                
                return;
            }
            _light.enabled = false;//set flashlight off
            Debug.Log("No Battery");
            return;
        }
        Debug.Log("No Flashlight");
    }
    public void CallUseFlashlight() {UseFlashlight();}//called in inspector
    
    private void UpdateFlashlightRotation()//method to update flashlight rotation
    {
        _light.transform.rotation = Camera.main.transform.rotation;//set flashlight object rotation to camera's rotation
    }

    private void UpdateBatteryLevel()//method to update current battery level
    {
        if (_light != null && _light.enabled)//if light is not null and flashlighe is on
        {
            if (HasBattery)//if has battery
            {
                _currentBatteryLevel -= Time.deltaTime * _batteryDrainRate;//drain current battery
            }
            else
            {
                _currentBatteryLevel = 0;//set current battery level to 0
                _light.enabled = false;//set flashlight off
            }
            HUDManager.Instance.BatteryUI.CallUpdateBatteryFill(_currentBatteryLevel, _initialBatteryLevel);
        }
    }

    private void RefillBatteryLevel(float batteryLevel)//method to add current battery level
    {
        _currentBatteryLevel += batteryLevel;//add battery
        _currentBatteryLevel = Mathf.Clamp(_currentBatteryLevel, 0, _initialBatteryLevel);//cap min and max battery level
        HUDManager.Instance.BatteryUI.CallUpdateBatteryFill(_currentBatteryLevel, _initialBatteryLevel);

    }
    public void CallRefillBatteryLevel(float batteryLevel){RefillBatteryLevel(batteryLevel);}//called in Battery module

    /*
    private void SetBatteryLevel()
    {
        _currentBatteryLevel = _initialBatteryLevel;
    }
    */
    
    private void Update()
    {
        UpdateFlashlightRotation();//call method
        UpdateBatteryLevel();//call method
    }
    
    private IEnumerator AfterFlashlightOffWait()
    {
        yield return new WaitForSeconds(_disableBatteryBarAfter);//wait for x seconds
        HUDManager.Instance.BatteryUI.BatteryBG.CrossFadeAlpha(0, 1, false);//invoke crossfade BatteryBG Alpha to 0
        HUDManager.Instance.BatteryUI.BatteryFill.CrossFadeAlpha(0, 1, false);//invoke crossfade BatteryFill Alpha to 0
    }
}
