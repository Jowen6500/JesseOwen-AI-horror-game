using UnityEngine;
using UnityEngine.UI;

public class MenuButtonBehavior : MonoBehaviour
{
    private RectTransform _buttonTransform;//ref button transform
    private Vector2 _originalPosition;//var to ref button original position
    private Vector2 _targetPosition;//var to ref button target position
    
    [Header("Settings")]
    [SerializeField] private Vector2 _hoverOffset = new Vector2(30, 0);//var to ref hover position
    [SerializeField] private float _moveSpeed = 10;//var to ref button move speed when hovered
    
    private void Awake()
    {
        _buttonTransform =  GetComponent<RectTransform>();//initialize button transform
        _originalPosition = _buttonTransform.anchoredPosition;//initialize button original position
        _targetPosition = _originalPosition;//set target position to original position at the start
    }
    
    private void Update()
    {
        //transition the button position using lerp inside update, so its position keeps updating
        _buttonTransform.anchoredPosition = Vector2.Lerp(_buttonTransform.anchoredPosition, _targetPosition, Time.deltaTime * _moveSpeed);
    }
    
    private void MoveButton()//function to set target position to our designated offset position
    {
        _targetPosition = _originalPosition + _hoverOffset;
    }
    public void CallMoveButton(){MoveButton();}
    
    private void MoveBackButton()//function to set target position back to the button original position
    {
        _targetPosition = _originalPosition;
    }
    public void CallMoveBackButton(){MoveBackButton();}
}
