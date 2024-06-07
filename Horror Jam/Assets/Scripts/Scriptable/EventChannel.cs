using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Events")]
public class EventChannel : ScriptableObject
{
    public delegate void EventTree(int eventID);
    public event EventTree CallEvent;

    public void TriggerEvent(int eventID) => CallEvent?.Invoke(eventID);
}
