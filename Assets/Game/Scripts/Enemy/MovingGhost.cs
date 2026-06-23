using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingGhost : MonoBehaviour
{
    [SerializeField] private List<Vector3> _destinations = new List<Vector3>();
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _distanceTolerance = 0.1f;
    [SerializeField] private bool _autoNextDestination;
    [SerializeField] private bool _playOnAwake = true;
    
    private int _destinationIndex;
    private Coroutine _moveCoroutine;

    public UnityEvent OnStartMoving;
    public UnityEvent OnReachDestination;
    public UnityEvent OnReachAllDestinations;
    
    private void Start()
    {
        if (_playOnAwake)
        {
            MoveToNextDestination();
        }
    }

    private void MoveToNextDestination()
    {
        if (_destinations.Count > 0 && _destinations.Count > _destinationIndex)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
            
            OnStartMoving?.Invoke();
            _moveCoroutine = StartCoroutine(MoveToTarget(_destinations[_destinationIndex]));
            _destinationIndex++;
            return;
        }
        OnReachAllDestinations?.Invoke();
        Destroy(this);
    }
    public void CallMoveToNextDestination(){MoveToNextDestination();}

    private void RotateToDestination()
    {
        if(_destinations.Count > 0 && _destinations.Count > _destinationIndex)
        {
            transform.LookAt(_destinations[_destinationIndex]);
        }
    }
    public void CallRotateToDestination(){RotateToDestination();}

    private IEnumerator MoveToTarget(Vector3 target)
    {
        RotateToDestination();
        
        while (Vector3.Distance(transform.position, target) > _distanceTolerance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }
        
        transform.position = target;
        OnReachDestination?.Invoke();
        
        if (_autoNextDestination)
        {
            MoveToNextDestination();
        }
        else
        {
            if (_destinationIndex >= _destinations.Count)
            {
                OnReachAllDestinations?.Invoke();
                Destroy(this);
            }
        }
    }
}
