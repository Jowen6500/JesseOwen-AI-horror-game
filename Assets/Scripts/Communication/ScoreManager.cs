using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    private int _score;
    
    public int Score{ get => _score; }

    /*//disable if using inspector to invoke event
    private void OnEnable()
    {
        //add "AddScore" into a listener of "OnSpaceInput"
        _inputManager.OnSpaceInput.AddListener(AddScore);
    }
    private void OnDisable()
    {
        //delete "OnSpaceInput" listener from "AddScore"
        _inputManager.OnSpaceInput.RemoveListener(AddScore);
    }
    */

    private void AddScore(int value) 
    { 
        _score = _score + value;
    } 
    public void CallAddScore(int value){ AddScore(value); }
}
