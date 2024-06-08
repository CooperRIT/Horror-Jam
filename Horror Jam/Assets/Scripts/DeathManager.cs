using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] DeathEventChannel deathEventChannel;
    [SerializeField] RespawnManager respawnManager;
    bool restarting;

    public void OnDeath(Transform player)
    {
        respawnManager.RestartScene(player);
    }

    private void OnEnable() => deathEventChannel.KillPlayer += OnDeath;
    private void OnDisable() => deathEventChannel.KillPlayer -= OnDeath;
}
