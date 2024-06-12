using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeConflict : MonoBehaviour, IInteract
{
    public string Prompt => "Press [E] to cause merge conflict";

    public void Interact(bool isInteracting)
    {
        Application.Quit();
    }
}
