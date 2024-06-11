using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BelowDeck.MiniUtil;

public class SoundMonster : EnemyBase
{

    [Header("Sound Values")]
    [SerializeField] float currentDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float minDistance;
    [Tooltip("How much sound is needed to alert the enemy")]
    [SerializeField] float soundToDistanceRatio;
    [SerializeField] SoundEventChannel soundEventChannel;
    [SerializeField] float currentSoundLevel => soundEventChannel.CurrentSoundLevel;
    Transform parentTransform;
    Transform player;
    [Header("AnimationCurves")]
    [Tooltip("IF U CHANGE THE VALUES ON THE MAX, GO AND CHANGE THE MAX IN SOUND EVENT CHANNELS")]
    [SerializeField] AnimationCurve soundToDistanceCurve;

    Vector3 startPosition;

    


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        parentTransform = transform.parent;
        startPosition = parentTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentSoundLevel = 
        SoundInterest(currentSoundLevel);
    }

    public void SoundInterest(float playerSound)
    {
        CalculateSoundThresholds();

        if (playerSound > soundToDistanceRatio)
        {
            //Go to the player
            //Debug.Log("I can hear you");
            parentTransform.LookAt(player.position);
        }
    }

    void CalculateSoundThresholds()
    {
        currentDistance = DistanceToPlayer();

        // Clamp the distance to be within the range
        float clampedDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Normalize the clamped distance(Puts it to between 0 and 1)
        float normalizedValue = (clampedDistance - minDistance) / (maxDistance - minDistance);

        //Uses this evaluated disatance to return the ammount of sound needed from that distance
        soundToDistanceRatio = soundToDistanceCurve.Evaluate(normalizedValue);

    }

    float DistanceToPlayer()
    {
        return MiniUtil.DistanceNoY(parentTransform.position, player.position);
    }


    public override void ResetEnemy()
    {
        parentTransform.position = startPosition;
    }
}
