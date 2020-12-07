using UnityEngine;

public class BonfireParticleSoundsSystem : MonoBehaviour
{
    [SerializeField]
    private MusicType _bonfireSound;

    void OnEnable()
    {
        AudioManager.PlayMusic(_bonfireSound);
    }

    private void OnDisable()
    {
        AudioManager.StopMusic();
    }
}
