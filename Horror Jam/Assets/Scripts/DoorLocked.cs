using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour, IInteract
{
    [Header("Interact Settings")]
    [SerializeField] private string interactPrompt = "The door is locked";
    public string Prompt { get { return interactPrompt; } }

    public void Interact()
    {
        //Play locked door sound effect
    }
}
