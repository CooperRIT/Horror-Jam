using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [SerializeField] private TextMeshProUGUI promptText;

    private void SetPromptText(string prompt) => promptText.text = prompt;

    private void OnEnable() => uiEventChannel.SetPrompt += SetPromptText;
    private void OnDisable() => uiEventChannel.SetPrompt -= SetPromptText;
}
