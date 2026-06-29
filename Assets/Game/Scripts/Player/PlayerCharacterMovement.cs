using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    private Vector3 _movementDirection;//var to ref movedir
    [SerializeField] private float _currentSpeed = 1;//var to ref current character speed
    private Vector3 _velocityXZ;//velocity of x and z axis
    [SerializeField] private CharacterController _characterController;//ref CharacterController module
    [SerializeField] private float _gravityScale = 1;//ref gravity scale value
    private float _velocityY;//velocity of y-axis
    private bool _isGrounded;//grounded state using bool
    private bool _isSprinting; public bool IsSprinting => _isSprinting;//sprint state using bool and add properties
    [SerializeField] private float _walkSpeed = 1;//the walk speed value cap
    [SerializeField] private float _sprintSpeed = 2;//the sprint speed value cap
    [SerializeField] private float _acceleration = 0.5f;//acceleration value to gain or lose speed overtime

    public bool Enabled { get; private set; } = true;//property used to determine movement enable or disable state
    
    private void SetEnabled(bool isEnabled)//method to change movement active status
    {
        Enabled = isEnabled;
    }
    public void CallSetEnabled(bool isEnabled) { SetEnabled(isEnabled); }
    
    private void SetMoveDirection(Vector2 inputDirection)//method to set move direction
    {
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }
    public void CallSetMoveDirection(Vector2 inputDirection){ SetMoveDirection(inputDirection); }//<<called through inspector

    private void CalculateVelocityXZ()
    {
        Transform cameraTransform = Camera.main.transform;//get transform camera
        Vector3 xDirection = _movementDirection.x * cameraTransform.right;//using "right" to identify the x axis of character player
        Vector3 zDirection = _movementDirection.z * cameraTransform.forward; //using "forward" to identify the z axis of character player
        Vector3 direction = xDirection + zDirection;//adding both Vector3 to get the direction player is pressing
        direction.y = 0; 
        if (_movementDirection.magnitude >= 0.01)//if magnitude of movedir > 0
        { 
            _velocityXZ = direction.normalized * (_currentSpeed * Time.deltaTime);//adding speed value to velocity x and z
        } 
        else _velocityXZ = Vector3.zero;//set velocity to zero
    }

    private void CalculateVelocityY()//calculate velocity of y(simulate gravity)
    {
        _velocityY += Physics.gravity.y * (_gravityScale * Time.deltaTime);
    }

    private void CheckIsGrounded()//ground check method
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");//declare groundLayer and assign the layer value
        
        //generate hidden sphere with its radius value on the designated transform and detects whether the colliding object is ground or not
        _isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundLayer);
    }

    private void ResetVelocityY()//reset velocity when is grounded true
    {
        if (_isGrounded == true && _velocityY < 0)
        {
            _velocityY = -1;
        }
    }

    private void SetSprinting(bool isSprinting)//set invoked sprinting bool value to _isSprinting var in this class
    {
        _isSprinting = isSprinting;

        if (isSprinting)//if is sprinting
        {
            HUDManager.Instance.StaminaUI.StaminaBG.CrossFadeAlpha(1, 0.5f, false);//invoke crossfade StaminaBG Alpha to 1
            HUDManager.Instance.StaminaUI.StaminaFill.CrossFadeAlpha(1, 0.5f, false);//invoke crossfade StaminaFill Alpha to 1
            //HUDManager.Instance.StaminaUI.CallSetVisible(true);//activate stamina bar
        }
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
            else _currentSpeed -= _acceleration * Time.deltaTime;//decrease current speed overtime
            
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
        if (Enabled)
        {
            CalculateVelocityXZ();//calculate velocity of x & z
            CalculateVelocityY();//calculate velocity of y
            Vector3 velocity = new Vector3(_velocityXZ.x, _velocityY, _velocityXZ.z);//combined direction velocity off all xyz
            _characterController.Move(velocity);//move the character using character controller
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(_isGrounded) Gizmos.color = Color.cyan;
        else Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
    
    private void Update()
    {
        CheckIsGrounded();
        ResetVelocityY();
        CalculateAcceleration();
        Move();
    }
}
