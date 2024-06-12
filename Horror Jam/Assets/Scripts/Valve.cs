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

    [Header("Interact Settings")]
    [SerializeField] private string interactPrompt = "Hold [E] to close valve";
    public string Prompt { get { return interactPrompt; } }

    [Header("Valve Settings")]
    [Tooltip("The amount of seconds the player has to interact with the valve")]
    [SerializeField] private float maxTurningAmount = 10f;

    [SerializeField] private float valveSpinRate = 0f;

    [SerializeField] private float spinRateMultiplier = 3f;

    [SerializeField] private float maxSpinRate = 5f;

    [SerializeField] private Transform valveObject;

    private float currentTurningAmount;

    private bool canTurn;

    public void Interact()
    {
        if (currentTurningAmount >= maxTurningAmount)
        {
            canTurn = false;
            Debug.Log("Valve Closed!");
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

            currentTurningAmount += Time.deltaTime;

            soundEventChannel.CurrentSoundLevel += audioPitcherSO.audioLevel;

            yield return null;
        }
        Debug.Log("Stopping!");
        StartCoroutine(StopTurning());
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
}
