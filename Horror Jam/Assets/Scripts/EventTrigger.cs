using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTrigger : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private EventChannel eventChannel;

    [Tooltip("The ID of the event being called")]
    [SerializeField] private int eventID;

    [SerializeField] private int playerLayer;

    [SerializeField] private bool destroyOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != playerLayer) return;

        eventChannel.TriggerEvent(eventID);

        if (!destroyOnTrigger) return;

        Destroy(gameObject);
    }
}
