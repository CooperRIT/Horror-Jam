using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum CultistStates
{
    Patroling = 0,
    Spotting = 1,
    Pursuing = 2,
    Stopped = 3
}

public class CultistAi : EnemyBase
{
    [Header("Scriptable Object Reference")]
    [SerializeField] AudioPitcherSO seenAudioPitcher;
    [SerializeField] AudioPitcherSO chaseAudioPitcher;

    CultistStates currentState;
    NavMeshAgent cultist;

    [Header("Patroling Variables")]
    [SerializeField] List<Vector3> locationsToPatrol = new List<Vector3>();
    [Tooltip("Circling patrol makes the cultist go back to the first point after going through all the points. Having this off makes the cultists retrace his steps when he arrives at the final point")]
    [SerializeField] bool circlingPatrol = false;
    Transform transformList;
    float minPatrolDistance = .5f;
    bool reverse;
    int currentPatrol = 0;

    [Header("Spotting Variables")]
    [SerializeField] float timer;
    [SerializeField] float spottingTime = 3;

    [Header("Pursuing Variables")]
    [Tooltip("Max speed when the cultist has noticed the player")]
    [SerializeField] float maxSpeed = 15;
    Transform playerTransform;
    Light headLight;
    float lightPerSecond = 30;
    Vector3 playerPreviousPosition;
    Vector3 predictionAdditive;

    [Header("VisionCone")]
    [SerializeField] float visionConeRange = 15;
    [SerializeField] float visionConeFOV = 120;
    //Layer mask for rays to hit against in order to stop on walls
    [SerializeField] LayerMask obstructionLayer;
    //How many Rays Are Fired Out the cone
    int visionConeResolution = 30;
    Vector3 raycastDirection;
    bool foundPlayer;

    [Header("Exposure")]
    [Tooltip("The max allowed exposure before cultist pursues player")]
    [SerializeField] float maxExposure = 1f;
    [Tooltip("How much exposure is applied per frame")]
    [SerializeField] float exposurePerFrame = .1f;
    [Tooltip("How much exposure is taken away per frame when the player is not visible to the cultist")]
    [SerializeField] float exposureDecayRate = .1f;
    [Tooltip("The current exposure of the player")]
    [SerializeField] float currentExposure;
    bool exposedThisFrame;
    int stateBeforeStopping = -1;

    [Header("Animation")]
    Transform cultistTransform;
    // How high the object will move
    float amplitude = .17f;
    // How fast the object will move
    float frequency = 3f;

    private Vector3 startPosition;

    [Header("ResetVariables")]
    float startingSpeed;
    float startingIntensity;

    private AudioSource source;


    // Start is called before the first frame update
    void Awake()
    {
        cultist = GetComponentInParent<NavMeshAgent>();
        startingSpeed = cultist.speed;
        cultistTransform = cultist.transform;
        transformList = transform.parent.parent.GetChild(1);
        headLight = transform.GetChild(0).GetComponent<Light>();
        source = GetComponentInParent<AudioSource>();
        startingIntensity = headLight.intensity;

        //Makes sure u have transform in the transform list
        if (Application.isEditor && transformList.childCount == 0)
        {
            throw new System.Exception("you are a goober, populate the transform list");
        }

        locationsToPatrol.Add(transform.parent.parent.position);
        for (int i = 0; i < transformList.childCount; i++)
        {
            //Makes the y normalized through out all cultist
            locationsToPatrol.Add(new Vector3(transformList.GetChild(i).position.x, cultistTransform.position.y, transformList.GetChild(i).position.z));
        }

        //When the cult member no longer needs their transform list, it destroys it
        Destroy(transformList.gameObject);
        currentState = CultistStates.Patroling;

        //VisionCone Initialization code

        //Converts the angle inputed from degrees to radians
        visionConeFOV *= Mathf.Deg2Rad;
        StartCoroutine(nameof(ConeCasting));
        startPosition = cultistTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case CultistStates.Patroling:
                Patroling();
                break;
            case CultistStates.Spotting:
                Spotting();
                break;
            case CultistStates.Pursuing:
                Pursuing();
                break;
            default:
                break;
        }

        AnimateCultist();

        if (foundPlayer)
        {
            return;
        }
        ConeCasting();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, raycastDirection * visionConeRange, Color.red);
        Debug.DrawRay(transform.position, transform.forward * visionConeRange, Color.red);
    }

    void Patroling()
    {
        cultist.SetDestination(locationsToPatrol[currentPatrol]);

        //Switch to Spotting once cultist arrives at destination
        if (Vector3.Distance(locationsToPatrol[currentPatrol], cultist.transform.position) <= minPatrolDistance)
        {
            currentState = CultistStates.Spotting;
        }
    }

    void Spotting()
    {
        if (timer <= spottingTime)
        {
            timer += 1 * Time.deltaTime;
            return;
        }

        //Switching back to patroling after finding nothing

        if (currentPatrol == locationsToPatrol.Count - 1)
        {
            if(circlingPatrol)
            {
                //will put the current patrol back to -1 which will be added to for a current patrol of 0 come the patrolling state
                currentPatrol = -1;
            }
            else
            {
                reverse = true;
            }
        }
        else if(currentPatrol == 0)
        {
            reverse = false;
        }

        currentPatrol += reverse ? -1 : 1;

        currentState = CultistStates.Patroling;
        timer = 0f;
    }

    void Pursuing()
    {
        //PredictMovement();
        cultist.SetDestination(playerTransform.position);
        //playerPreviousPosition = playerTransform.position;
    }

    /*oid PredictMovement()
    {
        Vector3 playerVelocity = playerTransform.position - playerPreviousPosition;
        predictionAdditive = playerTransform.position + playerVelocity * 100;
    }*/

    void PredictRotation()
    {
        

        //var rotation = Quaternion.LookRotation(heading);
        //
    }

    void ConeCasting()
    {
        float currentAngle = -visionConeFOV / 2;
        float angleIncrement = visionConeFOV / (visionConeResolution - 1);
        float sine;
        float cosine;

        for (int i = 0; i < visionConeResolution; i++)
        {
            sine = Mathf.Sin(currentAngle);
            cosine = Mathf.Cos(currentAngle);
            raycastDirection = (transform.forward * cosine) + (transform.right * sine);
            if(Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, visionConeRange, obstructionLayer))
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    if (stateBeforeStopping == -1)
                    {
                        Debug.Log("Seen");
                        seenAudioPitcher.Play(source);
                        stateBeforeStopping = (int)currentState;
                    }
                    currentState = CultistStates.Stopped;
                    cultist.SetDestination(transform.position);
                    cultistTransform.LookAt(hit.point);
                    if (!exposedThisFrame)
                    {
                        currentExposure += exposurePerFrame * Time.deltaTime;
                        exposedThisFrame = true;
                    }

                    if (currentExposure >= maxExposure)
                    {
                        playerTransform = hit.transform;
                        StartCoroutine(nameof(SeenPlayer));
                    }
                }
            }
            currentAngle += angleIncrement;
        }
        if(playerTransform != null)
        {
            return;
        }
        if(!exposedThisFrame && currentState == CultistStates.Stopped)
        {
            currentExposure -= exposureDecayRate * Time.deltaTime;

            if (currentExposure <= 0)
            {
                currentState = stateBeforeStopping == -1 ? currentState : (CultistStates)stateBeforeStopping;
                currentExposure = 0;
                stateBeforeStopping = -1;
            }
        }
        exposedThisFrame = false;
    }

    IEnumerator SeenPlayer()
    {
        foundPlayer = true;
        cultist.speed = maxSpeed;
        cultist.angularSpeed = 500;
        cultist.acceleration = 30;
        cultist.SetDestination(transform.position);
        while(true)
        {
            if (headLight.intensity < 20)
            {
                headLight.intensity += lightPerSecond * Time.deltaTime;
                yield return null;
            }
            else
            {
                break;
            }

        }
        chaseAudioPitcher.Play(source);
        currentState = CultistStates.Pursuing;
    }

    /// <summary>
    /// Adds slight up and down hover with sin waves
    /// </summary>
    void AnimateCultist()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        cultist.transform.position = new Vector3(cultistTransform.position.x, newY, cultistTransform.position.z);
    }

    public override void ResetEnemy()
    {
        foundPlayer = false;
        cultistTransform.position = startPosition;
        currentState = CultistStates.Patroling;
        headLight.intensity = .6f;
        timer = 0f;
        cultist.SetDestination(transform.position);
        cultist.speed = startingSpeed;
    }
}
