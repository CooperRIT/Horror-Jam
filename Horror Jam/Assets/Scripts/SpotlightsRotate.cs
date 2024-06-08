using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightsRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;

    [SerializeField] private float rotationTime;

    private float currentRotationTime;

    private Transform startPos;
    private Transform endPos;

    private bool reverseDirection;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = new (transform.rotation.x, minYRotation, transform.rotation.z, transform.rotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        if (!reverseDirection)
            transform.Rotate(transform.rotation.x, maxYRotation, transform.rotation.z, Space.World);
        else
            transform.Rotate(transform.rotation.x, minYRotation, transform.rotation.z, Space.World);

        if (currentRotationTime < Time.time)
        {
            currentRotationTime = Time.time + rotationTime;
            reverseDirection = !reverseDirection;
        }
    }
}
