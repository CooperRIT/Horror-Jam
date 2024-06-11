using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Sound Source")]
public class SoundSourceSO : ScriptableObject
{
    [Tooltip("The level of sound produced when active")]
    public float audioLevel;
}
