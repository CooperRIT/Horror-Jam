using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Sound Event")]
public class SoundEventChannel : ScriptableObject
{
    private float currentSoundLevel;
    public float CurrentSoundLevel { get {return Mathf.Clamp(currentSoundLevel, 0, 100); } }
}
