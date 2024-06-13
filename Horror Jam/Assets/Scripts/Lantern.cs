using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lantern : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private AudioPitcherSO audioPitcherSO;
    [SerializeField] private SoundEventChannel soundEventChannel;
    float audioAcc = 0;

    Controls controls;
    bool isOn;

    [Header("Light Properties")]
    [SerializeField] Light spotLight;
    [SerializeField] float maxIntensity;
    [Tooltip("How much intensity is added per second")]
    [SerializeField] float intensityRate = 5;
    [SerializeField] float maxRange;
    float currentIntensity = 0;
    float currentRange = 0;

    [Header("WheelProperties")]
    [SerializeField] Transform wheel;
    [SerializeField] float maxSpinRate;
    float currentSpinRate;
    float spinRateRate = 2;
    Vector3 spinDirection;

    private AudioSource audioSource;

    private void Awake()
    {
        controls = new Controls();
        audioSource = GetComponentInParent<AudioSource>();
        //Reset Sound Values When Picked up
        soundEventChannel.CurrentSoundLevel = 0;
    }

    void Start()
    {
        spotLight.intensity = 0;
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.BasicControls.SpinLantern.performed += SpinLamp;
        controls.BasicControls.SpinLantern.canceled += SlowDownLamp;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.BasicControls.SpinLantern.performed -= SpinLamp;
        controls.BasicControls.SpinLantern.canceled -= SlowDownLamp;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpinLamp(InputAction.CallbackContext context)
    {
        if(isOn)
        {
            return;
        }

        isOn = true;
        StartCoroutine(nameof(SpinLampAction));
        audioSource.Play();
    }

    void SlowDownLamp(InputAction.CallbackContext context)
    {
        isOn = false;
    }

    IEnumerator SpinLampAction()
    {
        while (isOn)
        {
            if(spotLight.intensity < maxIntensity)
            {
                spotLight.intensity += intensityRate * Time.deltaTime;
            }
            if (currentSpinRate < maxSpinRate)
            {
                wheel.Rotate(currentSpinRate,0 , 0);
                currentSpinRate += spinRateRate * Time.deltaTime;
                audioSource.volume = currentSpinRate / maxSpinRate;
            }
            else
            {
                wheel.Rotate(maxSpinRate, 0, 0);
            }
            soundEventChannel.CurrentSoundLevel += audioPitcherSO.audioLevel * Time.deltaTime;
            audioAcc += audioPitcherSO.audioLevel * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(nameof(SlowDownLampAction));
    }

    IEnumerator SlowDownLampAction()
    {
        while (!isOn)
        {
            if (spotLight.intensity > 0)
            {
                if (audioAcc > 0)
                {
                    audioAcc -= audioPitcherSO.audioLevel * Time.deltaTime;
                    soundEventChannel.CurrentSoundLevel -= audioPitcherSO.audioLevel * Time.deltaTime;
                }
                spotLight.intensity -= intensityRate * Time.deltaTime;
            }
            if (currentSpinRate > 0)
            {
                wheel.Rotate(currentSpinRate, 0, 0);
                currentSpinRate -= spinRateRate * Time.deltaTime;
                audioSource.volume = currentSpinRate / maxSpinRate;
            }
            yield return null;
        }
    }
}
