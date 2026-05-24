using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //[SerializeField] private ScoreManager _scoreManager;
    public UnityEvent<int> OnSpaceInput;//create event on space input
    [SerializeField] private int _reward = 10;
    
    
    private void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            //_scoreManager.CallAddScore();//call method add score
            OnSpaceInput?.Invoke(_reward);//trigger add 10
        } 
    } 
}
