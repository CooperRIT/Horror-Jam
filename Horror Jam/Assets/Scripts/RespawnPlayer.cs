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
    // Start is called before the first frame update
    public void AnimEventRespawnPlayer()
    {
        deathEventChannel.TriggerRespawn();
        animator.SetTrigger("SetStateToNull");
    }
}
