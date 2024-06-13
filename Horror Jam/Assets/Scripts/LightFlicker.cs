using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BelowDeck.MiniUtil;
using UnityEngine.Audio;

public class LightFlicker : MonoBehaviour
{
    [Header("Light Flicker Settings")]
    [SerializeField] float flickerDistance;

    private Vector3 enemyPosition => MonsterPosition.Instance.transform.position;

    private AudioSource audioSource;

    private Light lightSource;

    bool isLightBroken;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lightSource = GetComponentInChildren<Light>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (isLightBroken) return;

        if (MiniUtil.DistanceNoY(transform.position, enemyPosition) > flickerDistance) return;

        lightSource.intensity = 0;
        audioSource.Play();
        isLightBroken = true;
    }
}
