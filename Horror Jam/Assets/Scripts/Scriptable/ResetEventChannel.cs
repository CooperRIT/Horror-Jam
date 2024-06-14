using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Reset")]
public class ResetEventChannel : ScriptableObject
{
    public delegate void ResetEvent();
    public event ResetEvent Reset;
    public void EventTrigger() => Reset?.Invoke();

    int valveIndex;
    public int ValveIndex { get { return valveIndex; } set { valveIndex = value; } }
}
