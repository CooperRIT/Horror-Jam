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
    public void AnimEventRespawnPlayer()
    {
        deathEventChannel.TriggerRespawn();
        transform.gameObject.SetActive(false);
        animator.SetTrigger("SetStateToNull");
    }
}
