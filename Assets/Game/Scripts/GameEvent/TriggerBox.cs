using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private bool _autoActive;
    [SerializeField] private string _tag;
    [SerializeField] private bool _isOneTime;
    
    public UnityEvent OnTrigger;

    private bool _isActive;

    private void Awake()
    {
        _isActive = _autoActive;
    }

    private void SetActive(bool isActive)
    {
        _isActive = isActive;
    }
    public void CallSetActive(bool isActive){SetActive(isActive);}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag) && _isActive)
        {
            OnTrigger?.Invoke();
            if (_isOneTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
