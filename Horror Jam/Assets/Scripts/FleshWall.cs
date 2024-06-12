using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshWall : MonoBehaviour
{
    [SerializeField] private float duration = 5f;

    [SerializeField] private Transform endPosition;

    public void CollapseWall() => StartCoroutine(WallSink());
    IEnumerator WallSink()
    {
        Vector3 startingPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(startingPosition, endPosition.position, elapsedTime / duration);

            yield return null;
        }
    }
}
