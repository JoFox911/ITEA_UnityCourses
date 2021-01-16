using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SoldierWeaponManager))]
[RequireComponent(typeof(PickUpHelper))]
public class FpsController : MonoBehaviour
{
    [SerializeField]
    private Camera _fpsCamera;

    private SoldierWeaponManager _soldierWeaponManager;
    private PickUpHelper _pickUpHelper;

    private bool _isGrabPossible;
    private bool _isWeaponSelected;
    private List<Tuple<int, int>> _lastWeaponAmmoStatus;

    void Awake()
    {
        _soldierWeaponManager = gameObject.GetComponent<SoldierWeaponManager>();
        _pickUpHelper = gameObject.GetComponent<PickUpHelper>();

        _soldierWeaponManager.Initialize(_fpsCamera.gameObject);

        EventAgregator.Subscribe<GrabBtnClickedEvent>(Grab);
        EventAgregator.Subscribe<ShootBtnClickedEvent>(Shoot);
        EventAgregator.Subscribe<ReloadBtnClickedEvent>(Reload);
        EventAgregator.Subscribe<FirstSlotWeaponBtnClickedEvent>(SelectFirstSlotWeapon);
        EventAgregator.Subscribe<SecondSlotWeaponBtnClickedEvent>(SelectSecondSlotWeapon);
    }
    private void FixedUpdate()
    {
        // todo redo
        var newValue = _pickUpHelper.CheckGrab();
        if (newValue != _isGrabPossible) {
            _isGrabPossible = newValue;
            var ChangeGrabBtnVisibilityEvent = new ChangeGrabBtnVisibilityEvent();
            ChangeGrabBtnVisibilityEvent.NewVisibilityValue = newValue;
            EventAgregator.Post(this, ChangeGrabBtnVisibilityEvent);
        }
    }

    private void Grab(object sender, GrabBtnClickedEvent eventData)
    {
        Debug.Log("FPS GRAB");

        if (_isGrabPossible)
        {
            var item = _pickUpHelper.GetPickUpObject();
            // check is weapon
            var weapon = item.GetComponent<Weapon>();
            var ammo = item.GetComponent<Ammo>();
            if (weapon != null)
            {
                _soldierWeaponManager.AddWeapon(weapon, UpdateWeaponUI);
                if (weapon.GetWeaponSlotType() == SlotWeaponType.FirstSlotWeapon)
                {
                    EventAgregator.Post(this, new ChangeFirstSlotWeaponIconEvent(weapon.GetWeaponIcon()));
                }
                else if (weapon.GetWeaponSlotType() == SlotWeaponType.SecondSlotWeapon)
                {
                    EventAgregator.Post(this, new ChangeSecondSlotWeaponIconEvent(weapon.GetWeaponIcon()));
                }
            } 
            else if (ammo != null)
            {
                _soldierWeaponManager.AddAmmo(ammo, UpdateWeaponUI);
            }
        }            
    }

    private void Shoot(object sender, ShootBtnClickedEvent eventData)
    {
        Debug.Log("FPS Shoot");

        _soldierWeaponManager.Shoot(UpdateWeaponUI);
    }

    private void Reload(object sender, ReloadBtnClickedEvent eventData)
    {
        Debug.Log("FPS Reload");

        _soldierWeaponManager.Reload(UpdateWeaponUI);
    }

    private void SelectFirstSlotWeapon(object sender, FirstSlotWeaponBtnClickedEvent eventData)
    {
        Debug.Log("FPS SelectFirstSlotWeapon");

        _soldierWeaponManager.SelectFirstSlotWeapon();
        UpdateWeaponUI();
    }


    private void SelectSecondSlotWeapon(object sender, SecondSlotWeaponBtnClickedEvent eventData)
    {
        Debug.Log("FPS SelectSecondSlotWeapon");

        _soldierWeaponManager.SelectSecondSlotWeapon();
        UpdateWeaponUI();
    }


    private void UpdateWeaponUI()
    {
        var newIsWeaponSelectedValue = _soldierWeaponManager.IsWeaponSelected();
        if (_isWeaponSelected != newIsWeaponSelectedValue)
        {
            _isWeaponSelected = newIsWeaponSelectedValue;
            EventAgregator.Post(this, new ChangeIsWeaponSelectedUIVisibleElementsEvent(newIsWeaponSelectedValue));
        }


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
        _lastWeaponAmmoStatus = newAmmoStatus;
    }

}


