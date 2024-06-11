using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioPitcher : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private AudioPitcherSO audioPitcherSO;
    [SerializeField] private SoundEventChannel soundEventChannel;

    [Header("Footstep Settings")]
    [Tooltip("How long it takes to play when you start walking")]
    [SerializeField] private float playSpeed = 1f;

    [Tooltip("Pause between footsteps")]
    [SerializeField] private float audioInterval = 0.3f;

    private PlayerController playerController;
    private AudioSource audioSource;

    private float currentTime;

    void Start()
    {
        playerController = GetComponent<PlayerController>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!playerController.IsGrounded || !playerController.IsMoving)
        {
            currentTime = 0f;
            soundEventChannel.currentSoundLevel -= audioPitcherSO.decayLevel * Time.deltaTime;
        }   
        else if (playerController.IsMoving)
            currentTime += playSpeed / 10 * Time.deltaTime;

        if (currentTime > audioInterval)
        {
            currentTime = 0.0f;

            audioPitcherSO.Play(audioSource);
            soundEventChannel.currentSoundLevel += audioPitcherSO.audioLevel * Time.deltaTime;
        }
    }
}
