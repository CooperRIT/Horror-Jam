using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAudio : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] HeartEventChannel heartEventChannel;
    [SerializeField] List<AudioPitcherSO> heartBeatSFX;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void HeartState(int state)
    {
        switch (state)
        {
            default:
                heartBeatSFX[0].Play(audioSource);
                break;
            case 1:
                heartBeatSFX[1].Play(audioSource);
                break;
            case 2:
                heartBeatSFX[2].Play(audioSource);
                break;
            case 3:
                heartBeatSFX[3].Play(audioSource);
                break;
        }
    }
    private void OnEnable() => heartEventChannel.Stage += HeartState;
    private void OnDisable() => heartEventChannel.Stage -= HeartState;
}
