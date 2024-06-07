using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    Controls controls;
    Vector2 inputDirection => controls.BasicControls.WASD.ReadValue<Vector2>();

    [SerializeField] CameraMovement cameraMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.BasicControls.CameraMovement.performed += cameraMovement.RotateCamera;
    }
    private void OnDisable()
    {
        controls.Disable();
        controls.BasicControls.CameraMovement.performed -= cameraMovement.RotateCamera;

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public Vector2 InputDirection
    {
        get { return inputDirection; }
    }
}
