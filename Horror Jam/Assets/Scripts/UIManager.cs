using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] float timePerLetter = .01f;

    WaitForSeconds timedLetters;

    string prompt;

    bool typing;

    private void SetPromptText(string prompt)
    {
        if(prompt == string.Empty)
        {
            Debug.Log("stopped");
            typing = false;
            StopAllCoroutines();
            promptText.text = string.Empty;
            return;
        }
        Debug.Log("Started");
        this.prompt = prompt;
        if (typing) return;
        StartCoroutine(nameof(DisplayText));
    }

    IEnumerator DisplayText()
    {
        typing = true;
        timedLetters = new WaitForSeconds(timePerLetter);
        string newPrompt = "";
        for(int i = 0; i < prompt.Length; i++)
        {
            newPrompt += prompt[i];
            promptText.text = newPrompt;
            yield return timedLetters;
        }
        Debug.Log("Done");
        typing = false;
    }

    private void OnEnable() => uiEventChannel.SetPrompt += SetPromptText;
    private void OnDisable() => uiEventChannel.SetPrompt -= SetPromptText;
}
