using UnityEngine;
using UnityEngine.VFX;

public class EventManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] private EventChannel eventChannel;

    [Header("Events Refernces")]
    [SerializeField] private VisualEffect lightningSpawner;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lightningClip;

    [Header("Respawn Manager")]
    [SerializeField] private RespawnManager respawnManager;

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
                audioSource.PlayOneShot(lightningClip);
                break;

            default:
                throw new System.Exception("Event ID out of range");
        }
    }

    private void OnEnable() => eventChannel.CallEvent += EventTree;
    private void OnDisable() => eventChannel.CallEvent -= EventTree;
}
