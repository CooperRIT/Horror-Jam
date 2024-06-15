using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightStates
{
    Green,
    Yellow,
    Red,
}

public class SoundMeter : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] SoundEventChannel soundEventChannel;


    LightStates state;

    [Header("Light Settings")]
    [SerializeField] List<Light> soundLights;

    [SerializeField] float blinkingInterval = 0.5f;

    int lightIndex;

    Light currentLight;

    bool isSameLight;

    float CurrentSoundLevel => soundEventChannel.CurrentSoundLevel;

    private void Start()
    {
        state = LightStates.Green;
        currentLight = soundLights[0];
    }

    private void Update()
    {
        switch (state)
        {
            case LightStates.Green:
                if (CurrentSoundLevel >= 35f)
                {
                    state = LightStates.Yellow;
                    isSameLight = false;
                }
                break;

            case LightStates.Yellow:
                if (CurrentSoundLevel >= 50f)
                {
                    state = LightStates.Red;
                    isSameLight = false;
                }   
                else if (CurrentSoundLevel < 35f)
                {
                    state = LightStates.Green;
                    isSameLight = false;
                }
                break;

            case LightStates.Red:
                if (CurrentSoundLevel < 50f)
                {
                    state = LightStates.Yellow;
                    isSameLight = false;
                }
                break;
        }

        if (!isSameLight)
        {
            //Re-enable current light
            currentLight.enabled = true;

            //Change current light
            currentLight = soundLights[(int)state];

            //Play blinking 
            isSameLight = true;
            StartCoroutine(BlinkLight());
        }
    }

    IEnumerator BlinkLight()
    {  
        while (isSameLight)
        {
            currentLight.enabled = false;
            yield return new WaitForSeconds(blinkingInterval);
            currentLight.enabled = true;
            yield return new WaitForSeconds(blinkingInterval);
        }
    }
}
