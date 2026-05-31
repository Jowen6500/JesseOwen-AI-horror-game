using UnityEngine;
using UnityEngine.Events;

//door script
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    public string Name => _name;//get "_name" value and assign it into the "Name" property inside the IInteractable
    
    [SerializeField] protected Transform _doorTransform;//transform var for the door you want to rotate
    [SerializeField] protected float _duration = 1f;//float var we'll use for the door animation duration
    [SerializeField] protected bool _isLocked;//bool var we'll use for door's lock state
    [SerializeField] protected string _keyID;//string var to store the door's key id
    
    protected bool _isAnimating;//bool var we'll use for door's animating state(is playing anim or not)
    public bool IsAnimating => _isAnimating;//get "_isAnimating" state and assign it into the "IsAnimating" property inside the ...
    
    protected bool _isOpen;//bool var to determine the door's open/close state
    public UnityEvent OnDoorOpen;//declare OnDoorOpen as UnityEvent to invoke an event
    public UnityEvent OnDoorClose;//declare OnDoorClose as UnityEvent to invoke an event
    protected Coroutine _animatingDoorCoroutine;//var to store coroutine that's being executed
    
    public virtual void Open()//opening door method, virtual so the child that inherits this method can modify it
    {
        _isOpen = true;//turn door state to open
        OnDoorOpen?.Invoke();//invoke to call method, open door
    }
 
    public virtual void Close()//closing door method, virtual so the child that inherits this method can modify it
    {
        _isOpen = false;//turn door state to close
        OnDoorClose?.Invoke();//invoke to call method, close door
    }
    
    //created context menu so the interact function can be called through the door component inside the inspector
    [ContextMenu("Interact Door")]
    public void Interact(PlayerCharacter character)//abstract function interact
    {
        if (_isOpen == true)//if door state is open
        {
            Close();
        }
        else//if door state is close
        {
            Open();//open the door
        }
    }
}
