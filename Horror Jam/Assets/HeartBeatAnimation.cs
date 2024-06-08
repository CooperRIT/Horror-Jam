using UnityEngine;

public class HeartBeatAnimation : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private int blendShapeIndex = 0;

    [SerializeField] private float beatSpeed = 1.0f;
    [SerializeField] private float maxBlendShapeValue = 100.0f;

    private float animationTime = 0.0f;

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        animationTime += Time.deltaTime * beatSpeed;
        float blendShapeValue = (Mathf.Sin(animationTime * Mathf.PI * 2) * 0.5f + 0.5f) * maxBlendShapeValue;

        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeValue);
    }
}
