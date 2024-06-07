using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Transform playerCamera;
    Vector2 inputDirection => playerInput.InputDirection;
    Transform mainTransform;
    Rigidbody mainRB;

    [Header("Player Movement Stats")]
    [SerializeField] float speed = 1;
    [SerializeField] float maxSpeed = 2;



    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        mainTransform = transform.parent;
        mainRB = mainTransform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CorrectMagnitude() < maxSpeed)
        {
            mainRB.velocity +=  CalculateMovement() * speed * Time.deltaTime;
        }
    }

    Vector3 CalculateMovement()
    {
        return playerCamera.forward * inputDirection.y + playerCamera.right * inputDirection.x;
    }

    float CorrectMagnitude()
    {
        return new Vector3(mainRB.velocity.x, 0, mainRB.velocity.z).magnitude;
    }
}
