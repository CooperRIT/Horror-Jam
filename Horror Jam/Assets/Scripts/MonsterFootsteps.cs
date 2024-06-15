using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFootsteps : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] AudioPitcherSO audioPitcherSO;

    AudioSource source;

    private void Start() => source = GetComponent<AudioSource>();
    public void PlayAudio()
    {
        audioPitcherSO.Play(source);
    }
}
