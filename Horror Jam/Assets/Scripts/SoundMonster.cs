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
    [SerializeField] float minSound = 10;
    [SerializeField] float maxSound = 100;
    [SerializeField] float soundToDistanceRatio;
    float currentSoundThreshold;
    Transform parentTransform;
    Transform player;
    [Header("AnimationCurves")]
    [SerializeField] AnimationCurve soundToDistanceCurve;

    


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        parentTransform = transform.parent;
        //SetKeys();
    }

    void SetKeys()
    {
        soundToDistanceCurve.keys[0].value = minSound;
        soundToDistanceCurve.keys[1].value = maxSound;
    }

    // Update is called once per frame
    void Update()
    {
        SoundInterest(20);
    }

    public void SoundInterest(float playerSound)
    {
        CalculateSoundThresholds();

        if (playerSound > soundToDistanceRatio)
        {
            //Go to the player

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
        throw new System.NotImplementedException();
    }
}
