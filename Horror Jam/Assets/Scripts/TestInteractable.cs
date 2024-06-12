using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteract
{
    [SerializeField] private float clearDistance = 5f;
    public float Distance { get { return clearDistance; } }

    [SerializeField] private string interactPrompt = "Press E for candy";
    public string Prompt { get { return interactPrompt; } }

    public void Interact()
    {
        Debug.Log("Your IP: 173.28.82.1");
    }

    public void ExitInteract()
    {
        
    }
}
