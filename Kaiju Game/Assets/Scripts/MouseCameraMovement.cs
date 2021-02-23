using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class MouseCameraMovement : MonoBehaviour
{

    [Header("Camera Settings")]
    public Camera cam;
    public float mouseSensitivity;
    public float cameraMinimumTilt;
    public float cameraMaximumTilt;

    private float verticalLookRoation;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if(Cursor.visible == false)
        {
            CameraMovement();
        }
    }

    void CameraMovement()
    {
        float moveX = Input.GetAxis("Mouse X");
        if (moveX != 0)
        {
            transform.Rotate(new Vector3(0, moveX, 0) * mouseSensitivity);
        }

        float moveY = Input.GetAxis("Mouse Y");
        if (moveY != 0)
        {
            verticalLookRoation = Mathf.Clamp(verticalLookRoation - moveY * mouseSensitivity, cameraMinimumTilt, cameraMaximumTilt);

            cam.transform.localEulerAngles = new Vector3(verticalLookRoation, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
        }
    }
}