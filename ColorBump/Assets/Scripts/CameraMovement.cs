using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    public Vector3 camVel;
    void Update()
    {
        if (PlayerController.instance.canMove)
            transform.position += Vector3.forward * cameraSpeed;

        camVel = Vector3.forward * cameraSpeed;
    }
}
