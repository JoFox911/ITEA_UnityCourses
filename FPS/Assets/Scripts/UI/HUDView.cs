﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [SerializeField]
    private Button _jumpBtn;

    [SerializeField]
    private Button _grabBtn;

    [SerializeField]
    private Button _shootBtn;

    [SerializeField]
    private Button _firstSlotWeaponBtn;

    [SerializeField]
    private Button _secondSlotWeaponBtn;

    [SerializeField]
    private Button _thirdSlotWeaponBtn;

    [SerializeField]
    private Button _reloadBtn;

    [SerializeField]
    private TextMeshProUGUI _firstSlotWeaponAmmoInClip;

    [SerializeField]
    private TextMeshProUGUI _firstSlotWeaponAmmoInStock;

    [SerializeField]
    private TextMeshProUGUI _secondSlotWeaponAmmoInClip;

    [SerializeField]
    private TextMeshProUGUI _secondSlotWeaponAmmoInStock;

    [SerializeField]
    private TextMeshProUGUI _thirdSlotWeaponAmmo;

    [SerializeField]
    private GameObject _aimImg;

    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Image _firstSlotWeaponIcon;

    [SerializeField]
    private Image _secondSlotWeaponIcon;

    [SerializeField]
    private Image _thirdSlotWeaponIcon;

    [SerializeField]
    private Button _pauseBtn;

    [SerializeField]
    private Button _throwGrenadeBtn;

    [SerializeField]
    private KillInfoView _killInfoView;

    [SerializeField]
    private TextMeshProUGUI _aliveEnemiesNumber;

    [SerializeField]
    private TextMeshProUGUI _killedEnemiesNumber;

    void Awake()
    {
        _jumpBtn.onClick.AddListener(EmitJumpBtnClickedEvent);
        _grabBtn.onClick.AddListener(EmitGrabBtnClickedEvent);
        _shootBtn.onClick.AddListener(EmitShootBtnClickedEvent);
        _firstSlotWeaponBtn.onClick.AddListener(EmitFirstSlotWeaponBtnClickedEvent);
        _secondSlotWeaponBtn.onClick.AddListener(EmitSecondSlotWeaponBtnClickedEvent);
        _thirdSlotWeaponBtn.onClick.AddListener(EmitThirdSlotWeaponBtnClickedEvent);        
        _reloadBtn.onClick.AddListener(EmitReloadBtnClickedEvent);
        _pauseBtn.onClick.AddListener(EmitPauseBtnClickedEvent);
        _throwGrenadeBtn.onClick.AddListener(EmitThrowGrenadeBtnClickedEvent);

        EventAgregator.Subscribe<ChangeGrabBtnVisibilityEvent>(SetGrabBtnVisibility);
        EventAgregator.Subscribe<SoldierKilledEvent>(PrintKillData);
        EventAgregator.Subscribe<ChangeHealthEvent>(SetNewHealthValue);
        EventAgregator.Subscribe<ChangeFirstSlotWeaponAmmoInClipVolumeEvent>(SetFirstSlotAmmoInClipVolume);
        EventAgregator.Subscribe<ChangeFirstSlotWeaponAmmoInStockVolumeEvent>(SetFirstSlotAmmoInStockVolume);
        EventAgregator.Subscribe<ChangeSecondSlotWeaponAmmoInClipVolumeEvent>(SetSecondSlotAmmoInClipVolume);
        EventAgregator.Subscribe<ChangeSecondSlotWeaponAmmoInStockVolumeEvent>(SetSecondSlotAmmoInStockVolume);
        EventAgregator.Subscribe<ChangeThirdSlotWeaponAmmoVolumeEvent>(SetThirdSlotAmmoVolume);
        EventAgregator.Subscribe<CurrentWeaponChangedEvent>(ChangeCurrentWeapon);
        EventAgregator.Subscribe<ChangeSlotWeaponIconEvent>(ChangeSlotWeaponIcon);
        EventAgregator.Subscribe<ChangeAliveEnemiesEvent>(ChangeAliveEnemies);
        EventAgregator.Subscribe<ChangeKilledEnemiesEvent>(ChangeKilledEnemies);
    }

    void OnDestroy()
    {
        EventAgregator.Unsubscribe<ChangeGrabBtnVisibilityEvent>(SetGrabBtnVisibility);
        EventAgregator.Unsubscribe<SoldierKilledEvent>(PrintKillData);
        EventAgregator.Unsubscribe<ChangeHealthEvent>(SetNewHealthValue);
        EventAgregator.Unsubscribe<ChangeFirstSlotWeaponAmmoInClipVolumeEvent>(SetFirstSlotAmmoInClipVolume);
        EventAgregator.Unsubscribe<ChangeFirstSlotWeaponAmmoInStockVolumeEvent>(SetFirstSlotAmmoInStockVolume);
        EventAgregator.Unsubscribe<ChangeSecondSlotWeaponAmmoInClipVolumeEvent>(SetSecondSlotAmmoInClipVolume);
        EventAgregator.Unsubscribe<ChangeSecondSlotWeaponAmmoInStockVolumeEvent>(SetSecondSlotAmmoInStockVolume);
        EventAgregator.Unsubscribe<ChangeThirdSlotWeaponAmmoVolumeEvent>(SetThirdSlotAmmoVolume);
        EventAgregator.Unsubscribe<CurrentWeaponChangedEvent>(ChangeCurrentWeapon);
        EventAgregator.Unsubscribe<ChangeSlotWeaponIconEvent>(ChangeSlotWeaponIcon);
        EventAgregator.Unsubscribe<ChangeAliveEnemiesEvent>(ChangeAliveEnemies);
        EventAgregator.Unsubscribe<ChangeKilledEnemiesEvent>(ChangeKilledEnemies);
    }

    private void ChangeAliveEnemies(object sender, ChangeAliveEnemiesEvent eventData)
    {
        _aliveEnemiesNumber.SetText(eventData.enemiesNumber.ToString());
    }

    private void ChangeKilledEnemies(object sender, ChangeKilledEnemiesEvent eventData)
    {
        _killedEnemiesNumber.SetText(eventData.enemiesNumber.ToString());
    }

    private void ChangeSlotWeaponIcon(object sender, ChangeSlotWeaponIconEvent eventData)
    {
        if (eventData.slotWeaponType == SlotWeaponType.FirstSlotWeapon)
        {
            _firstSlotWeaponIcon.sprite = eventData.weaponIcon;
        }
        else if (eventData.slotWeaponType == SlotWeaponType.SecondSlotWeapon)
        {
            _secondSlotWeaponIcon.sprite = eventData.weaponIcon;
        }
        else if (eventData.slotWeaponType == SlotWeaponType.ThirdSlotWeapon)
        {
            _thirdSlotWeaponIcon.sprite = eventData.weaponIcon;
        }
    }

    private void PrintKillData(object sender, SoldierKilledEvent eventData)
    {
        _killInfoView.AddKillInfoItem(eventData.killerInfo);
        Debug.Log(eventData.killerInfo);
    }

    private void EmitThrowGrenadeBtnClickedEvent()
    {
        EventAgregator.Post(this, new AttacklickedEvent());
    }

    private void EmitJumpBtnClickedEvent()
    {
        EventAgregator.Post(this, new JumpBtnClickedEvent());
    }

    
    private void EmitPauseBtnClickedEvent()
    {
        EventAgregator.Post(this, new PauseClickedEvent());
    }

    private void EmitGrabBtnClickedEvent()
    {
        Debug.Log("HUD GRAB");
        EventAgregator.Post(this, new GrabBtnClickedEvent());
    }

    private void EmitShootBtnClickedEvent()
    {
        Debug.Log("HUD SHOOT");
        EventAgregator.Post(this, new AttacklickedEvent());
    }

    private void EmitFirstSlotWeaponBtnClickedEvent()
    {
        Debug.Log("HUD FIRST WEAPON SLOT");
        EventAgregator.Post(this, new FirstSlotWeaponBtnClickedEvent());
    }

    private void EmitSecondSlotWeaponBtnClickedEvent()
    {
        Debug.Log("HUD SECOND WEAPON SLOT");
        EventAgregator.Post(this, new SecondSlotWeaponBtnClickedEvent());
    }

    private void EmitThirdSlotWeaponBtnClickedEvent()
    {
        Debug.Log("HUD SECOND WEAPON SLOT");
        EventAgregator.Post(this, new ThirdSlotWeaponBtnClickedEvent());
    }

    

    private void EmitReloadBtnClickedEvent()
    {
        Debug.Log("HUD RELOAD");
        EventAgregator.Post(this, new ReloadBtnClickedEvent());
    }

    private void SetGrabBtnVisibility(object sender, ChangeGrabBtnVisibilityEvent eventData)
    {
        Debug.Log("HUD SET GRAB VISIBILITY");
        _grabBtn.gameObject.SetActive(eventData.NewVisibilityValue);
    }

    private void SetFirstSlotAmmoInClipVolume(object sender, ChangeFirstSlotWeaponAmmoInClipVolumeEvent eventData)
    {
        Debug.Log("HUD SET FS AMMO IN CLIP VOLUME");
        _firstSlotWeaponAmmoInClip.SetText(eventData.newVolume.ToString());
    }

    private void SetFirstSlotAmmoInStockVolume(object sender, ChangeFirstSlotWeaponAmmoInStockVolumeEvent eventData)
    {
        Debug.Log("HUD SET FS AMMO IN STOCK VOLUME");
        _firstSlotWeaponAmmoInStock.SetText("/" + eventData.newVolume.ToString());
    }

    private void SetSecondSlotAmmoInClipVolume(object sender, ChangeSecondSlotWeaponAmmoInClipVolumeEvent eventData)
    {
        Debug.Log("HUD SET SS AMMO IN CLIP VOLUME");
        _secondSlotWeaponAmmoInClip.SetText(eventData.newVolume.ToString());
    }

    private void SetSecondSlotAmmoInStockVolume(object sender, ChangeSecondSlotWeaponAmmoInStockVolumeEvent eventData)
    {
        Debug.Log("HUD SET SS AMMO IN STOCK VOLUME");
        _secondSlotWeaponAmmoInStock.SetText("/" + eventData.newVolume.ToString());
    }

    private void SetThirdSlotAmmoVolume(object sender, ChangeThirdSlotWeaponAmmoVolumeEvent eventData)
    {
        _thirdSlotWeaponAmmo.SetText(eventData.newVolume.ToString());
    }


    private void SetNewHealthValue(object sender, ChangeHealthEvent eventData)
    {
        _healthBar.value = eventData.newValue;
    }

    private void ChangeCurrentWeapon(object sender, CurrentWeaponChangedEvent eventData)
    {
        _aimImg.gameObject.SetActive(true);
        if (eventData.slotWeaponType == SlotWeaponType.FirstSlotWeapon ||
            eventData.slotWeaponType == SlotWeaponType.SecondSlotWeapon)
        {
            _reloadBtn.gameObject.SetActive(true);
            _shootBtn.gameObject.SetActive(true);
            _throwGrenadeBtn.gameObject.SetActive(false);
        }
        else if (eventData.slotWeaponType == SlotWeaponType.ThirdSlotWeapon)
        {
            _reloadBtn.gameObject.SetActive(false);
            _shootBtn.gameObject.SetActive(false);
            _throwGrenadeBtn.gameObject.SetActive(true);
        }
    }
    

}