using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour, IInteract
{
    public string Prompt => "The Door is locked";

    public void Interact()
    {
        //Play locked door sound effect
    }
}
