using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    [SerializeField] ScreenShakeEventChannel screenShake;

    [SerializeField] private AnimationCurve shakeCurve;

    [ContextMenu("Shake")]
    public void Shake()
    {
        StartShaking(1, 1);
    }

    public void StartShaking(float duration, float intensity) => StartCoroutine(Shake(duration, intensity));

    IEnumerator Shake(float duration, float intensity)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;

            transform.position = startPosition + (Random.insideUnitSphere * (intensity * shakeCurve.Evaluate(elapsedTime)));
            
            yield return null;
        }

        transform.position = startPosition;
    }

    private void OnEnable() => screenShake.ScreenShake += StartShaking;
    private void OnDisable() => screenShake.ScreenShake -= StartShaking;
}
