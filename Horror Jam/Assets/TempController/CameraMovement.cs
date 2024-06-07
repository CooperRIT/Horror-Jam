using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform mainTransform;

    [Header("Goofy Value Shit")]
    [SerializeField] float sensitivity = 90f;
    [SerializeField] int xClamp = 90;
    [SerializeField] int yClamp = 90;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

    }

    public void RotateCamera(InputAction.CallbackContext context)
    {
        Vector2 rotations = context.ReadValue<Vector2>();

        float mouseX = rotations.x * sensitivity * Time.deltaTime;
        float mouseY = rotations.y * sensitivity * Time.deltaTime;

        // Adjust the rotation around the X axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        // Apply rotation to the camera and the player body
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        mainTransform.Rotate(Vector3.up * mouseX);
    }
}
