using System;
using System.Collections.Generic;
using UnityEngine;

public class SoldierWeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _weaponHolder;

    private Dictionary<AmmoType, int> _availableAmmo;

    private readonly int _slotsNumber = 2;
    private List<Weapon> _weaponsList;
    private Weapon _currentWeapon;

    private GameObject _raycastSource;

    public void Initialize(GameObject raycastSource)
    {
        _weaponsList = new List<Weapon>();
        _availableAmmo = new Dictionary<AmmoType, int>();

        for (var i = 0; i < _slotsNumber; i++)
        {
            _weaponsList.Add(null);
        }

        _raycastSource = raycastSource;
    }

    public void SelectFirstSlotWeapon()
    {
        SelectSlotWeapon(0);
    }

    public void SelectSecondSlotWeapon()
    {
        SelectSlotWeapon(1);
    }

    public Weapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }

    public void AddWeapon(Weapon weapon, Action callback)
    {
        // прячем объект и перемещаем в контейнер для оружия
        weapon.gameObject.SetActive(false);
        weapon.transform.SetParent(_weaponHolder.transform);

        //Put the game object in the ignore raycast layer(2)
        weapon.gameObject.layer = 2;

        var shootingWeapon = (ShootingWeapon)weapon;
        AddAmmoByType(shootingWeapon.GetWeaponAmmoType(), shootingWeapon.GetWeaponAmmoVolume());

        if (weapon.GetWeaponSlotType() == SlotWeaponType.FirstSlotWeapon)
        {
            AddWeaponToSlot(0, weapon);
        }
        else if (weapon.GetWeaponSlotType() == SlotWeaponType.SecondSlotWeapon)
        {
            AddWeaponToSlot(1, weapon);
        }

        callback?.Invoke();
    }


    public void AddAmmo(Ammo ammo, Action callback)
    {
        AddAmmoByType(ammo.GetAmmoType(), ammo.GetAmmoVolume());

        Destroy(ammo.gameObject);

        callback?.Invoke();

    }

    private void AddAmmoByType(AmmoType ammoType, int ammoNumber)
    {
        if (_availableAmmo.ContainsKey(ammoType))
        {
            _availableAmmo[ammoType] += ammoNumber;
        }
        else
        {
            _availableAmmo.Add(ammoType, ammoNumber);
        }

    }

    public void Shoot(Action callback)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Shoot(_raycastSource);
            callback?.Invoke();
        }
    }

    public void Reload(Action callback)
    {
        if (_currentWeapon != null)
        {
            var currentWeaponPossibleAmmo = _availableAmmo[_currentWeapon.GetWeaponAmmoType()];
            if (currentWeaponPossibleAmmo > 0)
            {
                _currentWeapon.Reload(currentWeaponPossibleAmmo, out int remaining);
                _availableAmmo[_currentWeapon.GetWeaponAmmoType()] = remaining;
                callback?.Invoke();
            }
        }
    }

    private void SelectSlotWeapon(int slotIndex)
    {
        if (_weaponsList[slotIndex] != null)
        {
            _currentWeapon = _weaponsList[slotIndex];

            _currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            _currentWeapon.transform.localPosition = new Vector3(0, 0, 0);

            for (var i = 0; i < _weaponsList.Count; i++)
            {
                if (i == slotIndex)
                {
                    _weaponsList[i].gameObject.SetActive(true);
                }
                else
                {
                    if (_weaponsList[i] != null)
                    {
                        _weaponsList[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void AddWeaponToSlot(int slotIndex, Weapon weapon)
    {
        if (_weaponsList[slotIndex] != null)
        {
            //todo может не надо уничтожеать?
            Destroy(_weaponsList[slotIndex]);
        }

        _weaponsList[slotIndex] = weapon;

        if (!IsWeaponSelected())
        {
            SelectSlotWeapon(slotIndex);
        }
    }

    public bool IsWeaponSelected()
    {
        return _currentWeapon != null;
    }

    public int GetCurrentWeaponAvailableAmmo() 
    {
        if (IsWeaponSelected())
        { 
            return _availableAmmo[_currentWeapon.GetWeaponAmmoType()];
        }
        return 0;
    }
}

