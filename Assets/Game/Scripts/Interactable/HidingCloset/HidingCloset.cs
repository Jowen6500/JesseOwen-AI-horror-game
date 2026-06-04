using System;
using System.Collections;
using UnityEngine;

public class HidingCloset : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;//var for object name
    public string Name => _name;//property to get _name value
    
    [SerializeField] private Transform _hidePosition;//var to ref hide position's transform
    [SerializeField] private Transform _unhidePosition;//var to ref hide position's transform
    [SerializeField] private float _duration = 1;//var to determine to hide/unhide duration
    [SerializeField] private Door _door;//var to ref the door object that we'll interact(object that implements door script)
    private PlayerCharacter _playerCharacter;//var to ref player character we'll use to hide
    private Coroutine _hideCoroutine;//var to ref coroutine hide animation
    private Coroutine _unhideCoroutine;//var to ref coroutine unhide animation
    
    public void Interact(PlayerCharacter character)//method interact to hide
    {
        Debug.Log("Interact");
        if (_hidePosition != null && _unhidePosition != null && _door != null)//safe check if ref _hidePosition, _unhidePosition, _door has value
        {
            _playerCharacter = character;//inserting "_hidingPlayer" with "character"
            if (_hideCoroutine != null)//if _hideCoroutine is in progress
            {
                StopCoroutine(_hideCoroutine);//stop _hideCoroutine's coroutine
            }
            _hideCoroutine = StartCoroutine(Hide());//start coroutine to hide(start hiding animation)
        }
    }
    
    public IEnumerator Hide()
    {
        _playerCharacter.CallSetIsHiding(true);//set hiding status to true
        _playerCharacter.Camera.CallSetCameraInputEnable(false);//set camera input false
        _playerCharacter.Movement.CallSetEnabled(false);//set players movement input false
        _playerCharacter.InteractDetector.CallSetEnabled(false);//deactivate interact detector
        //_hidingPlayer.Camera.CallResetCameraRotation();//reset camera rotation
 
        _door.Open();//open hiding spot door
        yield return new WaitWhile(() => _door.IsAnimating);//wait while door animating is true(continue execution when false)
        
        float time = 0f;//float to calculate animation time with "0" as its starting value
        Vector3 startPosition = _playerCharacter.transform.position;//ref player's position when interacting hiding spot
        float startPanRotation = _playerCharacter.Camera.PanAxis;//ref player's camera pan-axis when interacting hiding spot
        float startTiltRotation = _playerCharacter.Camera.TiltAxis;//ref player's camera tilt-axis when interacting hiding spot*
        
        while (time < _duration)//while time is smaller than _interactDuration
        {
            time = time + Time.deltaTime;//add 1 to time every one second
            _playerCharacter.transform.position = Vector3.Lerp(startPosition, _hidePosition.position, time / _duration);//change player's position to targeted position using Lerp
            
            float panAxis = Mathf.Lerp(startPanRotation, _hidePosition.eulerAngles.y, time / _duration);//var to calculate player's pan angle to targeted angle using Lerp
            float tiltAxis = Mathf.Lerp(startTiltRotation, _hidePosition.eulerAngles.z, time / _duration);//var to calculate player's tilt angle to targeted angle using Lerp*
            
            _playerCharacter.Camera.CallSetPanAxisValue(panAxis);//assign panAxis into player's camera pan angle value
            _playerCharacter.Camera.CallSetTiltAxisValue(tiltAxis);//assign tiltAxis into player's camera tilt angle value*
            
            yield return null;//stop here and execute code above on the next frame
        }
        _playerCharacter.transform.position = _hidePosition.position;//after animation ended set player's position to hide position(safe check)
        _playerCharacter.transform.rotation = _hidePosition.rotation;//after animation ended set player's pan rotation to hide rotation(safe check)
 
        _door.Close();//close hiding spot door
        yield return new WaitWhile(() => _door.IsAnimating);//wait while door animating is true(continue execution when false)
        _playerCharacter.Input.OnInteractInput.AddListener(StopHiding);//listen function StopHiding dari event input interact
    }
    
    public void StopHiding()
    {
        Debug.Log("StopHiding");
        if (_unhideCoroutine != null)//if _unhideCoroutine is in progress
        {
            StopCoroutine(_unhideCoroutine);//stop _unhideCoroutine's coroutine
        }
        StartCoroutine(Unhide());//start coroutine to unhide(start unhiding animation)
    }
    
    public IEnumerator Unhide()
    {
        _playerCharacter.Input.OnInteractInput.RemoveListener(StopHiding);//unlisten function StopHiding dari event input interact
        _door.Open();//open hiding spot door
        yield return new WaitWhile(() => _door.IsAnimating);//wait while door animating is true(continue execution when false)
        
        float time = 0f;//float to calculate animation time with "0" as its starting value
        Vector3 startPosition = _playerCharacter.transform.position;//ref player's position when deciding to unhide
        
        while (time < _duration)//while time is smaller than _interactDuration
        {
            time = time + Time.deltaTime;//add 1 to time every one second
            _playerCharacter.transform.position = Vector3.Lerp(startPosition, _unhidePosition.position, time / _duration);//change player's position to targeted position using Lerp
            //float panAxis = Mathf.Lerp(_playerCharacter.Camera.PanAxis, _unhidePosition.eulerAngles.y, time / _duration);//var to calculate player's pan angle to targeted angle using Lerp
            //_playerCharacter.Camera.CallSetPanAxisValue(panAxis);//assign panAxis into player's camera pan angle value
            yield return null;//stop here and execute code above on the next frame
        }
        _playerCharacter.transform.position = _unhidePosition.position;//after animation ended set player's position to unhide position(safe check)
        //_playerCharacter.transform.rotation = _unhidePosition.rotation;//after animation ended set player's pan rotation to unhide rotation(safe check)
 
        _door.Close();//close hiding spot door
        
        _playerCharacter.Camera.CallSetCameraInputEnable(true);//set camera input true
        _playerCharacter.Movement.CallSetEnabled(true);//set players movement input true
        _playerCharacter.InteractDetector.CallSetEnabled(true);//reactivate interact detector
        _playerCharacter.CallSetIsHiding(false);//set hiding status to false
        _playerCharacter = null;//set _hidingPlayer back to null
        
        yield return new WaitWhile(() => _door.IsAnimating);//wait while door animating is true(continue execution when false)
    }
}
