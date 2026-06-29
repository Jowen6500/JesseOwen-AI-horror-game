using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    [SerializeField] private Vector3 _openPosition;//var to determine opened door position
    [SerializeField] private Vector3 _closedPosition;//var to determine closed door position

    private IEnumerator SlideDoor(Vector3 targetPosition)//create a "IEnumerator" function to animate sliding door with "Vector3 targetPosition" as its argument
    {
        _isAnimating = true;//turn is animating state to true(cause current function is going to trigger animation)
        Vector3 startPosition = _doorTransform.localPosition;//var to determine start position of the door by assigning the current Vector3 position value of the door
        float time = 0;//var to count the animation time that was played
 
        while (time < _duration)//while loop to the animation, while time's value is lower than _duration
        {
            time += Time.deltaTime;//add a value of 1f every one second
            
            // Melakukan interpolasi posisi awal ke posisi target 
            // Menentukan alpha dengan rumus time/duration
            // alpha bernilai 0 s.d 1, alpha merupakan nilai yang dianimasikan
            // 0 => posisi awal, 1 => posisi akhir 
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / _duration);
            _doorTransform.localPosition = position;//change the door's position to "position" var(that has been calculated)
            yield return null;//tells a coroutine to pause its execution and resume on the very next frame
            //which means the block of code above will be executed every frame
        }
        //code below will be executed after the while loop above is done(animation process has finished)
        //assign the door's position value to "targetPosition" value to make sure its position makes it to the targeted position
        _doorTransform.localPosition = targetPosition;
        _isAnimating = false;//turn the is animating state to false after all code above has been executed
    }
    
    public override void Open()//Override function open to change the opening door behavior
    {
        if (_animatingDoorCoroutine != null)//if coroutine of "_animatingDoorCoroutine" is being executed
        {
            StopCoroutine(_animatingDoorCoroutine);//stop coroutine of "_animatingDoorCoroutine"
        }
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_openPosition));//start coroutine to animate door's position with its parameter value assigned with "_openPosition"
        
        base.Open();//call Open() method from the base/parent class(class Door)
    }
    
    public override void Close()//Override function close to change the closing door behavior
    {
        if (_animatingDoorCoroutine != null)//if coroutine of "_animatingDoorCoroutine" is being executed
        {
            StopCoroutine(_animatingDoorCoroutine);//stop coroutine of "_animatingDoorCoroutine"
        }
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_closedPosition));//start coroutine to animate door's position with its parameter value assigned with "_closedPosition"

        base.Close();//call Close() method from the base/parent class(class Door)
    }
}
