using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    Vector3 currentSpawnPoint;
    [SerializeField] List<CultistAi> cultists;

    private void Awake()
    {
        cultists = new List<CultistAi>();
        Transform enemies = GameObject.Find("Enemies").transform;

        if (enemies == null)
        {
            throw new System.Exception("Put all of your enemies under a gameobject called 'Enemies'");
        }

        for(int i = 0; i < enemies.childCount; i++)
        {
            cultists.Add(enemies.GetChild(i).GetChild(0).GetComponent<CultistAi>());
        }
    }

    public void SetSpawnPoint(Vector3 spawnPointPosition)
    {
        currentSpawnPoint = spawnPointPosition;
    }

    public Vector3 CurrentSpawnPoint
    {
        get { return currentSpawnPoint; }
    }

    public void RestartScene(Transform player)
    {
        foreach(CultistAi cultistAi in cultists)
        {
            cultistAi.RestartCultist();
        }
        player.transform.position = currentSpawnPoint;
        Debug.Log("restarted cultists and player");
    }

}
