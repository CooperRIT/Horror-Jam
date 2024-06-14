using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMeter : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] SoundEventChannel soundEventChannel;


    [Header("Light Settings")]
    [SerializeField] List<Light> soundLights;

    [SerializeField] float blinkingInterval = 0.5f;

    float blinkingTime;

    Light currentLight;

    bool isSameLight = true;

    float CurrentSoundLevel => soundEventChannel.CurrentSoundLevel;

    private void Start()
    {
        currentLight = soundLights[0];
    }

    private void Update()
    {
        if (CurrentSoundLevel < 30f)
        {
            currentLight = soundLights[0];
            isSameLight = false;
        }
        else if (CurrentSoundLevel < 70)
        {
            currentLight = soundLights[1];
            isSameLight = false;
        }
        else
        {
            currentLight = soundLights[2];
            isSameLight = false;
        }

        if (!isSameLight)
            StartCoroutine(BlinkLight());
    }

    IEnumerator BlinkLight()
    {
        isSameLight = true;
        while (!isSameLight)
        {
            currentLight.enabled = false;
            yield return new WaitForSeconds(blinkingInterval);
            currentLight.enabled = true;
            yield return new WaitForSeconds(blinkingInterval);
        }
    }
}
