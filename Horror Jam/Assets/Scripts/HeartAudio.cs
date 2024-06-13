using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAudio : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] HeartEventChannel heartEventChannel;
    [SerializeField] List<AudioClip> heartBeatSFX;

    private AudioSource audioSource;

    private void Start() => audioSource = GetComponent<AudioSource>();

    void HeartState(int state)
    {
        switch (state)
        {
            default:
                audioSource.clip = null;
                audioSource.Play();
                break;
            case 1:
                audioSource.clip = heartBeatSFX[0];
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = heartBeatSFX[1];
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = heartBeatSFX[2];
                audioSource.Play();
                break;
        }
    }
    private void OnEnable() => heartEventChannel.Stage += HeartState;
    private void OnDisable() => heartEventChannel.Stage -= HeartState;
}
