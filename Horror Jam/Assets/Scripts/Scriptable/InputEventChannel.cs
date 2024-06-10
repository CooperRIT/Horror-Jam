using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Input", fileName = "EventChannel")]
public class InputEventChannel : ScriptableObject
{
    public delegate void SetPlayerInput(bool canInput);
    public event SetPlayerInput CanInput;
    public void TriggerEvent(bool canInput) => CanInput?.Invoke(canInput);
}
