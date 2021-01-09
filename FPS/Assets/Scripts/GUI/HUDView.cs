using System;
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
    private Button _reloadBtn;

    [SerializeField]
    private TextMeshProUGUI _ammoInClip;

    [SerializeField]
    private TextMeshProUGUI _ammoInStock;

    [SerializeField]
    private GameObject _aimImg;

    void Awake()
    {
        _jumpBtn.onClick.AddListener(EmitJumpBtnClickedEvent);
        _grabBtn.onClick.AddListener(EmitGrabBtnClickedEvent);
        _shootBtn.onClick.AddListener(EmitShootBtnClickedEvent);
        _firstSlotWeaponBtn.onClick.AddListener(EmitFirstSlotWeaponBtnClickedEvent);
        _secondSlotWeaponBtn.onClick.AddListener(EmitSecondSlotWeaponBtnClickedEvent);
        _reloadBtn.onClick.AddListener(EmitReloadBtnClickedEvent);

        EventAgregator.Subscribe<ChangeGrabBtnVisibilityEvent>(SetGrabBtnVisibility);
        EventAgregator.Subscribe<ChangeAmmoInClipVolumeEvent>(SetAmmoInClipVolume);
        EventAgregator.Subscribe<ChangeAmmoInStockVolumeEvent>(SetAmmoInStockVolume);
        EventAgregator.Subscribe<ChangeIsWeaponSelectedUIVisibleElementsEvent>(SetIsWeaponSelectedUIVisibleElements);
    }

    private void EmitJumpBtnClickedEvent()
    {
        EventAgregator.Post(this, new JumpBtnClickedEvent());
    }

    private void EmitGrabBtnClickedEvent()
    {
        Debug.Log("HUD GRAB");
        EventAgregator.Post(this, new GrabBtnClickedEvent());
    }

    private void EmitShootBtnClickedEvent()
    {
        Debug.Log("HUD SHOOT");
        EventAgregator.Post(this, new ShootBtnClickedEvent());
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

    private void SetAmmoInClipVolume(object sender, ChangeAmmoInClipVolumeEvent eventData)
    {
        Debug.Log("HUD SET AMMO IN CLIP VOLUME");
        _ammoInClip.SetText(eventData.newVolume.ToString());
    }

    private void SetAmmoInStockVolume(object sender, ChangeAmmoInStockVolumeEvent eventData)
    {
        Debug.Log("HUD SET AMMO IN STOCK VOLUME");
        _ammoInStock.SetText(eventData.newVolume.ToString());
    }

    private void SetIsWeaponSelectedUIVisibleElements(object sender, ChangeIsWeaponSelectedUIVisibleElementsEvent eventData)
    {
        Debug.Log("HUD SET VISIBILITY ON CHANGE IS WEAPON SELECTED");
        _ammoInStock.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
        _ammoInClip.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
        _aimImg.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
        _reloadBtn.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
        _shootBtn.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
    }

    
}
