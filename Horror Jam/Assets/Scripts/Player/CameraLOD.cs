using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLOD : MonoBehaviour
{
    public static CameraLOD Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
