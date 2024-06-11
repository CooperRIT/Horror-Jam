using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    Transform cutSceneAnimatorTransform;
    Animator cutSceneAnimator;
    Transform player;
    bool hasCutscenes;
    [SerializeField] InputEventChannel inputEventChannel;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        cutSceneAnimatorTransform = GameObject.Find("CutScenes").transform;
        hasCutscenes = cutSceneAnimatorTransform.childCount != 0;
        cutSceneAnimator = cutSceneAnimatorTransform.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void OnStartCutScene(int cutSceneIndex)
    {
        if(!hasCutscenes)
        {
            return;
        }
        cutSceneAnimatorTransform.GetChild(cutSceneIndex).gameObject.SetActive(true);
        inputEventChannel.TriggerEvent(false);
        player.parent = cutSceneAnimatorTransform.GetChild(cutSceneIndex).GetChild(1);
        player.localPosition = Vector3.zero;
        string paramName = cutSceneAnimator.GetParameter(cutSceneIndex).name;
        cutSceneAnimator.SetTrigger(paramName);
    }
    

    //HOW TO MAKE A CUTSCENE
    //1: Create a new empty game object under the CutScenes GameObject(Order does matter as they are indexed properly)
    //2: Add whatever creature or whatever is interacting with the player as the first child, then a transform, call it something like player holder, as the second child(This will parent the player to it when an animation is called)
    //3: Create your animation, make an anim clip, name is correctly, and do your animation
        //3a: Create an animation event in your anim, link it to the AnimEventMethod and then fill the index param with the index of the cutscene
    //4: Create a trigger in the anim tree with the same name as your anim, put this trigger just before the "SetStateToNull" trigger as that is used to reset the animator
    //5: Make a transition with that trigger as the condition from the empty state and create another transition from your anim back to the empty state with the trigger "SetStateToNull" as your condition

    //If you have any problems please dm me
}
