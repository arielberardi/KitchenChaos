using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpToCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    };
    
    [SerializeField] private Mode _mode;
    
    private void LateUpdate()
    {
        switch (_mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform.position);
                break;
            case Mode.LookAtInverted:
                Vector3 cameraDirection = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + cameraDirection);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.position;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.position;
                break;
        }
    }
}
