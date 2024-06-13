using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] DeathEventChannel deathEventChannel;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// Public void for anim event playing
    /// </summary>
    public void AnimEventRespawnPlayer(int cutsceneIndex)
    {
        deathEventChannel.TriggerRespawn();
        transform.GetChild(cutsceneIndex).gameObject.SetActive(false);
        animator.SetTrigger("SetStateToNull");
    }

    /// <summary>
    /// Put in the index of the cutscene, Plays audio from source of the FIRST child of that child
    /// </summary>
    /// <param name="childIndex"></param>
    public void PlaySound(int childIndex)
    {
        transform.GetChild(childIndex).GetChild(0).GetComponent<AudioSource>().Play();
    }

    public void DisablePlayer(int childIndex)
    {
        
    }

    public void EnablePlayer(int childIndex)
    {
        transform.GetChild(childIndex).GetChild(1).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(childIndex).GetChild(1).GetChild(0).gameObject.SetActive(false);
    }

    public void TeleportToPlayer()
    {

    }
}
