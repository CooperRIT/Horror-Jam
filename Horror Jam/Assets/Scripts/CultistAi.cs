using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum CultistStates
{
    Patroling = 0,
    Spotting = 1,
    Pursuing = 2
}

public class CultistAi : MonoBehaviour
{
    CultistStates currentState;
    NavMeshAgent cultist;

    [Header("Patroling Variables")]
    [SerializeField] List<Vector3> locationsToPatrol = new List<Vector3>();
    Transform transformList;
    float minPatrolDistance = .5f;
    bool reverse;
    int currentPatrol = 0;

    [Header("SpottingVariables")]
    [SerializeField] float timer;
    float spottingTime = 3;

    [Header("VisionCone")]
    //Material For Rendering The Vision Cone
    [SerializeField] Material visionConeMaterial;
    [SerializeField] float visionConeRange;
    [SerializeField] float visionConeAngle;

    //Layer mask for rays to hit against in order to stop on walls
    [SerializeField] LayerMask obstructionLayer;
    //How many Triangles Comprise the cone
    int visionConeResolution = 120;
    Mesh visionConeMesh;
    MeshFilter meshFilter;

    Vector3 raycastDirection;


    // Start is called before the first frame update
    void Awake()
    {
        cultist = GetComponentInParent<NavMeshAgent>();
        transformList = transform.GetChild(0);

        //Makes sure u have transform in the transform list
        if(Application.isEditor && transformList.childCount == 0)
        {
            throw new System.Exception("you are a goober, populate the transform list");
        }

        locationsToPatrol.Add(transform.parent.position);
        for (int i = 0; i < transformList.childCount; i++)
        {
            locationsToPatrol.Add(transformList.GetChild(i).position);
        }

        //When the cult member no longer needs their transform list, it destroys it
        Destroy(transformList.gameObject);
        currentState = CultistStates.Patroling;

        //VisionCone Initialization code

        //Converts the angle inputed from degrees to radians
        visionConeAngle *= Mathf.Deg2Rad;
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
                break;
        }

        ConeCasting();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, raycastDirection * visionConeRange, Color.red);
    }

    void Patroling()
    {
        cultist.SetDestination(locationsToPatrol[currentPatrol]);

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
            reverse = true;
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

    }

    void ConeCasting()
    {
        float currentAngle = -visionConeAngle / 2;
        float angleIncrement = visionConeAngle / (visionConeResolution - 1);
        float sine;
        float cosine;

        for (int i = 0; i < visionConeResolution; i++)
        {
            sine = Mathf.Sin(currentAngle);
            cosine = Mathf.Cos(currentAngle);
            raycastDirection = (transform.forward * cosine) + (transform.right * sine);
            if(Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, visionConeRange, obstructionLayer))
            {
                Debug.Log("ConeHitSomething");
            }

            currentAngle += angleIncrement;
        }
    }
}
