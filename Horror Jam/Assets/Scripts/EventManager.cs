using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class EventManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private EventChannel eventChannel;
    [SerializeField] private AudioPitcherSO audioPitcherSO;

    [Header("Events Refernces")]
    [SerializeField] private VisualEffect lightningSpawner;
    [SerializeField] private AudioSource audioSource;
    

    [Header("Light Settings")]
    [SerializeField] private List<Light> lightList;

    [Tooltip("How many lights you want to enable in one event trigger")]
    [SerializeField] private int lightIterations;

    [SerializeField] private float lightIntensity = 25f;

    private int lightIndex = 0;

    [Header("Respawn Manager")]
    [SerializeField] private RespawnManager respawnManager;

    [Header("Scene 2 References")]
    [SerializeField] private SoundMonster soundMonster;

    [Header("Valve 2 references")]
    [SerializeField] Transform teleportPosition;
    [SerializeField] Transform runPosition;

    [Header("case 4 references")]
    [SerializeField] GameObject event4;

    [Header("Case 6 reference")]
    [SerializeField] GameObject lantern;

    public void EventTree(int eventID, Transform vfxTransform)
    {
        //Checks if the event is a respawn point(all respawn points have a negative event ID
        if (eventID < 0)
        {
            if (respawnManager == null) Debug.Log("Poop");
            respawnManager.SetSpawnPoint(vfxTransform.position);
            return;
        }

        switch (eventID)
        {
            case 0:
                Debug.Log("You are stinky, go shower!");
                break;

            case 1:
                lightningSpawner.transform.position = vfxTransform.position;
                lightningSpawner.transform.rotation = vfxTransform.rotation;

                lightningSpawner.Play();
                audioPitcherSO.Play(audioSource);
                break;
            case 2:
                event4.gameObject.SetActive(true);
                break;

            case 3:
                for (int i = 0; i < lightIterations; i++)
                {
                    if (lightIndex >= lightList.Count) return;

                    lightList[lightIndex].intensity = lightIntensity;
                    lightIndex++;
                }
                break;
            case 4:
                soundMonster.TeleportToPosition(teleportPosition.position);
                break;
            case 5:
                soundMonster.DeAgroMonster(5);
                soundMonster.RunToPosition(runPosition.position);
                break;
            case 6:
                lantern.SetActive(false);
                break;

            default:
                throw new System.Exception("Event ID out of range");
        }
    }

    private void OnEnable() => eventChannel.CallEvent += EventTree;
    private void OnDisable() => eventChannel.CallEvent -= EventTree;
}
