using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] DeathEventChannel deathEventChannel;
    [SerializeField] RespawnManager respawnManager;

    [SerializeField] CutsceneManager cutsceneManager;

    public void OnKilledByCultist(int cutSceneIndex)
    {
        //Change this value for different cutscenes
        //TEMP FOR TESTING
        cutsceneManager.OnStartCutScene(cutSceneIndex);
    }

    /// <summary>
    /// For actually respawning the player
    /// </summary>
    public void OnDeath()
    {
        respawnManager.RestartScene();
    }

    private void OnEnable()
    {
        deathEventChannel.CultistKill += OnKilledByCultist;
        deathEventChannel.KillPlayer += OnDeath;
    }
    private void OnDisable()
    {
        deathEventChannel.CultistKill -= OnKilledByCultist;
        deathEventChannel.KillPlayer -= OnDeath;
    }
}
