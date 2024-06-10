using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInput : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private InputEventChannel inputEventChannel;

    private PlayerController controller;

    private Rigidbody rb;
    private void Start()
    {
        controller = GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody>();
    }
    private void SetInput(bool canInput)
    {
        rb.velocity = new Vector3(0, 0, 0);
        controller.enabled = canInput;
    }
    private void OnEnable() => inputEventChannel.CanInput += SetInput;
    private void OnDisable() => inputEventChannel.CanInput += SetInput;
}
