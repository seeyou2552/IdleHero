using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject cameraObj;
    public void OnCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            cameraObj.GetComponent<CameraFollow>().ChangeTarget();
        }
    }
}
