using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioPitcher : MonoBehaviour
{
    [Header("Footstep Settings")]
    [Tooltip("The audio clips that will be played when walking")]
    [SerializeField] private AudioClip[] audioClips;

    [Tooltip("Minimum pitch the footstep can be")]
    [SerializeField] private float minPitch = 0.95f;

    [Tooltip("Maximum pitch the footstep can be")]
    [SerializeField] private float maxPitch = 1.1f;

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
            currentTime = 0f;
        else if (playerController.IsMoving)
            currentTime += playSpeed / 10 * Time.deltaTime;

        if (currentTime > audioInterval)
        {
            currentTime = 0.0f;

            audioSource.pitch = Random.Range(minPitch, maxPitch);

            int index = Random.Range(0, audioClips.Length);
            AudioClip clip = audioClips[index];
            audioSource.PlayOneShot(clip);
        }
    }
}
