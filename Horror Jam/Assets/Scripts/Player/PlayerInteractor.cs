using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BelowDeck.MiniUtil;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [Header("Interactor Settings")]
    [SerializeField] private int interactLayer;

    private bool canInteract;
    public void CanInteract() => canInteract = true;

    public void StopInteracting() => canInteract = false;

    bool interactable;

    IInteract interact;

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.gameObject.layer != interactLayer;

        if (other.gameObject.TryGetComponent(out interact))
        {
            uiEventChannel.TriggerEvent(interact.Prompt);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (interactable) return;

        if (interact == null) return;

        if (canInteract)
            interact.Interact();
        else
            interact.ExitInteract();
    }
    private void OnTriggerExit(Collider other)
    {
        uiEventChannel.TriggerEvent(string.Empty);
        interact = null;
    }
}
public interface IInteract
{
    string Prompt { get; }
    public void Interact();
    public void ExitInteract();
}