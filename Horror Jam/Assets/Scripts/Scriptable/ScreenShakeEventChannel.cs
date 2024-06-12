using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Screen Shake")]
public class ScreenShakeEventChannel : ScriptableObject
{
    public delegate void ShakeScreen(float duration, float intensity);
    public event ShakeScreen ScreenShake;
    public void TriggerEvent(float duration, float intensity) => ScreenShake?.Invoke(duration, intensity);
}
