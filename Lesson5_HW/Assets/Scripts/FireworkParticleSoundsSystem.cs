using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireworkParticleSoundsSystem : MonoBehaviour
{
    [SerializeField]
    private SFXType[] _launchSoundTypes;
    [SerializeField]
    private SFXType[] _explosionSoundTypes;

    private ParticleSystem _particleSystem;

    private int _currentNumberOfParticles = 0;


    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        if (_particleSystem == null) 
        {
            Debug.LogError("Particle system is missing");
        }
    }

    void Update()
    {
        if (_particleSystem.particleCount < _currentNumberOfParticles)
        {
            AudioManager.PlaySFX(_explosionSoundTypes[Random.Range(0, _explosionSoundTypes.Length)]);
        }

        if (_particleSystem.particleCount > _currentNumberOfParticles)
        {
            AudioManager.PlaySFX(_launchSoundTypes[Random.Range(0, _launchSoundTypes.Length)]);
        }

        _currentNumberOfParticles = _particleSystem.particleCount;
    }
}
