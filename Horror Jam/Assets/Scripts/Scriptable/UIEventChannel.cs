using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Event Channel/UI", fileName = "UIEventChannel", order = 1)]
public class UIEventChannel : ScriptableObject
{
    private bool isTextFinished;
    public bool IsTextFinished { 
        get { return isTextFinished;  }
        set { isTextFinished = value; }
    } 

    public delegate void UpdatePrompt(string prompt);
    public event UpdatePrompt SetPrompt;
    public void TriggerEvent(string prompt) => SetPrompt?.Invoke(prompt);
}
