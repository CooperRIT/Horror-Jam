using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Sound Event")]
public class SoundEventChannel : ScriptableObject
{
    private float currentSoundLevel = 0;
    public float CurrentSoundLevel 
    {
        get {return Mathf.Clamp(currentSoundLevel, 0, 100); } 
        set 
        { 
            currentSoundLevel = value;
            currentSoundLevel = Mathf.Clamp(currentSoundLevel, 0, 100);
        }
    }
}
