using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EventManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private EventChannel eventChannel;

    [Header("Events Refernces")]
    [SerializeField] private VisualEffect lightningSpawner;

    public void EventTree(int eventID)
    {
        switch (eventID)
        {
            case 0:
                Debug.Log("You are stinky, go shower!");
                break;

            case 1:
                lightningSpawner.Play();
                break;

            default:
                throw new System.Exception("Event ID out of range");
        }
    }
    private void OnEnable() => eventChannel.CallEvent += EventTree;
    private void OnDisable() => eventChannel.CallEvent -= EventTree;
}
