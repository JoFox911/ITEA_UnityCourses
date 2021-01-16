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
    private TextMeshProUGUI _firstSlotWeaponAmmoInClip;

    [SerializeField]
    private TextMeshProUGUI _firstSlotWeaponAmmoInStock;

    [SerializeField]
    private TextMeshProUGUI _secondSlotWeaponAmmoInClip;

    [SerializeField]
    private TextMeshProUGUI _secondSlotWeaponAmmoInStock;

    [SerializeField]
    private GameObject _aimImg;

    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Image _firstSlotWeaponIcon;

    [SerializeField]
    private Image _secondSlotWeaponIcon;

    void Awake()
    {
        _jumpBtn.onClick.AddListener(EmitJumpBtnClickedEvent);
        _grabBtn.onClick.AddListener(EmitGrabBtnClickedEvent);
        _shootBtn.onClick.AddListener(EmitShootBtnClickedEvent);
        _firstSlotWeaponBtn.onClick.AddListener(EmitFirstSlotWeaponBtnClickedEvent);
        _secondSlotWeaponBtn.onClick.AddListener(EmitSecondSlotWeaponBtnClickedEvent);
        _reloadBtn.onClick.AddListener(EmitReloadBtnClickedEvent);

        EventAgregator.Subscribe<ChangeGrabBtnVisibilityEvent>(SetGrabBtnVisibility);
        EventAgregator.Subscribe<ChangeIsWeaponSelectedUIVisibleElementsEvent>(SetIsWeaponSelectedUIVisibleElements);
        EventAgregator.Subscribe<SoldierKilledEvent>(PrintKillData);
        EventAgregator.Subscribe<ChangeHealthEvent>(SetNewHealthValue);

        EventAgregator.Subscribe<ChangeFirstSlotWeaponAmmoInClipVolumeEvent>(SetFirstSlotAmmoInClipVolume);
        EventAgregator.Subscribe<ChangeFirstSlotWeaponAmmoInStockVolumeEvent>(SetFirstSlotAmmoInStockVolume);
        EventAgregator.Subscribe<ChangeSecondSlotWeaponAmmoInClipVolumeEvent>(SetSecondSlotAmmoInClipVolume);
        EventAgregator.Subscribe<ChangeSecondSlotWeaponAmmoInStockVolumeEvent>(SetSecondSlotAmmoInStockVolume);
        EventAgregator.Subscribe<ChangeFirstSlotWeaponIconEvent>(ChangeFirstSlotWeaponIcon);
        EventAgregator.Subscribe<ChangeSecondSlotWeaponIconEvent>(ChangeSecondSlotWeaponIcon);
    }

    private void PrintKillData(object sender, SoldierKilledEvent eventData)
    {
        Debug.Log(eventData.killerName + " " + eventData.killerWeaponName + " killed : " + eventData.victimName);
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

    private void ChangeFirstSlotWeaponIcon(object sender, ChangeFirstSlotWeaponIconEvent eventData)
    {
        Debug.Log("HUD ChangeSecondSlotWeaponIcon");
        _firstSlotWeaponIcon.sprite = eventData.NewIcon;
    }

    private void ChangeSecondSlotWeaponIcon(object sender, ChangeSecondSlotWeaponIconEvent eventData)
    {
        Debug.Log("HUD ChangeSecondSlotWeaponIcon");
        _secondSlotWeaponIcon.sprite = eventData.NewIcon;
    }

    private void SetIsWeaponSelectedUIVisibleElements(object sender, ChangeIsWeaponSelectedUIVisibleElementsEvent eventData)
    {
        Debug.Log("HUD SET VISIBILITY ON CHANGE IS WEAPON SELECTED");
        _aimImg.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
        _reloadBtn.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
        _shootBtn.gameObject.SetActive(eventData.NewIsWeaponSelectedValue);
    }

    private void SetNewHealthValue(object sender, ChangeHealthEvent eventData)
    {
        _healthBar.value = eventData.newValue;
    }
}
