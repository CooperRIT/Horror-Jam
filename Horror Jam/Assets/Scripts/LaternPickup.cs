using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaternPickup : MonoBehaviour, IInteract
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [Header("Lantern Reference")]
    [SerializeField] private GameObject lantern;

    public string Prompt => "Press [E] to pickup lantern";

    public void Interact()
    {
        lantern.SetActive(true);
        gameObject.SetActive(false);

        uiEventChannel.TriggerEvent(string.Empty);
    }
}
