using System;
using UnityEngine;
using UnityEngine.Events;

public class HighlightGhost : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _dotTreshold = 0.8f;
    [SerializeField] private bool _autoActive;
    bool _isActive;
    
    public UnityEvent OnSeeGhost;

    private void Awake()
    {
        _isActive = _autoActive;
    }
    
    private void SetActive(bool value)
    {
        _isActive = value;
    }
    public void CallSetActive(bool value){SetActive(value);}

    private bool CheckIsPlayerSeeGhost()
    {
        Transform playerCamera = Camera.main.transform;
        Vector3 ghostDirection = (transform.position - playerCamera.position).normalized;
        float dotResult = Vector3.Dot(playerCamera.forward, ghostDirection);

        if (dotResult > _dotTreshold)
        {
            float distance = Vector3.Distance(playerCamera.position, transform.position);

            if (distance < _maxDistance)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if (_isActive)
        {
            bool isSeeGhost = CheckIsPlayerSeeGhost();
            if (isSeeGhost)
            {
                OnSeeGhost?.Invoke();
                Destroy(this);
            }
        }
    }
}
