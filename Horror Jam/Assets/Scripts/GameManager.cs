using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject player;
     
    public GameObject Player { get { return player; } }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else 
            Instance = this;
    }
}
