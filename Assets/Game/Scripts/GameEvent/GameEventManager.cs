using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager _instance;
    private Dictionary<string, GameEventBase> _gameEvents = new Dictionary<string, GameEventBase>();

    public static GameEventManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Register(GameEventBase gameEvent)
    {
        if (!_gameEvents.ContainsKey(gameEvent.ID))
        {
            _gameEvents.Add(gameEvent.ID, gameEvent);
        }
    }
    public void CallRegister(GameEventBase gameEvent){Register(gameEvent);}

    private void Unregister(GameEventBase gameEvent)
    {
        if (_gameEvents.ContainsKey(gameEvent.ID))
        {
            _gameEvents.Remove(gameEvent.ID);
        }
    }
    public void CallUnregister(GameEventBase gameEvent){Unregister(gameEvent);}

    private void TriggerEvent(string id)
    {
        bool isGameEventExist = _gameEvents.TryGetValue(id, out GameEventBase gameEvent);
        if (isGameEventExist)
        {
            gameEvent.Trigger();
        }
    }
    public void CallTriggerEvent(string id){TriggerEvent(id);}

    private void FinishEvent(string id)
    {
        bool isGameEventFound = _gameEvents.TryGetValue(id, out GameEventBase gameEvent);
        if (isGameEventFound)
        {
            gameEvent.Finish();
        }
    }
    public void CallFinishEvent(string id){FinishEvent(id);}
}
