using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BelowDeck.MiniUtil;

public class LightingLOD : MonoBehaviour
{
    [SerializeField] private float despawnDistance = 30f;

    [SerializeField] private Light spotLight;

    private Vector3 Player => CameraLOD.Instance.transform.position;

    private void Update()
    {
        if (MiniUtil.DistanceNoY(transform.position, Player) > despawnDistance)
            spotLight.enabled = false;
        else
            spotLight.enabled = true;
    }
}
