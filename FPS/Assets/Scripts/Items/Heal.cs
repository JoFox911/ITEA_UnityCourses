using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private float _healPOints = 25;

    public float GetHealpoints()
    {
        return _healPOints;
    }
}
