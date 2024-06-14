using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTrigger : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private EventChannel eventChannel;
    [SerializeField] private ResetEventChannel resetEventChannel;


    [Header("Event Trigger Settings")]
    [Tooltip("The ID of the event being called")]
    [SerializeField] private int eventID;

    [SerializeField] private int playerLayer;

    [SerializeField] private Transform vfxTransform;

    [SerializeField] private bool destroyOnTrigger;

    [SerializeField] int valveIndex;

    private Collider triggerCollider;

    private void Start() => triggerCollider = GetComponent<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != playerLayer) return;

        eventChannel.TriggerEvent(eventID, vfxTransform);

        resetEventChannel.ValveIndex = valveIndex;

        if (!destroyOnTrigger) return;

        triggerCollider.enabled = false;
    }
    void OnReset() => triggerCollider.enabled = true;
    private void OnEnable() => resetEventChannel.Reset += OnReset;
    private void OnDisable() => resetEventChannel.Reset -= OnReset;
}
