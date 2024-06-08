using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLightMoving : MonoBehaviour
{
    [SerializeField] Transform shipLight;
    [SerializeField] float rotationAmmount;
    float desiredRoation;
    float initalY;
    bool reverse;
    float timer = 3;
    float rotationTime;
    WaitForSeconds waitTime;
    // Start is called before the first frame update
    void Start()
    {
        shipLight = transform.parent;
        initalY = shipLight.rotation.y;
        waitTime = new WaitForSeconds(timer);
    }

    IEnumerator MoveLight()
    {
        while (true)
        {
            if (shipLight.rotation.y == desiredRoation)
            {
                yield return 
            }
            yield return null;
        }
    }
}
