using UnityEngine;

public class ShootingWeapon : Weapon
{
    [SerializeField]
    private float _fireRate = 15f;

    [SerializeField]
    private ParticleSystem _muzzleFlash;
    
    [SerializeField]
    protected float _range = 100f;

    private float _nextTimeToFire = 0f;

    private RaycastHit _hit;

    void Awake()
    {
        _currentAmmo = 0;
    }

    public override void Shoot(GameObject _raycastSource, string shooterName)
    {
        _currentAmmo--;

        _nextTimeToFire = Time.time + 1f / _fireRate;

        _muzzleFlash.Play();
        
        if (Physics.Raycast(_raycastSource.transform.position, _raycastSource.transform.forward, out _hit, _range))
        {
            var target = _hit.transform.GetComponent<IShootable>();
            if (target != null)
            {
                var shootData = new AttackData(shooterName, _damage, _weaponName, _weaponIcon);
                target.TakeDamage(shootData);
            }

            if (_hit.rigidbody != null)
            {
                _hit.rigidbody.AddForce(-_hit.normal * _impactForce);
            }

            var impactGO  = Instantiate(_impactEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
            Destroy(impactGO, _destroyImpactTimeout);
        }
    }

    public override bool IsWeaponReady()
    {
        return (Time.time >= _nextTimeToFire) && !IsOutOfAmmo();
    }
}
