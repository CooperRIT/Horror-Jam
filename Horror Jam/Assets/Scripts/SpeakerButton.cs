using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerButton : MonoBehaviour, IInteract
{
    [SerializeField] SoundMonster soundMonster;
    [SerializeField] Transform runPosition;
    [SerializeField] AudioSource audioSource;
    public string Prompt => "Press [E] to distract";

    public void ExitInteract()
    {

    }

    public void Interact()
    {
        audioSource.Play();
        soundMonster.DeAgroMonster(10);
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
