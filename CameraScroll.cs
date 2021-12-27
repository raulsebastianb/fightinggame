using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    private float cameraFOV;
    [SerializeField]
    private float zoomSpeed;
    private float mouseScrollInput;

    void Start()
    {
        cameraFOV = camera.fieldOfView;
    }

    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        cameraFOV -= mouseScrollInput * zoomSpeed;
        cameraFOV = Mathf.Clamp(cameraFOV, 30, 60);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, cameraFOV, zoomSpeed);
    }
}
