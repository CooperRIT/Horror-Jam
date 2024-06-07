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
}
