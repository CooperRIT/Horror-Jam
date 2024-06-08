using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [Header("Interactor Settings")]
    [SerializeField] private int interactLayer;

    private bool canInteract;
    public void CanInteract() => canInteract = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != interactLayer) return;

        //Set prompt text
        if (other.gameObject.TryGetComponent(out IInteract interact))
            uiEventChannel.TriggerEvent(interact.Prompt);

        if (!canInteract) return;

        interact.Interact();

        canInteract = false;
    }
    private void OnTriggerExit(Collider other) => uiEventChannel.TriggerEvent(string.Empty);
}
public interface IInteract
{
    string Prompt { get; }
    public void Interact();
}