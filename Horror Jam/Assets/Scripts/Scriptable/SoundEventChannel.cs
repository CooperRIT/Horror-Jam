using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Sound Event")]
public class SoundEventChannel : ScriptableObject
{
    public delegate float SoundLevel(float soundLevel, bool decayLevel);
    public event SoundLevel UpdateSoundLevel;
    public void TriggerEvent(float soundLevel, bool decayLevel) => UpdateSoundLevel?.Invoke(soundLevel, decayLevel);
}
