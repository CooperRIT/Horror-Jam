using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    [Header("Camera Tilt Settings")]
    [Tooltip("Controls how fast the camera returns to the origin position")]
    [SerializeField] private float tiltInSpeed = 10f;

    [Tooltip("Controls how fast the camera moves to the tilted position")]
    [SerializeField] private float tiltOutSpeed = 9f;

    [Tooltip("Controls how far the camera tilts when moving")]
    [SerializeField] private float tiltAmount = -4f;

    [Tooltip("The player Camera")]
    [SerializeField] private Transform cam;

    //Private Variables
    private PlayerController playerController;
    private Rigidbody rb;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Guard Clause to prevent applying affect when not ideal
        if (playerController.ApplyMovementEffects || !playerController.IsGrounded)
            return;

        TiltUpdate();
    }
    /// <summary>
    /// Applies camera tilt based on player movement
    /// </summary>
    void TiltUpdate()
    {
        bool doTilt = false;

        //If the player is moving above a certain velocity, set the current rotation to the tilt amount
        if (playerController.MoveInput.x != 0 && rb.velocity.magnitude > 2)
            doTilt = true;

        if (doTilt)
        {
            if (playerController.MoveInput.x < 0)
                currentRotation = new(0, 0, -tiltAmount);
            else
                currentRotation = new(0, 0, tiltAmount);
        }
        //Return to origin position
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, tiltOutSpeed * Time.deltaTime);

        //Rotate camera to tilted position
        targetRotation = Vector3.Lerp(targetRotation, currentRotation, tiltInSpeed * Time.deltaTime);
        cam.transform.localRotation = Quaternion.Euler(targetRotation);
    }
}
