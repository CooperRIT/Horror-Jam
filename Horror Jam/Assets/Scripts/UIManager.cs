using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private UIEventChannel uiEventChannel;

    [SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] float timePerLetter = .01f;

    WaitForSeconds timedLetters;

    string prompt;

    bool typing;

    [Header("Audio Pitcher Settings")]
    [Tooltip("The audio clips that will be played when walking")]
    [SerializeField] private AudioClip[] audioClips;

    [Tooltip("Minimum pitch the footstep can be")]
    [SerializeField] private float minPitch = 0.95f;

    [Tooltip("Maximum pitch the footstep can be")]
    [SerializeField] private float maxPitch = 1.1f;

    [SerializeField] private AudioSource audioSource;

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
            AudioPitcher();
            newPrompt += prompt[i];
            promptText.text = newPrompt;
            yield return timedLetters;
        }
        Debug.Log("Done");
        typing = false;
    }

    void AudioPitcher()
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);

        int index = Random.Range(0, audioClips.Length);
        AudioClip clip = audioClips[index];
        audioSource.PlayOneShot(clip);
    }

    public void LoadScene(int sceneNumber) => SceneManager.LoadScene(sceneNumber);
    public void Quit() => Application.Quit();

    private void OnEnable() => uiEventChannel.SetPrompt += SetPromptText;
    private void OnDisable() => uiEventChannel.SetPrompt -= SetPromptText;
}
