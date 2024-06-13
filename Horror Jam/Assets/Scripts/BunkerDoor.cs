using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunkerDoor : MonoBehaviour, IInteract
{
    [Header("Scene Settings")]
    [SerializeField] private int sceneNumber;

    [Header("Interact Settings")]
    [SerializeField] private string interactPrompt = "Press [E] to go below deck";
    public string Prompt { get { return interactPrompt; } }

    public void Interact() => SceneManager.LoadScene(sceneNumber);
    public void ExitInteract()
    {
        //Empty
    }
}
