using System;
using UnityEngine;

public class SoldierWeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _weaponHolder;

    private GunLikeWeapon _currentWeapon;


    private GunLikeWeapon _pistolSlot;
    private GunLikeWeapon _rifleSlot;

    private bool _isWeaponSelected;

    private GameObject _raycastSource;

    public void Initialize(GameObject raycastSource)
    {
        _raycastSource = raycastSource;
    }

    public void SelectPistol()
    {
        if (_pistolSlot != null) 
        {
            _isWeaponSelected = true;
            _currentWeapon = _pistolSlot;

            _currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            _currentWeapon.transform.localPosition = new Vector3(0,0,0);

            Debug.Log("_currentWeapon" + _currentWeapon.transform.eulerAngles + " " + _currentWeapon.transform.position);

            _pistolSlot.gameObject.SetActive(true);
            if (_rifleSlot != null)
            {
                _rifleSlot.gameObject.SetActive(false);
            }
        }
    }

    public void SelectRifle()
    {
        if (_rifleSlot != null)
        {
            _isWeaponSelected = true;
            _currentWeapon = _rifleSlot;
            _rifleSlot.gameObject.SetActive(true);
            if (_pistolSlot != null)
            {
                _pistolSlot.gameObject.SetActive(false);
            }
        }
    }

    public GunLikeWeapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }

    public void AddGunLikeWeapon(GunLikeWeapon weapon, Action callback)
    {
        weapon.gameObject.SetActive(false);
        weapon.transform.SetParent(_weaponHolder.transform);

        if (weapon.GetWeaponType() == WeaponType.Pistol)
        {
            AddPistol(weapon);
        } 
        else if (weapon.GetWeaponType() == WeaponType.Rifle)
        {
            AddRifle(weapon);
        }

        callback?.Invoke();
    }

    private void AddPistol(GunLikeWeapon pistol)
    {
        Debug.Log("AddPistol");
        if (_pistolSlot != null)
        {
            //todo может не надо уничтожеать?
            Destroy(_pistolSlot);
        }

        _pistolSlot = pistol;

        if (!_isWeaponSelected)
        {
            SelectPistol();
        }
    }

    private void AddRifle(GunLikeWeapon rifle)
    {
        if (_rifleSlot != null)
        {
            Destroy(_rifleSlot);
        }

        _rifleSlot = rifle;

        if (!_isWeaponSelected)
        {
            SelectRifle();
        }
    }

    public void Shoot()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Shoot(_raycastSource);
        }
    }
}

public enum WeaponType { 
    Pistol,
    Rifle
}
