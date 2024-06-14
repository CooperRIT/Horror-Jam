using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshWall : MonoBehaviour
{
    [SerializeField] private float duration = 5f;

    [SerializeField] private Transform startPosition;

    [SerializeField] private Transform endPosition;

    public void CollapseWall() => StartCoroutine(WallSink());

    public void ResetWall() => transform.position = startPosition.position;
    IEnumerator WallSink()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, elapsedTime / duration);

            yield return null;
        }
    }
}
