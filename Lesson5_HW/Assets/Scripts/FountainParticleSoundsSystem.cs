using UnityEngine;

public class FountainParticleSoundsSystem : MonoBehaviour
{
    [SerializeField]
    private MusicType _fountainSound;

    void OnEnable()
    {
        AudioManager.PlayMusic(_fountainSound);
    }

    private void OnDisable()
    {
        AudioManager.StopMusic();
    }
}
