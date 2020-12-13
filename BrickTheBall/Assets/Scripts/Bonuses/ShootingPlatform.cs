using UnityEngine;

public class ShootingPlatform : Bonus
{
    [SerializeField]
    private float _shootingDuration = 20f;

    [SerializeField]
    private float _fireCooldown = 0.4f;

    protected override void ApplyEffect()
    {
        GameEvents.ShootingPlatformCatchEvent(_shootingDuration, _fireCooldown);
    }
}
