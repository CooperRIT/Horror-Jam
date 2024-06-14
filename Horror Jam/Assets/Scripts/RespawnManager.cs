using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] ResetEventChannel resetEventChannel;
    [SerializeField] SoundEventChannel soundEventChannel;

    Vector3 currentSpawnPoint;
    [SerializeField] List<EnemyBase> enemyList;
    Transform player;

    [Header("Fade Controller")]
    [SerializeField] Image fadePanel;
    [Tooltip("How much transparency u want to take away for add during fades")]
    [Range(0, 1)]
    [SerializeField] float fadeAmount = 0;
    [SerializeField] AnimationCurve fadeInCurve;
    [SerializeField] AnimationCurve fadeOutCurve;
    [SerializeField] InputEventChannel inputEventChannel;

    private void Awake()
    {
        enemyList = new List<EnemyBase>();

        player = GameObject.Find("Player").transform;

        Transform enemies = GameObject.Find("Enemies").transform;

        if (enemies == null)
        {
            Debug.Log("No Cultists Found");
        }

        //This is the location of the AI enemy script on each enemy
        /*
         * Parent
         *  EnemyBase
         *   Scripts Object <-- Getting this and taking the AI script from it
        */
        for (int i = 0; i < enemies.childCount; i++)
        {
            enemyList.Add(enemies.GetChild(i).GetChild(0).GetChild(0).GetComponent<EnemyBase>());
        }
        if (fadePanel == null)
        {
            return;
        }
        StartCoroutine(nameof(FadeIn));
    }

    private void Start()
    {

    }

    public void SetSpawnPoint(Vector3 spawnPointPosition)
    {
        currentSpawnPoint = spawnPointPosition;
    }

    public Vector3 CurrentSpawnPoint
    {
        get { return currentSpawnPoint; }
    }

    [ContextMenu("ResetScene")]
    public void RestartScene()
    {
        //Experimental For Cutscense

        foreach(EnemyBase enemies in enemyList)
        {
            enemies.ResetEnemy();
        }
        Debug.Log("UnparentedPlayer");
        soundEventChannel.CurrentSoundLevel = 0;
        player.parent = null;
        player.transform.position = currentSpawnPoint;
        Debug.Log("restarted cultists and player");
        resetEventChannel.EventTrigger();
        inputEventChannel.TriggerEvent(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        //makes a new color to copy these changes onto and sets the starting oppacity of the fade pannel to 1
        Color slate = fadePanel.color;
        slate.a = 1;
        fadePanel.color = slate;
        float lerpAmmount = 0;
        while (fadePanel.color.a != .0f)
        {
            slate.a = fadeInCurve.Evaluate(lerpAmmount);
            fadePanel.color = slate;
            lerpAmmount += fadeAmount * Time.deltaTime;
            yield return null;
        }
        Debug.Log("Finished Fading In");
    }

    IEnumerator FadeOut()
    {
        Color slate = fadePanel.color;
        slate.a = 0;
        fadePanel.color = slate;
        float lerpAmmount = 0;
        while (fadePanel.color.a != 1)
        {
            slate.a = fadeOutCurve.Evaluate(lerpAmmount);
            fadePanel.color = slate;
            lerpAmmount += fadeAmount * Time.deltaTime;
            yield return null;
        }
        Debug.Log("Finished Fading Out");
    }

    void CutToBlack()
    {
        Color slate = fadePanel.color;
        slate.a = 1;
        fadePanel.color = slate;
    }
}
