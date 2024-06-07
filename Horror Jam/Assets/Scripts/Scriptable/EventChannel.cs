using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Events")]
public class EventChannel : ScriptableObject
{
    public delegate void EventTree(int eventID, Transform vfxTransform);
    public event EventTree CallEvent;

    public void TriggerEvent(int eventID, Transform vfxTransform) => CallEvent?.Invoke(eventID, vfxTransform);
}
