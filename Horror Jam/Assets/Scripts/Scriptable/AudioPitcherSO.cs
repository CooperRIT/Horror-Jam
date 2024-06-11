using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Pitcher")]
public class AudioPitcherSO : AudioSO
{
    [Header("Sound Level Settings")]
    [Tooltip("The level of sound produced when active")]
    public float audioLevel;
    public float decayLevel;

    [Header("Pitch Settings")]
    public AudioClip[] audioClips;
    public RangedFloat volume;
    public RangedFloat pitch;

    public override void Play(AudioSource source)
    {
        if (audioClips.Length == 0 || source == null)
            return;

        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];

        source.volume = Random.Range(volume.minValue, volume.maxValue);

        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);

        source.PlayOneShot(clip);
    }
}