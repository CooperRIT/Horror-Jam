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
}
