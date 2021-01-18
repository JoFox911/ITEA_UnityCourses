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
    private int _currentAmmo;
    private RaycastHit _hit;

    void Awake()
    {
        _currentAmmo = 0;
        _isOutOfAmmo = true;
    }

    void OnDisable()
    {
        _isOutOfAmmo = _currentAmmo == 0;
    }

    public override void Shoot(GameObject _raycastSource, string shooterName)
    {
        _currentAmmo--;
        if (_currentAmmo <= 0)
        {
            _isOutOfAmmo = true;
        }

        _nextTimeToFire = Time.time + 1f / _fireRate;

        _muzzleFlash.Play();
        
        if (Physics.Raycast(_raycastSource.transform.position, _raycastSource.transform.forward, out _hit, _range))
        {
            //Debug.Log("SHOOT" + _hit.transform.tag);

            var target = _hit.transform.GetComponent<IShootable>();
            if (target != null)
            {
                var shootData = new AttackData(_damage, shooterName, _weaponName, _weaponIcon);
                target.TakeDamage(shootData);
            }

            if (_hit.rigidbody != null)
            {
                _hit.rigidbody.AddForce(-_hit.normal * _impactForce);
            }

            GameObject impactGO  = Instantiate(_impactEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    public override void Reload(int enabledAmmoNumber, out int remaining)
    {
        var possibleAmmoForWeaponClip = enabledAmmoNumber >= _ammoVolume - _currentAmmo
                                                ? _ammoVolume - _currentAmmo
                                                : enabledAmmoNumber;

        remaining = enabledAmmoNumber - possibleAmmoForWeaponClip;
        _currentAmmo += possibleAmmoForWeaponClip;
        _isOutOfAmmo = false;
    }

    public override int GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    public override bool IsWeaponReady()
    {
        return (Time.time >= _nextTimeToFire) && !_isOutOfAmmo;
    }
}
