using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;

public class HeartAudio : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] HeartEventChannel heartEventChannel;
    [SerializeField] ScreenShakeEventChannel screenShakeEventChannel;
    [SerializeField] List<AudioClip> heartBeatSFX;

    [Header("Cutscene Settings")]
    [SerializeField] float explodeDelay;
    [SerializeField] RespawnManager respawnManager;

    private AudioSource audioSource;

    ParticleSystem heartBurst;
    ParticleSystem heartExplode;

    HeartBeatAnimation heartAnimation;

    bool canHeartExplode = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        heartAnimation = GetComponent<HeartBeatAnimation>();

        heartBurst = transform.GetChild(1).GetComponent<ParticleSystem>();
        heartExplode = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    void HeartState(int state)
    {
        switch (state)
        {
            default:
                audioSource.clip = null;
                audioSource.Play();
                break;
            case 1:
                audioSource.clip = heartBeatSFX[0];
                audioSource.Play();
                break;
            case 2:
                audioSource.clip = heartBeatSFX[1];
                audioSource.Play();
                break;
            case 3:
                audioSource.clip = heartBeatSFX[2];
                audioSource.Play();
                break;
            case 4:

                if (!canHeartExplode) return;

                canHeartExplode = false;
                audioSource.clip = heartBeatSFX[3];
                audioSource.loop = false;
                StartCoroutine(HeartExplode());
                break;
        }
    }

    IEnumerator HeartExplode()
    {
        audioSource.Play();
        screenShakeEventChannel.TriggerEvent(2.7f, 2);
        yield return new WaitForSeconds(explodeDelay);
        heartAnimation.enabled = false;
        heartBurst.Play();
        heartExplode.Play();
        yield return new WaitForSeconds(4f);
        respawnManager.startFadeOut();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);

        //Fade out and thank you for playing message or thank you for your sacrifice
        //Load title screen
    }

    private void OnEnable() => heartEventChannel.Stage += HeartState;
    private void OnDisable() => heartEventChannel.Stage -= HeartState;
}
