using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channel/Death", fileName = "DeathEventChannel", order = 2)]
public class DeathEventChannel : ScriptableObject
{
    public delegate void OnDeath(Transform player);
    public event OnDeath KillPlayer;

    public void TriggerEvent(Transform player) => KillPlayer?.Invoke(player);
}
