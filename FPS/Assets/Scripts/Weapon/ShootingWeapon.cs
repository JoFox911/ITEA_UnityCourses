﻿using System.Collections;
using UnityEngine;

public class ShootingWeapon : Weapon
{
    [SerializeField]
    private float _fireRate = 15f;
    [SerializeField]
    private ParticleSystem _muzzleFlash;
    [SerializeField]
    private GameObject _impactEffect;


    private float _nextTimeToFire = 0f;
    private bool isReloading;
    private int _currentAmmo;
    private RaycastHit _hit;

    void Awake()
    {
        _currentAmmo = 0;
        _isOutOfAmmo = true;
    }

    public override void Shoot(GameObject _raycastSource)
    {

        if ((Time.time <= _nextTimeToFire) || _isOutOfAmmo || isReloading)
        {
            return;
        }

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
                target.TakeDamage(_damage);
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
        var possibleAmmoForWeaponClip = 0;
        if (!isReloading) 
        {
            isReloading = true;
            possibleAmmoForWeaponClip = enabledAmmoNumber >= _ammoVolume - _currentAmmo
                                                    ? _ammoVolume - _currentAmmo
                                                    : enabledAmmoNumber;
            StartCoroutine(FinishReload());
            _currentAmmo += possibleAmmoForWeaponClip;
        }
        remaining = enabledAmmoNumber - possibleAmmoForWeaponClip;
    }

    public IEnumerator FinishReload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _isOutOfAmmo = false;
        isReloading = false;
        
    }

    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }
}
