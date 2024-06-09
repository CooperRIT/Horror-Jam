using UnityEngine;

public class HeartBeatAnimation : MonoBehaviour
{

    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField] private int blendShapeIndex = 0;

    [SerializeField] private float beatInterval = 1.0f;  
    [SerializeField] private float beatDuration = 0.2f; 
    [SerializeField] private float pauseDuration = 0.6f; 
    [SerializeField] private float maxBlendShapeValue = 100.0f;

    private float animationTime = 0.0f;

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        animationTime += Time.deltaTime;

        float blendShapeValue = CalculateHeartbeat(animationTime);

        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeValue);

        if (animationTime > beatInterval)
        {
            animationTime -= beatInterval;
        }
    }

    float CalculateHeartbeat(float time)
    {
        float cyclePosition = time % beatInterval;

        if (cyclePosition < beatDuration)
        {
            return Mathf.Sin(cyclePosition / beatDuration * Mathf.PI) * maxBlendShapeValue;
        }
        else if (cyclePosition < 2 * beatDuration)
        {
            return Mathf.Sin((cyclePosition - beatDuration) / beatDuration * Mathf.PI) * maxBlendShapeValue;
        }
        else
        {
            return 0.0f;
        }
    }
}
