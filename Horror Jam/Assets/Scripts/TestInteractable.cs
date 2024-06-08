using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteract
{
    public string Prompt => "Press E for candy";

    public void Interact()
    {
        Debug.Log("Your IP: 173.28.82.1");
    }
}
