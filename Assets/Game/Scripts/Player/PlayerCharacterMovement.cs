using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    private Vector3 _movementDirection;//var to initialize movedir
    [SerializeField] private float _currentSpeed = 1;//var to initialize current character speed
    private Vector3 _velocityXZ;
    [SerializeField] private CharacterController _characterController;//initialize character controller
    [SerializeField] private float _gravityScale = 1;//initialize gravity scale value
    private float _velocityY;
    private bool _isGrounded;
    private bool _isSprinting; public bool IsSprinting => _isSprinting;
    [SerializeField] private float _walkSpeed = 1;
    [SerializeField] private float _sprintSpeed = 2;
    [SerializeField] private float _acceleration = 0.5f;

    private void SetMoveDirection(Vector2 inputDirection)//method to set move direction
    {
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }
    public void CallSetMoveDirection(Vector2 inputDirection){ SetMoveDirection(inputDirection); }//<<called through inspector

    private void CalculateVelocityXZ()
    {
        Transform cameraTransform = Camera.main.transform;//get transform camera
        Vector3 xDirection = _movementDirection.x * cameraTransform.right;
        Vector3 zDirection = _movementDirection.z * cameraTransform.forward; 
        Vector3 direction = xDirection + zDirection; 
        direction.y = 0; 
        if (_movementDirection.magnitude >= 0.01)//if magnitude of movedir > 0
        { 
            _velocityXZ = direction.normalized * (_currentSpeed * Time.deltaTime); 
        } 
        else _velocityXZ = Vector3.zero;//set velocity to zero
    }

    private void CalculateVelocityY()//calculate velocity of y(simulate gravity)
    {
        _velocityY += Physics.gravity.y * (_gravityScale * Time.deltaTime);
    }

    private void CheckIsGrounded()//ground check method
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        _isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundLayer);
    }

    private void ResetVelocityY()//reset velocity when is grounded true
    {
        if (_isGrounded == true && _velocityY < 0)
        {
            _velocityY = -1;
        }
    }

    private void SetSprinting(bool isSprinting)//to set invoked sprinting bool value to var in this class
    {
        _isSprinting = isSprinting;
    }
    public void CallSetSprinting(bool isSprinting){ SetSprinting(isSprinting); }//<<called through inspector

    private void CalculateAcceleration()//calculates acceleration of players movement
    {
        if (_movementDirection.magnitude >= 0.01)//if magnitude of movedir > 0
        { 
            if (_isSprinting)//if sprinting
            { 
                _currentSpeed += _acceleration * Time.deltaTime;//increase current speed overtime
            } 
            else _currentSpeed -= _acceleration * Time.deltaTime;//else sprinting decelerate speed
            
            _currentSpeed = Mathf.Clamp(_currentSpeed, _walkSpeed, _sprintSpeed);//to cap current speed value
        }
        else
        {
            _currentSpeed = 0;//set current speed to 0
            //_currentSpeed -= _acceleration * Time.deltaTime; //decrease current speed overtime
            //_currentSpeed = Mathf.Clamp(_currentSpeed, 0, _sprintSpeed);//to cap current speed value
        }
    }
    
    private void Move()//method to move character player
    {
        CalculateVelocityXZ();//calculate velocity of x & z
        CalculateVelocityY();//calculate velocity of y
        Vector3 velocity = new Vector3(_velocityXZ.x, _velocityY, _velocityXZ.z);//combined direction velocity off all xyz
        _characterController.Move(velocity);//move the character using character controller
    }
    
    private void Update()
    {
        CheckIsGrounded();
        ResetVelocityY();
        CalculateAcceleration();
        Move();
    }
}
