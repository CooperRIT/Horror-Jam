using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour
{
    Vector3 currentSpawnPoint;
    [SerializeField] List<CultistAi> cultists;
    [SerializeField] Transform player;
    [SerializeField] CutsceneManager cutsceneManager;

    [Header("Fade Controller")]
    [SerializeField] Image fadePanel;
    [Tooltip("How much transparency u want to take away for add during fades")]
    [Range(0, 1)]
    [SerializeField] float fadeAmount = 0;
    [SerializeField] AnimationCurve fadeInCurve;
    [SerializeField] AnimationCurve fadeOutCurve;

    private void Awake()
    {
        cultists = new List<CultistAi>();
        Transform enemies = GameObject.Find("Enemies").transform;

        if (enemies == null)
        {
            throw new System.Exception("Put all of your enemies under a gameobject called 'Enemies'");
        }

        //This is the location of the cultist AI script on each cultist
        for(int i = 0; i < enemies.childCount; i++)
        {
            cultists.Add(enemies.GetChild(i).GetChild(1).GetChild(0).GetComponent<CultistAi>());
        }
        if (fadePanel == null)
        {
            return;
        }

        StartCoroutine(nameof(FadeIn));
        player = GameObject.Find("Player").transform;
        cutsceneManager.GetComponent<RespawnManager>();
        //StartCoroutine(nameof(FadeOut));
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

    public void RestartScene()
    {
        //Experimental For Cutscense
        player.parent = null;

        foreach(CultistAi cultistAi in cultists)
        {
            cultistAi.RestartCultist();
        }
        player.transform.position = currentSpawnPoint;
        Debug.Log("restarted cultists and player");
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
