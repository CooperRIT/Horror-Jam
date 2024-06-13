using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [SerializeField] private AudioPitcherSO audioPitcherSO;

    [Header("UI Element References")]
    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] float timePerLetter = .01f;

    WaitForSeconds timedLetters;

    string prompt;

    bool typing;

    [SerializeField] private AudioSource audioSource;

    private void SetPromptText(string prompt)
    {
        if (prompt == string.Empty)
        {
            typing = false;
            StopAllCoroutines();
            promptText.text = string.Empty;
            return;
        }
        this.prompt = prompt;
        if (typing) return;
        StartCoroutine(nameof(DisplayText));
    }

    IEnumerator DisplayText()
    {
        typing = true;
        uiEventChannel.IsTextFinished = false;
        timedLetters = new WaitForSeconds(timePerLetter);
        string newPrompt = "";
        for(int i = 0; i < prompt.Length; i++)
        {
            audioPitcherSO.Play(audioSource);
            newPrompt += prompt[i];
            promptText.text = newPrompt;
            yield return timedLetters;
        }
        typing = false;
        uiEventChannel.IsTextFinished = true;
    }

    public void LoadScene(int sceneNumber) => SceneManager.LoadScene(sceneNumber);
    public void Quit() => Application.Quit();

    private void OnEnable() => uiEventChannel.SetPrompt += SetPromptText;
    private void OnDisable() => uiEventChannel.SetPrompt -= SetPromptText;
}
