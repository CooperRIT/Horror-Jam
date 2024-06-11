using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour, IInteract
{
    [Header("Interact Settings")]
    [SerializeField] private string interactPrompt = "Hold [E] to close valve";
    Animator animator;
    public string Prompt { get { return interactPrompt; } }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
