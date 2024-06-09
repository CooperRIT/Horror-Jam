using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    Transform cutSceneAnimatorTransform;
    [SerializeField] Animator cutSceneAnimator;
    [SerializeField] Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        cutSceneAnimatorTransform = GameObject.Find("CutScenes").transform;
        cutSceneAnimator = cutSceneAnimatorTransform.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void OnStartCutScene(int cutSceneIndex)
    {
        cutSceneAnimatorTransform.GetChild(cutSceneIndex).gameObject.SetActive(true);
        player.parent = cutSceneAnimatorTransform.GetChild(cutSceneIndex).GetChild(1);
        player.localPosition = Vector3.zero;
        string paramName = cutSceneAnimator.GetParameter(cutSceneIndex).name;
        cutSceneAnimator.SetTrigger(paramName);
    }
}
