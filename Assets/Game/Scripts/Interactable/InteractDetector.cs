using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;//var to ref PlayerCharacter module
    [SerializeField] private float _detectorDistance;//float var to set boxcast distance
    [SerializeField] private Vector3 _detectorBoxSize = Vector3.one;//Vector3 var to set the boxcast size(scaling xyz)
    [SerializeField] private LayerMask _interactableLayer;//LayerMask var to ref the interactable layer
 
    private IInteractable _detectedInteractable;//var to ref detected object
    private bool _isInteracting;//bool var for interacting state
    private bool _isDetectingInteractable;//bool var to set is detecting interactable state
    private RaycastHit _hitInfo;//var to get hitted object info

    public bool Enabled { get; private set; } = true;//create property to determine interaction active state with "true" as starting value
    private void SetEnabled(bool isEnabled)//method to change movement active status
    {
        Enabled = isEnabled;
    }
    public void CallSetEnabled(bool isEnabled) { SetEnabled(isEnabled); }
    
    private void Interact()//method to execute interaction
    {
        if (_detectedInteractable != null && _isDetectingInteractable && Enabled)//if _detectedInteractable is not null(has value) and ray is detecting interactable object
        {
            _isInteracting = true;//set interacting state to true on this frame
            _detectedInteractable.Interact(_playerCharacter);//call interact function and send _playerCharacter var as its argument
            _detectedInteractable = null;//set _detectedInteractable to null(detected object = null)
        }
    }
    public void CallInteract() { Interact(); }//called through inspector
    
    private void UpdateDetection()//method to update the raycast detection for interaction
    {
        if (_isInteracting)//if we are interacting
        {
            _isInteracting = false;//set interacting state to false
            return;//Keluar dari function untuk frame saat ini, kemudian kembali di frame berikut nya
        }

        if (Enabled)
        {
            Transform cameraTransform = Camera.main.transform;//get ref of the main camera's transform component
        
            //set a boolean to detect interactable object
            _isDetectingInteractable = Physics.BoxCast(
                cameraTransform.position, 
                _detectorBoxSize * 0.5f, 
                cameraTransform.forward, 
                out _hitInfo, 
                Quaternion.identity, 
                _detectorDistance, 
                _interactableLayer);

            if (_isDetectingInteractable)//if interactable layer is hit
            {
                /*
                IInteractable interactable = _hitInfo.collider.gameObject.GetComponent<IInteractable>();//Mengecek apakah object punya component class yang implementasi interface interactable
                if (interactable != null)//if interactable is not null(has value)
                {
                    _detectedInteractable = interactable;//insert object into _detectedInteractable var
                }
                */
                _detectedInteractable = _hitInfo.collider.gameObject.GetComponent<IInteractable>();
                return;
            }
            _detectedInteractable = null;//set _detectedInteractable to null if no interactable object is detected
        }
    }
    
    private void OnDrawGizmosSelected()//built-in method to draw gizmos(only visible when the object using this script is selected)
    {
        Gizmos.color = Color.red;//set gizmo color to red
        Transform cameraTransform = Camera.main.transform;//var to ref the main camera's transform data
        
        // Membuat detector dengan boxcast
        // Start detector dari posisi camera, dengan jarak yang sudah
        // ditentukan, arah nya ke depan camera, informasi object yang 
        // terdeteksi disimpan ke dalam hit, sudut rotasi nol di semua sumbu
        // jarak dan layer yang sudah ditentukan menggunakan variable.
        // Akan bernilai true jika ada object interactable terdeteksi
        // Akan bernilai false jika tidak ada object interactable terdeteksi
        //bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position, _detectorBoxSize * 0.5f, cameraTransform.forward, out RaycastHit hit, Quaternion.identity, _interactableLayer);
        
        //set a boolean to detect interactable object
        //Physics.BoxCast(Vector3 startPoint, Vector3 size, Vector3 direction, out RaycastHit takeInfoFromHittedObject, Quaternion rotation, float maxDistance(optional), LayerMask targetedLayer)
        /*bool isDetectingInteractable = Physics.BoxCast(
            cameraTransform.position,
            _detectorBoxSize * 0.5f, 
            cameraTransform.forward, 
            out RaycastHit hit, 
            Quaternion.identity,
            _detectorDistance,
            _interactableLayer);//this bool will set to true if it hit the targeted layer(physically exist but invisible by the players)*/
        
        if (_isDetectingInteractable)//if targeted layer is hit
        {
            Gizmos.color = Color.green;//set gizmo color green
            
            //draw a line(only visible in unity scene)
            //Gizmos.DrawLine(Vector3 startPosition, Vector3 endPosition)
            //draw a line forward from the main camera to the hitted object
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * _hitInfo.distance);
            
            //draw a cube(only visible in unity scene)
            //Gizmos.DrawWireCube(Vector3 cubePosition, Vector3 cubeSize)
            //draw a cube onto the hitted object using _detectorBoxSize var for its size
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * _hitInfo.distance, _detectorBoxSize);
        }
        else
        {
            //draw a line(only visible in unity scene)
            //Gizmos.DrawLine(Vector3 startPosition, Vector3 endPosition)
            //draw a line forward from the main camera to _detectorDistance
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * _detectorDistance);
            
            //draw a cube(only visible in unity scene)
            //Gizmos.DrawWireCube(Vector3 cubePosition, Vector3 cubeSize)
            //draw a cube to _detectorDistance
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * _detectorDistance, _detectorBoxSize);
        }
    }
    
    private void Update()
    {
        UpdateDetection();//call UpdateDetection method every frame
    }
}
