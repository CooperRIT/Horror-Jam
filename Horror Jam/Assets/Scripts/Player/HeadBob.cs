using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Headbob Refrences")]
    [Tooltip("Camera holder which will have movement applied to it")]
    [SerializeField] private Transform headbobTarget;

    //Private Refrences
    private PlayerController playerController;
    private Rigidbody rb;

    [Header("Headbob Settings")]

    [SerializeField] private Vector2 headbobSpeed;

    [SerializeField] private Vector2 headbobIntensity;

    [SerializeField] private AnimationCurve headbobCurveY;

    [SerializeField] private AnimationCurve headbobCurveX;

    //Private Variables
    private Vector2 currentPos;
    private Vector2 currentTime;
    private Vector3 velocity;

    private float smoothTime;

    private bool applyMovementEffects => playerController.ApplyMovementEffects;
    private bool isGrounded => playerController.IsGrounded;
    private bool isMoving => playerController.IsMoving;


    private void Start()
    {
        //Get components
        playerController = GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (playerController == null) return;

        if (applyMovementEffects) return;

        UpdateBob();
    }

    void UpdateBob()
    {
        float speedFactor = rb.velocity.magnitude;

        if (!isGrounded || !isMoving)
        {
            currentPos = Vector2.zero;
            currentTime = Vector2.zero;
            smoothTime = 0.2f;
        }
        else if (isMoving)
        {
            currentTime.x += headbobSpeed.x / 10 * Time.deltaTime * speedFactor;
            currentTime.y += headbobSpeed.y / 10 * Time.deltaTime * speedFactor;
            currentPos.x = headbobCurveX.Evaluate(currentTime.x) * headbobIntensity.x;
            currentPos.y = headbobCurveY.Evaluate(currentTime.y) * headbobIntensity.y;

            smoothTime = 0.1f;
        }
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = new(currentPos.x, currentPos.y, 0);
        Vector3 desiredPos = Vector3.SmoothDamp(headbobTarget.localPosition, targetPos, ref velocity, smoothTime);

        headbobTarget.localPosition = desiredPos;
    }
}
