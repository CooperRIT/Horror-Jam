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
    [SerializeField] Transform[] locationsToPatrol;
    int currentPatrol;

    [Header("SpottingVariables")]
    float spottingTime;
    // Start is called before the first frame update
    void Start()
    {
        
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
        cultist.SetDestination(locationsToPatrol[currentPatrol].position);
    }

    void Spotting()
    {

    }

    void Pursuing()
    {

    }
}
