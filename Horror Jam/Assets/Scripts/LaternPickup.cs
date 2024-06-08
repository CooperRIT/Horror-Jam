using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaternPickup : MonoBehaviour, IInteract
{
    public string Prompt => "Press [E] to pickup lantern";

    public void Interact()
    {
        Debug.Log("Your IP: 173.28.82.1");
    }
}
