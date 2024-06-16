using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadInteractable : MonoBehaviour, IInteract
{
    public string Prompt => currentPrompt;

    [SerializeField] string currentPrompt = "...";

    public void ExitInteract()
    {
        
    }

    public void Interact()
    {
        
    }
}
