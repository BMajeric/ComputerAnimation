using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 camPosition;
    private bool shiftClicked = false;

    private Vector3 camRotation = new Vector3(0, 0, 0);

    [Header("Camera Settings")]
    public float camSpeed = 1.0f;
    [Range(0, 30)]
    public float sensitivity = 5.0f;

    void Update()
    {
        camPosition = Vector3.zero;
        // Camera acceleration
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!shiftClicked)
            {
                shiftClicked = true;
                camSpeed *= 4;
            }
        } else
        {
            if (shiftClicked)
            {
                shiftClicked = false;
                camSpeed /= 4;
            }
        }

        // Camera movement
        if (Input.GetKey(KeyCode.W))
        {
            camPosition += Vector3.forward * camSpeed / 60;
        }
        if (Input.GetKey(KeyCode.S))
        {
            camPosition += Vector3.back * camSpeed / 60;
        }
        if (Input.GetKey(KeyCode.A))
        {
            camPosition += Vector3.left * camSpeed / 60;
        }
        if (Input.GetKey(KeyCode.D))
        {
            camPosition += Vector3.right * camSpeed / 60;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            camPosition += Vector3.up * camSpeed / 60;
        }
        if (Input.GetKey(KeyCode.E))
        {
            camPosition += Vector3.down * camSpeed / 60;
        }

        // Camera rotation
        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity;
        camRotation.y += Input.GetAxis("Mouse X") * sensitivity;
        camRotation.x = Mathf.Clamp(camRotation.x, -89.0f, 89.0f);

        this.transform.Translate(camPosition);
        this.transform.localEulerAngles = camRotation;
    }
}
