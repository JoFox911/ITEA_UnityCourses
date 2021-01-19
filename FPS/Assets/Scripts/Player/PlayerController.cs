using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SoldierWeaponManager))]
[RequireComponent(typeof(PickUpHelper))]
[RequireComponent(typeof(Soldier))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera _fpsCamera;

    private SoldierWeaponManager _soldierWeaponManager;
    private PickUpHelper _pickUpHelper;
    private Soldier _soldier;

    private bool _isGrabPossible;
    private List<Tuple<int, int>> _lastWeaponAmmoStatus;

    void Awake()
    {
        _soldierWeaponManager = gameObject.GetComponent<SoldierWeaponManager>();
        _pickUpHelper = gameObject.GetComponent<PickUpHelper>();
        _soldier = gameObject.GetComponent<Soldier>();

        _soldierWeaponManager.Initialize(_fpsCamera.gameObject);

        EventAgregator.Subscribe<GrabBtnClickedEvent>(Grab);
        EventAgregator.Subscribe<AttacklickedEvent>(Attack);
        EventAgregator.Subscribe<ReloadBtnClickedEvent>(Reload);
        EventAgregator.Subscribe<FirstSlotWeaponBtnClickedEvent>(SelectFirstSlotWeapon);
        EventAgregator.Subscribe<SecondSlotWeaponBtnClickedEvent>(SelectSecondSlotWeapon);
        EventAgregator.Subscribe<ThirdSlotWeaponBtnClickedEvent>(SelectThirdSlotWeapon);
    }
    private void Update()
    {
        if (_pickUpHelper.IsItemDetected != _isGrabPossible) {
            _isGrabPossible = _pickUpHelper.IsItemDetected;
            EventAgregator.Post(this, new ChangeGrabBtnVisibilityEvent(_isGrabPossible));
        }
    }

    void OnDestroy()
    {
        EventAgregator.Unsubscribe<GrabBtnClickedEvent>(Grab);
        EventAgregator.Unsubscribe<AttacklickedEvent>(Attack);
        EventAgregator.Unsubscribe<ReloadBtnClickedEvent>(Reload);
        EventAgregator.Unsubscribe<FirstSlotWeaponBtnClickedEvent>(SelectFirstSlotWeapon);
        EventAgregator.Unsubscribe<SecondSlotWeaponBtnClickedEvent>(SelectSecondSlotWeapon);
        EventAgregator.Unsubscribe<ThirdSlotWeaponBtnClickedEvent>(SelectThirdSlotWeapon);
    }

    private void Grab(object sender, GrabBtnClickedEvent eventData)
    {
        if (_isGrabPossible)
        {
            var item = _pickUpHelper.GetPickUpObject();
            // check is weapon
            var weapon = item.GetComponent<Weapon>();
            var ammo = item.GetComponent<Ammo>();
            var heal = item.GetComponent<Heal>();

            if (weapon != null)
            {
                _soldierWeaponManager.AddWeapon(weapon, UpdateAmmoUI);
            } 
            else if (ammo != null)
            {
                _soldierWeaponManager.AddAmmo(ammo, UpdateAmmoUI);
            }
            else if (heal != null)
            {
                Destroy(heal.transform.gameObject);
                _soldier.ApplyHeal(heal.GetHealpoints());
            }
        }            
    }

    private void Attack(object sender, AttacklickedEvent eventData)
    {
        _soldierWeaponManager.Attack(UpdateAmmoUI);
    }

    private void Reload(object sender, ReloadBtnClickedEvent eventData)
    {
        _soldierWeaponManager.Reload(UpdateAmmoUI);
    }

    private void SelectFirstSlotWeapon(object sender, FirstSlotWeaponBtnClickedEvent eventData)
    {
        _soldierWeaponManager.SelectFirstSlotWeapon();
        UpdateAmmoUI();
    }


    private void SelectSecondSlotWeapon(object sender, SecondSlotWeaponBtnClickedEvent eventData)
    {
        _soldierWeaponManager.SelectSecondSlotWeapon();
        UpdateAmmoUI();
    }

    private void SelectThirdSlotWeapon(object sender, ThirdSlotWeaponBtnClickedEvent eventData)
    {
        _soldierWeaponManager.SelectThirdSlotWeapon();
        UpdateAmmoUI();
    }


    // todo redo?
    private void UpdateAmmoUI()
    {
        var newAmmoStatus = _soldierWeaponManager.GetWeaponAmmoStatuses();

        if (newAmmoStatus[0] != null)
        {
            if (_lastWeaponAmmoStatus == null || _lastWeaponAmmoStatus[0] == null ||
                (_lastWeaponAmmoStatus != null && _lastWeaponAmmoStatus[0] != null && newAmmoStatus[0].Item1 != _lastWeaponAmmoStatus[0].Item1))
            {
                EventAgregator.Post(this, new ChangeFirstSlotWeaponAmmoInClipVolumeEvent(newAmmoStatus[0].Item1));
            }
            if (_lastWeaponAmmoStatus == null || _lastWeaponAmmoStatus[0] == null ||
                (_lastWeaponAmmoStatus != null && _lastWeaponAmmoStatus[0] != null && newAmmoStatus[0].Item2 != _lastWeaponAmmoStatus[0].Item2))
            {
                EventAgregator.Post(this, new ChangeFirstSlotWeaponAmmoInStockVolumeEvent(newAmmoStatus[0].Item2));
            }
        }

        if (newAmmoStatus[1] != null)
        {
            if (_lastWeaponAmmoStatus == null || _lastWeaponAmmoStatus[1] == null ||
                (_lastWeaponAmmoStatus != null && _lastWeaponAmmoStatus[1] != null && newAmmoStatus[1].Item1 != _lastWeaponAmmoStatus[1].Item1))
            {
                EventAgregator.Post(this, new ChangeSecondSlotWeaponAmmoInClipVolumeEvent(newAmmoStatus[1].Item1));
            }
            if (_lastWeaponAmmoStatus == null || _lastWeaponAmmoStatus[1] == null ||
                (_lastWeaponAmmoStatus != null && _lastWeaponAmmoStatus[1] != null && newAmmoStatus[1].Item2 != _lastWeaponAmmoStatus[1].Item2))
            {
                EventAgregator.Post(this, new ChangeSecondSlotWeaponAmmoInStockVolumeEvent(newAmmoStatus[1].Item2));
            }
        }

        if (newAmmoStatus[2] != null)
        {
            if (_lastWeaponAmmoStatus == null || _lastWeaponAmmoStatus[2] == null ||
                (_lastWeaponAmmoStatus != null && _lastWeaponAmmoStatus[2] != null && newAmmoStatus[2].Item2 != _lastWeaponAmmoStatus[2].Item2))
            {
                EventAgregator.Post(this, new ChangeThirdSlotWeaponAmmoVolumeEvent(newAmmoStatus[2].Item2));
            }
        }


        
        _lastWeaponAmmoStatus = newAmmoStatus;
    }

}


