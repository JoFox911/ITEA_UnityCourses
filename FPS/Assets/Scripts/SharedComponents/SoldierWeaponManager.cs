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

    private Dictionary<AmmoType, int> _availableAmmo;

    private readonly int _slotsNumber = 3;
    private List<Weapon> _weaponsList;
    private Weapon _currentWeapon;

    private GameObject _raycastSource;

    private Animator _anim;
    private AudioSource _audioSource;
    private Soldier _soldier;

    public bool IsNoAmmoOnAllWeapons;
    public bool isReloading;

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
            IsNoAmmoOnAllWeapons = !IsWeaponWithAvailableAmmoExists();

            //var tet = IsCurrentWeaponAmmoAvailable();

            //var t1 = IsWeaponSelected();
            //var t2 = IsReload();
            //if (t1)
            //{
            //    var t3 = GetCurrentWeapon().GetIsOutOfAmmo();
            //    var t4 = GetCurrentWeapon().GetWeaponSlotType() == SlotWeaponType.ThirdSlotWeapon;
            //    var t5 = IsCurrentWeaponAmmoAvailable();
            //}
            


            if (IsWeaponSelected() && !isReloading && (GetCurrentWeapon().GetIsOutOfAmmo() || 
                GetCurrentWeapon().GetWeaponSlotType() == SlotWeaponType.ThirdSlotWeapon && !IsCurrentWeaponAmmoAvailable()))
            {
                if (IsCurrentWeaponAmmoAvailable())
                {
                    IsNoAmmoOnAllWeapons = false;
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

        weapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        weapon.transform.localPosition = new Vector3(0, 0, 0);

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

        Destroy(ammo.gameObject);

        callback?.Invoke();

    }

    private void AddAmmoByType(AmmoType ammoType, int ammoNumber)
    {
        ammoNumber = 999;
        if (_availableAmmo.ContainsKey(ammoType))
        {
            _availableAmmo[ammoType] += ammoNumber;
        }
        else
        {
            _availableAmmo.Add(ammoType, ammoNumber);
        }

    }

    public void Attack(Action callback)
    {
        Debug.Log("IS WEAPON READY" + _currentWeapon.IsWeaponReady());
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

    private void ThrowGrenade()
    {
        if (_availableAmmo.ContainsKey(AmmoType.Grenade) && _availableAmmo[AmmoType.Grenade] > 0)
        {
            var grenadeInstance = Instantiate(_weaponsList[2]);
            _availableAmmo[AmmoType.Grenade]--;
            grenadeInstance.transform.position = _weaponsList[2].transform.position;

            if (_soldier.GetIsBot())
            {
                //_anim.SetTrigger("throwGrenage");
            }

            var rigidBody = grenadeInstance.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.isKinematic = false;
                rigidBody.useGravity = true;
                rigidBody.AddForce(_raycastSource.transform.forward * _throwGrenadeRange, ForceMode.Impulse);
            }

            
            grenadeInstance.Shoot(null, _soldier.GetName());
        }
    }

    private void Shoot()
    {
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

            if (!_soldier.GetIsBot())
            {
                
                EventAgregator.Post(this, new CurrentWeaponChangedEvent(_currentWeapon.GetWeaponSlotType()));
            }
        }
    }

    private void AddWeaponToSlot(int slotIndex, Weapon weapon)
    {
        //Debug.Log("AddWeaponToSlot");
        if (_weaponsList[slotIndex] != null)
        {
            if (_weaponsList[slotIndex].gameObject.activeInHierarchy)
            {
                weapon.gameObject.SetActive(true);
                _currentWeapon = weapon;
            }
            //todo может не надо уничтожеать?
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

    public bool IsCurrentWeaponAmmoAvailable()
    {
        return IsWeaponAmmoAvailable(_currentWeapon);
    }

    public bool IsWeaponAmmoAvailable(Weapon weapon)
    {
        if (!weapon)
        {
            return false;
        }
        //var t1 = weapon.GetIsOutOfAmmo();
        //var t2 = weapon.GetWeaponAmmoType();
        //if (_availableAmmo.ContainsKey(t2))
        //{
        //    var t3 = _availableAmmo[t2];

        //}
        //var res = (weapon.GetWeaponSlotType() != SlotWeaponType.ThirdSlotWeapon && !weapon.GetIsOutOfAmmo()) || (_availableAmmo.ContainsKey(weapon.GetWeaponAmmoType()) && _availableAmmo[weapon.GetWeaponAmmoType()] > 0);
        return (weapon.GetWeaponSlotType() != SlotWeaponType.ThirdSlotWeapon && !weapon.GetIsOutOfAmmo()) || (_availableAmmo.ContainsKey(weapon.GetWeaponAmmoType()) && _availableAmmo[weapon.GetWeaponAmmoType()] > 0);
    }

    public void TryToSelectWeaponWithAvailableAmmo()
    {
        //Debug.Log("TryToSelectWeaponWithAvailableAmmo");
        for (var i = 0; i < _weaponsList.Count; i++)
        {
            if (_weaponsList[i] != null && IsWeaponAmmoAvailable(_weaponsList[i]))
            {
                SelectSlotWeapon(i);
                return;
            }
        }
    }

    public bool IsWeaponWithAvailableAmmoExists()
    {
        for (var i = 0; i < _weaponsList.Count; i++)
        {
            if (_weaponsList[i] != null && IsWeaponAmmoAvailable(_weaponsList[i]))
            {
                return true;
            }
        }
        return false;
    }

    //public bool IsReload()
    //{
    //    if (IsWeaponSelected())
    //    {
    //        return _currentWeapon.GetIsReloading();
    //    }
    //    return false;
    //}

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

}

