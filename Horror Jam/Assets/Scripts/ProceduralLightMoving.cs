using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLightMoving : MonoBehaviour
{
    [SerializeField] float rotationAmmount = 10;
    [SerializeField] float desiredRoationAmmount = 70;
    [SerializeField] float timer = 3;
    Transform shipLight;
    float initalY;
    bool reverse;
    WaitForSeconds waitTime;
    // Start is called before the first frame update
    void Start()
    {
        shipLight = transform.parent;
        initalY = shipLight.rotation.y;
        waitTime = new WaitForSeconds(timer);
        StartCoroutine(nameof(MoveLight));
    }

    IEnumerator MoveLight()
    {
        yield return waitTime;
        float additiveRoation = 0;
        while (true)
        {
            if (Mathf.Abs(additiveRoation) >= desiredRoationAmmount)
            {
                additiveRoation = 0;
                rotationAmmount *= -1;
                yield return waitTime;
            }
            shipLight.Rotate(0, rotationAmmount * Time.deltaTime, 0);
            additiveRoation += rotationAmmount * Time.deltaTime;

            yield return null;
        }
    }
}
