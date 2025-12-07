using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class InteractUi : MonoBehaviour
{
    private vThirdPersonCamera _mainCam;

    private void Start()
    {
        // Find the vThirdPersonCamera component in the scene
        _mainCam = FindObjectOfType<vThirdPersonCamera>();

        // Optional: Check if _maincam was found to avoid null reference errors
        if (_mainCam == null)
        {
            Debug.LogError("vThirdPersonCamera not found in the scene!");
        }
    }

    private void LateUpdate()
    {
        if (_mainCam != null)
        {
            var rotation = _mainCam.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }
}
