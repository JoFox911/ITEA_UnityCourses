using UnityEngine;

public class ShootingPlatform : Bonus
{
    [SerializeField]
    private float _shootingDuration = 10f;

    [SerializeField]
    private float _fireCooldown = 0.5f;

    protected override void ApplyEffect()
    {
        GameEvents.ShootingPlatformCatchEvent(_shootingDuration, _fireCooldown);
    }
}
