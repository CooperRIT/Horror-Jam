using System;
using UnityEngine;

[Serializable]
public struct RangedFloat
{
    public float minValue;
    public float maxValue;
}
public abstract class AudioSO : ScriptableObject
{
    public abstract void Play(AudioSource source);
}
