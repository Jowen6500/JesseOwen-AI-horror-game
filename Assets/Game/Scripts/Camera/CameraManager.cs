using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _cameraPanTilt;//var to ref CinemachinachinePanTilt
    [SerializeField] private CinemachineInputAxisController _cameraInput;//var to ref CinemachineInputAxisController
    public float PanAxis => _cameraPanTilt.PanAxis.Value;//create a property that ref camera's pan rotation angle
    public float TiltAxis => _cameraPanTilt.TiltAxis.Value;//create a property that ref camera's tilt rotation angle*
    
    private void SetCameraInputEnable(bool isActive)//method we'll use to turn on/off camera's input
    {
        _cameraInput.enabled = isActive;
    }
    public void CallSetCameraInputEnable(bool isActive) { SetCameraInputEnable(isActive); }//called on HidingCloset module/class
 
    private void ResetCameraRotation()//method to reset the camera's rotation
    {
        _cameraPanTilt.PanAxis.Value = 0;
        _cameraPanTilt.TiltAxis.Value = 0;
    }
    public void CallResetCameraRotation() { ResetCameraRotation(); }//called on HidingCloset module/class

    private void SetPanAxisValue(float panValue)//method to set the camera's pan rotation value
    {
        _cameraPanTilt.PanAxis.Value = panValue;
    }
    public void CallSetPanAxisValue(float panValue) { SetPanAxisValue(panValue); }//called on HidingCloset module/class
    
    private void SetTiltAxisValue(float tiltValue)//method to set the camera's tilt rotation value
    {
        _cameraPanTilt.TiltAxis.Value = tiltValue;
    }
    public void CallSetTiltAxisValue(float tiltValue) { SetTiltAxisValue(tiltValue); }//called on HidingCloset module/class
}
