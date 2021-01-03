using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public LookState state;
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        xRotation = 0f;

        switch (state)
        {
            case LookState.LockedInvisible:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case LookState.VisibleMoveable:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }    
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void ChangeLookState(LookState state)
    {
        switch (state)
        {
            case LookState.LockedInvisible:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case LookState.VisibleMoveable:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}

public enum LookState { LockedInvisible, VisibleMoveable }