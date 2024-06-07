using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float sensitivity = 30f;

    [Header("Movement Settings")]
    [Tooltip("The speed the player moves at")]
    [SerializeField] private float walkSpeed = 60f;

    [Tooltip("The maximum angle the player can walk up without losing speed")]
    [SerializeField] private float maxSlopeAngle = 45f;

    [Tooltip("The height the floating rigidbody is offset from the ground")]
    [SerializeField] private float heightOffset = 1.01f;

    [Tooltip("The range of the ground checking ray")]
    [SerializeField] private float offsetRayDistance = 1f;

    [Tooltip("Adds force to the total Y-offset")]
    [SerializeField] private float offsetStrength = 200f;

    [Tooltip("Affects how controlled the offset is")]
    [SerializeField] private float offsetDamper = 10f;

    [Tooltip("Adds more drag to the players velocity")]
    [SerializeField] private float dragRate = 5f;

    [Tooltip("The layers the player can walk on")]
    [SerializeField] private LayerMask groundLayer;

    private PlayerControls playerActions;
    private PlayerControls.PlayerActions playerMovement;
    
    private Rigidbody rb;

    private RaycastHit groundHit;

    private Camera cam;

    private float lookRotation;

    private bool isGrounded;
    private bool isMoving;
    private bool applyMovementEffects;
    public bool IsGrounded { get { return isGrounded; } }
    public bool IsMoving { get { return isMoving; } }
    public bool ApplyMovementEffects { get { return applyMovementEffects; } }

    // Start is called before the first frame update
    void Awake()
    {
        playerActions = new PlayerControls();
        playerMovement = playerActions.Player;

        rb = GetComponent<Rigidbody>();

        cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, out groundHit, offsetRayDistance, groundLayer);
        Debug.Log(groundHit.distance);
        Debug.DrawRay(transform.position, -Vector3.up * offsetRayDistance, Color.yellow);
    }
    void FixedUpdate() => Move();

    void LateUpdate() => Look();

    private Vector3 MoveDirection()
    {
        //Read player input
        Vector2 moveInput = playerMovement.Move?.ReadValue<Vector2>() ?? Vector2.zero;

        //Project two vectors onto an orthagonal plane and multiply them by the players x and y inputs
        Vector3 moveDirection =
            (Vector3.ProjectOnPlane(transform.forward, Vector3.up) * moveInput.y +
            Vector3.ProjectOnPlane(transform.right, Vector3.up) * moveInput.x);
        //Normalize the two projected inputs added together to get the movement direction
        moveDirection.Normalize();

        //Returns unit vector
        return moveDirection;
    }

    private void Move()
    {
        if (!isGrounded) return;

        //Check if moving
        isMoving = rb.velocity.magnitude < 0;

        //Apply walk speed to the movement vector
        Vector3 moveForce = MoveDirection() * walkSpeed;

        //Find the angle between the players up position and the groundHit
        float slopeAngle = Vector3.Angle(Vector3.up, groundHit.normal);

        //Set to (0, 0, 0)
        Vector3 yOffsetForce = Vector3.zero;

        //If surface angle is within max angle
        if (slopeAngle <= maxSlopeAngle)
        {
            //Find difference between ground distance and the offset
            float yOffsetError = (heightOffset - groundHit.distance);

            //Find the dot product of vector3.up and of the players velocity
            float yOffsetVelocity = Vector3.Dot(Vector3.up, rb.velocity);

            //Set the offset force of the floating rigidbody
            yOffsetForce = Vector3.up * (yOffsetError * offsetStrength - yOffsetVelocity * offsetDamper);
        }
        //Calculate the combinded force between direction and offset
        Vector3 combinedForces = moveForce + yOffsetForce;

        //Calculate damping forces by multiplying the drag and player velocity
        Vector3 dampingForces = rb.velocity * dragRate;

        //Add forces to rigidbody
        rb.AddForce((combinedForces - dampingForces) * (100 * Time.fixedDeltaTime));
    }

    private void Look()
    {
        //Check if player is looking too far up or down
        applyMovementEffects = lookRotation > 80 || lookRotation < -80;

        //Read mouse input
        Vector2 lookForce = playerMovement.Look?.ReadValue<Vector2>() ?? Vector2.zero;

        //Turn the player with the X-input
        gameObject.transform.Rotate(lookForce.x * sensitivity * Vector3.up / 100);

        //Add Y-input multiplied by sensitivity to float
        lookRotation += (-lookForce.y * sensitivity / 100);

        //Clamp the look rotation so the player can't flip the camera
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);

        //Set cameras rotation
        cam.transform.eulerAngles = new(lookRotation, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
    }

    private void OnEnable()
    {
        playerMovement.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable(); 
    }
}
