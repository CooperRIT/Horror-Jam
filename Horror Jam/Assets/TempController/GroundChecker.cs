using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Tooltip("Make sure to have this pointing up")]
    [SerializeField] Transform circleCastPosition;
    [SerializeField] LayerMask ground;
    [SerializeField] float circleCastRadius = 1;
    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(circleCastPosition.position, circleCastRadius, ground))
        {
            Debug.Log("Touching the ground");
        }

        //if(Physics.OverlapSphereNonAlloc(circleCastPosition.position, circleCastRadius, ground))
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(circleCastPosition.position, circleCastRadius);
    }

    bool Grounded
    {
        get { return grounded; }
    }
}
