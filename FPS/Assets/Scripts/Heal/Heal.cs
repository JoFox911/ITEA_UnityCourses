using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private float _healPoints = 25;

    public float GetHealpoints()
    {
        return _healPoints;
    }
}
