using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunkerDoor : MonoBehaviour, IInteract
{
    [Header("Scene Settings")]
    [SerializeField] private int sceneNumber;

    public string Prompt => "Press [E] to go below deck";

    public void Interact() => SceneManager.LoadScene(sceneNumber);
}
