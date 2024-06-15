using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaternPickup : MonoBehaviour, IInteract
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [Header("Lantern Reference")]
    [SerializeField] private GameObject lantern;

    [Header("Interact Settings")]
    [SerializeField] private string interactPrompt = "Press [E] to pickup lantern";
    public string Prompt { get { return interactPrompt; } }

    public void Interact()
    {
        lantern.SetActive(true);
        gameObject.SetActive(false);

        uiEventChannel.TriggerEvent("Hold [LMB] to charge lantern");
        Invoke(nameof(ClearText), 5f);
    }
    public void ExitInteract()
    {

    }

    void ClearText() => uiEventChannel.TriggerEvent(string.Empty);
}
