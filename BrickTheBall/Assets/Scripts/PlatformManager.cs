using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private Vector3 initialPossition;

    void Awake()
    {
        initialPossition = transform.position;

        GameEvents.OnResetGameState += ResetState;
    }

    void OnDestroy()
    {
        GameEvents.OnResetGameState -= ResetState;
    }

    public void ResetState()
    {
        transform.position = initialPossition;
    }
}
