using UnityEngine;

public class DisturbingMouthAnimation : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private int[] mouthBlendShapeIndices;

    [SerializeField] private float minAnimationSpeed = 0.5f;
    [SerializeField] private float maxAnimationSpeed = 2.0f;
    [SerializeField] private float maxBlendShapeValue = 100.0f;
    [SerializeField] private float randomnessFactor = 0.1f;

    private float[] animationTimes;

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        animationTimes = new float[mouthBlendShapeIndices.Length];
        for (int i = 0; i < animationTimes.Length; i++)
        {
            animationTimes[i] = Random.Range(0f, Mathf.PI * 2f);
        }
    }

    void Update()
    {
        for (int i = 0; i < mouthBlendShapeIndices.Length; i++)
        {
            float animationSpeed = Random.Range(minAnimationSpeed, maxAnimationSpeed);
            animationTimes[i] += Time.deltaTime * animationSpeed;

            float blendShapeValue = (Mathf.Sin(animationTimes[i] * Mathf.PI * 2) * 0.5f + 0.5f) * maxBlendShapeValue;
            blendShapeValue += Random.Range(-randomnessFactor, randomnessFactor) * maxBlendShapeValue;
            blendShapeValue = Mathf.Clamp(blendShapeValue, 0f, maxBlendShapeValue);

            skinnedMeshRenderer.SetBlendShapeWeight(mouthBlendShapeIndices[i], blendShapeValue);
        }
    }
}
