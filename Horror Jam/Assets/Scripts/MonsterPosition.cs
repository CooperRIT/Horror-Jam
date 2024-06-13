using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPosition : MonoBehaviour
{
    public static MonsterPosition Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else 
            Instance = this;
    }
}
