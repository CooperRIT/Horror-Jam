using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Death", fileName = "DeathEventChannel", order = 2)]
public class DeathEventChannel : ScriptableObject
{
    //For triggering the KilledByCultistCutscene
    public delegate void OnKilledByCultist(int cutSceneIndex);
    public event OnKilledByCultist CultistKill;

    ///For respawning the player
    public delegate void OnDeath();
    public event OnDeath KillPlayer;

    public void startCutSceneOnKill(int cutSceneIndex) => CultistKill?.Invoke(cutSceneIndex);
    public void TriggerRespawn() => KillPlayer?.Invoke();
}
