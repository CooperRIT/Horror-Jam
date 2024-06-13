using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Heart")]
public class HeartEventChannel : ScriptableObject
{
    public delegate void HeartStage(int stage);
    public event HeartStage Stage;
    public void TriggerEvent(int stage) => Stage?.Invoke(stage);
}
