using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Soldier))]
public class SoldierWeaponManager : MonoBehaviour
{
    [SerializeField]
    protected float _throwGrenadeRange = 10f;

    [SerializeField]
    private GameObject _weaponHolder;

    public bool IsNoAmmoOnAllWeapons;
    public bool isReloading;
    public bool isWeaponReady;

    private readonly int _slotsNumber = 3;
    private readonly int _ignoreRaycastLayer = 2;

    private Dictionary<AmmoType, int> _availableAmmo;    

    private List<Weapon> _weaponsList;
    private Weapon _currentWeapon;

    private GameObject _raycastSource;

    private Animator _anim;
    private AudioSource _audioSource;
    private Soldier _soldier;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _soldier = GetComponent<Soldier>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        AnimationCheck();        

        //  также можно использовать если захотим добавить код когда автоперезарядка и смена оружия
        if (_soldier.GetIsBot())
        {
            WeaponCheck();

            if (IsWeaponSelected() && !isReloading && !IsWeaponAmmoLoaded(_currentWeapon))
            {
                if (IsWeaponAmmoAvailable(_currentWeapon))
                {
                    Reload(null);
                }
                else
                {
                    TryToSelectWeaponWithAvailableAmmo();
                }
            }
        }
    }

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

    public void SelectThirdSlotWeapon()
    {
        SelectSlotWeapon(2);
    }

    public void AddWeapon(Weapon weapon, Action callback)
    {
        // прячем объект и перемещаем в контейнер для оружия
        weapon.gameObject.SetActive(false);
        weapon.transform.SetParent(_weaponHolder.transform);

        //Put the game object in the ignore raycast layer(2)
        weapon.gameObject.layer = _ignoreRaycastLayer;

        weapon.transform.localEulerAngles = Vector3.zero;
        weapon.transform.localPosition = Vector3.zero;

        // add ammo from weapon to all ammo list
        AddAmmoByType(weapon.GetWeaponAmmoType(), weapon.GetWeaponAmmoVolume());

        if (weapon.GetWeaponSlotType() == SlotWeaponType.FirstSlotWeapon)
        {
            AddWeaponToSlot(0, weapon);
        }
        else if (weapon.GetWeaponSlotType() == SlotWeaponType.SecondSlotWeapon)
        {
            AddWeaponToSlot(1, weapon);
        }
        else if (weapon.GetWeaponSlotType() == SlotWeaponType.ThirdSlotWeapon)
        {
            AddWeaponToSlot(2, weapon);
        }

        callback?.Invoke();
    }

    public void AddAmmo(Ammo ammo, Action callback)
    {
        AddAmmoByType(ammo.GetAmmoType(), ammo.GetAmmoVolume());

        // удаляем сам объект
        Destroy(ammo.gameObject);

        callback?.Invoke();

    }

    public void Attack(Action callback)
    {
        if (IsWeaponSelected() && !isReloading && _currentWeapon.IsWeaponReady())
        {
            if (_currentWeapon.GetWeaponSlotType() == SlotWeaponType.ThirdSlotWeapon)
            {
                ThrowGrenade();
            }
            else
            {
                Shoot();
            }
            callback?.Invoke();
        }
    }

    public void Reload(Action callback)
    {
        if (IsWeaponSelected())
        {
            var currentWeaponPossibleAmmo = _availableAmmo[_currentWeapon.GetWeaponAmmoType()];
            if (currentWeaponPossibleAmmo > 0)
            {
                _anim.Play("m_weapon_reload", 0, 0f);
                if (_soldier.GetIsBot())
                {
                    AudioManager.PlaySFXOnAudioSource(SFXType.Reload, _audioSource);
                }
                else
                {
                    AudioManager.PlaySFX(SFXType.Reload);
                }

                var requiredNumberOfAmmo = _currentWeapon.MissingNumberOfAmmo();
                var availableAmmoForWeaponClip = currentWeaponPossibleAmmo >= requiredNumberOfAmmo
                                                ? requiredNumberOfAmmo
                                                : currentWeaponPossibleAmmo;

                _currentWeapon.Reload(availableAmmoForWeaponClip);
                _availableAmmo[_currentWeapon.GetWeaponAmmoType()] = currentWeaponPossibleAmmo - availableAmmoForWeaponClip;
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

            if (!_soldier.GetIsBot())
            {
                
                EventAgregator.Post(this, new CurrentWeaponChangedEvent(_currentWeapon.GetWeaponSlotType()));
            }
        }
    }

    private void AddWeaponToSlot(int slotIndex, Weapon weapon)
    {
        if (_weaponsList[slotIndex] != null)
        {
            if (_weaponsList[slotIndex].gameObject.activeInHierarchy)
            {
                weapon.gameObject.SetActive(true);
                _currentWeapon = weapon;
            }
            Destroy(_weaponsList[slotIndex].gameObject);
        }

        if (!_soldier.GetIsBot())
        {
            EventAgregator.Post(this, new ChangeSlotWeaponIconEvent(weapon.GetWeaponSlotType(), weapon.GetWeaponIcon()));
        }

        _weaponsList[slotIndex] = weapon;

        if (!IsWeaponSelected())
        {
            SelectSlotWeapon(slotIndex);
        }
    }

    public List<Tuple<int, int>> GetWeaponAmmoStatuses()
    {
        var result = new List<Tuple<int, int>>();
        foreach (var weapon in _weaponsList)
        {
            if (weapon != null)
            {
                result.Add(new Tuple<int, int>(weapon.GetCurrentAmmo(), _availableAmmo[weapon.GetWeaponAmmoType()]));
            }
            else
            {
                result.Add(null);
            }
        }
        return result;
    }

    private void Shoot()
    {
        if (_currentWeapon == null)
        {
            return;
        }

        _currentWeapon.Shoot(_raycastSource, _soldier.GetName());

        if (_soldier.GetIsBot())
        {
            _anim.SetTrigger("shoot");
            AudioManager.PlaySFXOnAudioSource(_currentWeapon.GetWeaponSound(), _audioSource);
        }
        else
        {
            AudioManager.PlaySFX(_currentWeapon.GetWeaponSound());
        }
    }

    private void ThrowGrenade()
    {
        if (_availableAmmo.ContainsKey(AmmoType.Grenade) && _availableAmmo[AmmoType.Grenade] > 0)
        {
            _availableAmmo[AmmoType.Grenade]--;

            var grenadeOrig = GetThirdSlotWeapon();
            var grenadeInstance = Instantiate(grenadeOrig);           
            grenadeInstance.transform.position = grenadeOrig.transform.position;

            var rig = grenadeInstance.GetComponent<Rigidbody>();
            if (rig != null)
            {
                rig.isKinematic = false;
                rig.useGravity = true;
                rig.AddForce(_raycastSource.transform.forward * _throwGrenadeRange, ForceMode.Impulse);
            }

            grenadeInstance.Shoot(null, _soldier.GetName());
        }
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

    private bool IsWeaponSelected()
    {
        return _currentWeapon != null;
    }

    // есть ли еще патроны в запасе или в самом оружии
    private bool IsWeaponAmmoAvailable(Weapon weapon)
    {
        if (!weapon)
        {
            return false;
        }

        // проверяем наличие патронов в самом оружии (актуально для всех оружий кроме гранат, их проверяем второй проверкой)
        if (IsWeaponAmmoLoaded(weapon))
        {
            return true;
        }
        // если патронов нет в оружии то проверим в запасе патронов
        else if (_availableAmmo.ContainsKey(weapon.GetWeaponAmmoType()) && _availableAmmo[weapon.GetWeaponAmmoType()] > 0)
        {
            return true;
        }
        return false;
    }

    //  есть ли еще патроны в самом оружии
    private bool IsWeaponAmmoLoaded(Weapon weapon) 
    {
        if (!weapon)
        {
            return false;
        }

        // проверяем наличие патронов в самом оружии (актуально для всех оружий кроме гранат, их проверяем второй проверкой)
        if (weapon.GetWeaponSlotType() != SlotWeaponType.ThirdSlotWeapon)
        {
            return !weapon.IsOutOfAmmo();
        }
        // есть ли патроны в запасе
        else
        {
            return _availableAmmo.ContainsKey(weapon.GetWeaponAmmoType()) && _availableAmmo[weapon.GetWeaponAmmoType()] > 0;
        }
    }

    private void TryToSelectWeaponWithAvailableAmmo()
    {
        var weaponIndex = IndexOfWeaponWithAvailableAmmo();
        if (weaponIndex != -1)
        {
            SelectSlotWeapon(weaponIndex);
        }
    }

    private int IndexOfWeaponWithAvailableAmmo()
    {
        if (_weaponsList != null)
        {
            for (var i = 0; i < _weaponsList.Count; i++)
            {
                if (_weaponsList[i] != null && IsWeaponAmmoAvailable(_weaponsList[i]))
                {
                    return i;
                }
            }
        }

        return -1;
    }

    private void AnimationCheck()
    {
        //Check if reloading
        //Check both animations
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("m_weapon_reload"))
        {
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
    }

    private void WeaponCheck()
    {
        IsNoAmmoOnAllWeapons = IndexOfWeaponWithAvailableAmmo() == -1;

        if (IsWeaponSelected() && !IsNoAmmoOnAllWeapons)
        {
            isWeaponReady = true;
        }
        else
        {
            isWeaponReady = false;
        }


    }

    private Weapon GetThirdSlotWeapon()
    {
        return _weaponsList[2];
    }
}