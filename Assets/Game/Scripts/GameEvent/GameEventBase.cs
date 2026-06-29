using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventBase : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private bool _isOneTime;

    public string ID => _id;
    public bool IsOneTime => _isOneTime;

    public UnityEvent OnEventTriggered;
    public UnityEvent OnEventFinished;

    public void Start()
    {
        //Register Event
        GameEventManager.Instance.CallRegister(this);
    }
    
    public virtual void Trigger()
    {
        OnEventTriggered?.Invoke();
    }

    public virtual void Finish()
    {
        OnEventFinished?.Invoke();
        if (_isOneTime)
        {
            //Unregist event
            GameEventManager.Instance.CallUnregister(this);
            Destroy(gameObject);
        }
    }
}
