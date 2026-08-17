using UnityEngine;

[CreateAssetMenu(
    fileName = "FeedbackProfile",
    menuName = "Mobile Game/Feedback Profile")]
public sealed class FeedbackProfile : ScriptableObject
{
    public ParticleSystem ParticlePrefab => particlePrefab;
    public float Particlelifetime => particleLifetime;
    public AudioClip Sound => sound;
    public float Volume
        => volume;
    public bool UseVibration => useVibration;
    
    [Header("Visual feedback")] [SerializeField]
    private ParticleSystem particlePrefab;

    [SerializeField, Min(0f)] private float particleLifetime = 2f;

    [Header("Audio feedback")] [SerializeField]
    private AudioClip sound;

    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    [Header("Haptic feedback")] [SerializeField]
    private bool useVibration = true;
}