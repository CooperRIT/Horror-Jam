using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTrigger : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private EventChannel eventChannel;


    [Header("Event Trigger Settings")]
    [Tooltip("The ID of the event being called")]
    [SerializeField] private int eventID;

    [SerializeField] private int playerLayer;

    [SerializeField] private Transform vfxTransform;

    [SerializeField] private bool destroyOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != playerLayer) return;

        eventChannel.TriggerEvent(eventID, vfxTransform);

        if (!destroyOnTrigger) return;

        Destroy(gameObject);
    }
}
