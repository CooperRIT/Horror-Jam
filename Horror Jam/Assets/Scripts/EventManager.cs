using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventChannel eventChannel;

    public void EventTree(int eventID)
    {
        switch (eventID)
        {
            case 0:
                Debug.Log("You are stinky, go shower!");
                break;

            default:
                throw new System.Exception("Event ID out of range");
        }
    }
    private void OnEnable() => eventChannel.CallEvent += EventTree;
    private void OnDisable() => eventChannel.CallEvent -= EventTree;
}
