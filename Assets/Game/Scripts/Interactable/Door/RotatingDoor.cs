using System.Collections;
using UnityEngine;

public class RotatingDoor : Door
{
    [SerializeField] private float _openAngle;//var to determine the rotation angle when the door opens
    [SerializeField] private float _closedAngle;//var to determine the rotation angle when the door closes
    
    private IEnumerator RotateDoor(float targetAngle)//create a "IEnumerator" function to animate rotating door with "float targetAngle" as its argument
    {
        _isAnimating = true;//turn is animating state to true(cause current function is going to trigger animation)
        float startAngle = _doorTransform.localEulerAngles.y;//var to determine start angle of the door by assigning the current y-axis rotation value of the door
        float time = 0;//var to count the animation time that was played
 
        while (time < _duration)//while loop to the animation, while time's value is lower than _duration
        {
            time += Time.deltaTime;//add a value of 1f every one second
            
            //Melakukan interpolasi sudut awal ke sudut target Menentukan alpha dengan rumus time/duration
            //alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            //0 => sudut rotasi awal, 1 => sudut rotasi akhir
            float angle = Mathf.LerpAngle(startAngle, targetAngle, time / _duration);
            _doorTransform.localRotation = Quaternion.Euler(0f, angle, 0f);//change the door's y-axis rotation to the targeted value angle(float angle)
            yield return null;//tells a coroutine to pause its execution and resume on the very next frame
            //which means the block of code above will be executed every frame
        }
        //code below will be executed after the while loop above is done(animation process has finished)
        //assign the door's y-axis rotation value to "targetAngle" value to make sure the y-axis rotation makes it to the target angle
        _doorTransform.localRotation = Quaternion.Euler(0f, targetAngle, 0);
        _isAnimating = false;//turn the is animating state to false after all code above has been executed
    }
    
    public override void Open()//Override function open to change the opening door behavior
    {
        if (_animatingDoorCoroutine != null)//if coroutine of "_animatingDoorCoroutine" is being executed
        {
            StopCoroutine(_animatingDoorCoroutine);//stop coroutine of "_animatingDoorCoroutine"
        }
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_openAngle));//start coroutine to animate door's rotation with its parameter value assigned with "_openAngle"
        
        base.Open();//call Open() method from the base/parent class(class Door)
    }
    
    public override void Close()//Override function close to change the closing door behavior
    {
        if (_animatingDoorCoroutine != null)//if coroutine of "_animatingDoorCoroutine" is being executed
        {
            StopCoroutine(_animatingDoorCoroutine);//stop coroutine of "_animatingDoorCoroutine"
        }
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_closedAngle));//start coroutine to animate door's rotation with its parameter value assigned with "_closedAngle"

        base.Close();//call Close() method from the base/parent class(class Door)
    }
    
    
}
