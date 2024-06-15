using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerButton : MonoBehaviour, IInteract
{
    [SerializeField] SoundMonster soundMonster;
    [SerializeField] Transform runPosition;
    [SerializeField] AudioSource audioSource;
    public string Prompt => throw new System.NotImplementedException();

    public void ExitInteract()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        audioSource.Play();
        soundMonster.RunToPosition(runPosition.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
