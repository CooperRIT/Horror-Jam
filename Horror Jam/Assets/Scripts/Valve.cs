using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Valve : MonoBehaviour, IInteract
{
    [Header("Scriptable Object Reference")]
    [SerializeField] SoundEventChannel soundEventChannel;
    [SerializeField] AudioPitcherSO audioPitcherSO;
    [SerializeField] UIEventChannel uiEventChannel;
    [SerializeField] ScreenShakeEventChannel screenShake;
    [SerializeField] HeartEventChannel heartEventChannel;

    [Header("Interact Settings")]
    [SerializeField] private string interactPrompt = "Hold [E] to close valve";
    public string Prompt { get { return interactPrompt; } }

    [Header("Valve Settings")]
    [Tooltip("The amount of seconds the player has to interact with the valve")]
    [SerializeField] private float maxTurningAmount = 10f;
    
    [Tooltip("How long it takes the valve to speed up")]
    [SerializeField] private float spinRateMultiplier = 3f;

    [Tooltip("The max speed the valve can rotate")]
    [SerializeField] private float maxSpinRate = 3f;

    [Tooltip("The object that is being rotated")]
    [SerializeField] private Transform valveObject;

    private float valveSpinRate = 0f;

    private float currentTurningAmount;

    private bool canTurn;

    private bool valveClosed;

    [Header("Screen Shake Settings")]
    [SerializeField] private float duration = 1f;

    [SerializeField] private float intensity = 0.3f;

    [Header("Audio Settings")]
    [Tooltip("How much time passes between each sound queue")]
    [SerializeField] private float playSoundInterval = 1f;

    private AudioSource source;

    private float playSoundTime;

    //MODIFIYED BY COOPER
    [Header("Flesh Wall Reference")]
    [SerializeField] List<FleshWall> fleshWalls = new List<FleshWall>();

    [Header("Heart Settings")]
    [SerializeField] int heartState;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (valveClosed) return;

        if (currentTurningAmount >= maxTurningAmount && !valveClosed)
        {
            //Prevent further interactions
            valveClosed = true;

            //Stop rotating
            canTurn = false;

            //Set new prompt
            interactPrompt = "Valve closed";
            uiEventChannel.TriggerEvent(interactPrompt);

            //Shake Screen
            screenShake.TriggerEvent(duration, intensity);

            //Change heart audio
            heartEventChannel.TriggerEvent(heartState);

            //Collapse flesh walls
            foreach(FleshWall fleshWall in fleshWalls)
            {
                fleshWall.CollapseWall();
            }
            return;
        }

        if (canTurn) return;

        canTurn = true;

        StartCoroutine(StartTurning());
    }
    public void ExitInteract()
    {
        canTurn = false;
    }

    IEnumerator StartTurning()
    {
        while (canTurn)
        {
            if (valveSpinRate < maxSpinRate)
            {
                valveObject.Rotate(0, 0, valveSpinRate);
                valveSpinRate += spinRateMultiplier * Time.deltaTime;
            }
            else
                valveObject.Rotate(0, 0, maxSpinRate);

            if (playSoundTime < Time.time)
            {
                playSoundTime = Time.time + playSoundInterval;
                audioPitcherSO.Play(source);
            }

            currentTurningAmount += Time.deltaTime;

            soundEventChannel.CurrentSoundLevel += audioPitcherSO.audioLevel * Time.deltaTime;

            yield return null;
        }
        StartCoroutine(StopTurning());
        StartCoroutine(SoundDecay());
    }

    IEnumerator StopTurning()
    {
        while (!canTurn)
        {
            if (valveSpinRate > 0)
            {
                valveObject.Rotate(0, 0, valveSpinRate);
                valveSpinRate -= spinRateMultiplier * Time.deltaTime;
            }
            yield return null;
        }
    }

    IEnumerator SoundDecay()
    {
        float currentTime = 0;

        while (maxTurningAmount > currentTime) 
        {
            currentTime += Time.deltaTime;
            soundEventChannel.CurrentSoundLevel -= audioPitcherSO.audioLevel * Time.deltaTime;
            yield return null;
        }
    }
}
