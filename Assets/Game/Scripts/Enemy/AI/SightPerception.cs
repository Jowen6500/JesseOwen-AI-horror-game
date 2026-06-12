using UnityEngine;

public class SightPerception : MonoBehaviour
{
    [SerializeField] private Transform _eyePosition;//ref ai eye position
    [SerializeField] private Transform _target;//ref its target position
    [SerializeField] private float _viewDistance = 10;//view distance var
    [SerializeField] private float _viewAngle = 70;//fov var
    [SerializeField] private LayerMask _targetLayerMask;//ref targeted layermask
    
    public bool CanSeePlayer { get; private set; }//property for can see player state
    public Vector3 LastSeenPosition { get; private set; }//property for last seen player position

    private void Update()
    {
        CanSeePlayer = CheckSight();//set can see player state into CheckSight value
    }
    
    public bool CheckSight()//returns target on ai fov range? state
    {
        if (_target == null)//if no target
        {
            return false;
        }
        
        //check distance
        float currentDistance = Vector3.Distance(_eyePosition.position, _target.position);//calculate eye position and target position distance
        if (currentDistance > _viewDistance)//if current distance > view distance
        {
            return false;
        }
        
        //check FOV
        Vector3 directionToTarget = _target.position - _eyePosition.position;//calculate target's direction from eye position
        float angle = Vector3.Angle(_eyePosition.forward, directionToTarget.normalized);//calculate target's angle position from eye position

        if (angle > _viewAngle * 0.5f)//if target's angle position > ai's view angle
        {
            return false;
        }
        
        //check raycast
        //create bool based on physics raycast to create detect target state
        bool isTargetOnSight = Physics.Raycast(_eyePosition.position, directionToTarget.normalized, out RaycastHit hit, _viewDistance, _targetLayerMask);

        if (isTargetOnSight)//if target detected
        {
            if (hit.transform == _target)//if hitted object's transform = target
            {
                LastSeenPosition = _target.position;//set last seen target's position
                return true;//set state to true
            }
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        if (_eyePosition == null)
        {
            return;
        }
        
        Gizmos.color = Color.red;
        bool isTargetOnSight = CheckSight();
        if (isTargetOnSight)
        {
            Gizmos.color = Color.green;
        }
        
        Gizmos.DrawWireSphere(_eyePosition.position, _viewDistance);

        Vector3 left = Quaternion.Euler(0, -_viewAngle / 2, 0) * _eyePosition.forward;
        Vector3 right = Quaternion.Euler(0, _viewAngle / 2, 0) * _eyePosition.forward;
        
        Gizmos.DrawRay(_eyePosition.position, left * _viewDistance);
        Gizmos.DrawRay(_eyePosition.position, right * _viewDistance);
    }
}
