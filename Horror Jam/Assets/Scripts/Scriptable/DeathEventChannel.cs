using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Death", fileName = "DeathEventChannel", order = 2)]
public class DeathEventChannel : ScriptableObject
{
    public delegate void OnKilledByCultist(int cutSceneIndex);
    public event OnKilledByCultist CultistKill;
    public delegate void OnDeath();
    public event OnDeath KillPlayer;

    public void TriggerCultistDeathEvent(int cutSceneIndex) => CultistKill?.Invoke(cutSceneIndex);
    public void TriggerRespawn() => KillPlayer?.Invoke();
}
