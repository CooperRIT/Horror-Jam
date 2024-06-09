using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingLOD : MonoBehaviour
{
    [SerializeField] private float despawnDistance = 30f;

    [SerializeField] private Light spotLight;

    private float playerZPos => CameraLOD.Instance.transform.position.z;

    private void Update()
    {
        if (Vector3.Distance(new Vector3(0, 0, transform.position.z), new Vector3(0, 0, playerZPos)) > despawnDistance)
            spotLight.enabled = false;
        else
            spotLight.enabled = true;
    }
}
