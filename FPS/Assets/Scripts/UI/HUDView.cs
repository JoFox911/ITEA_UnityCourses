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
    private Button _shootBtn2;

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
    private TextMeshProUGUI _aliveEnemiesNumber;

    [SerializeField]
    private KillInfoView _killInfoView;

    void Awake()
    {
        if (_jumpBtn != null)
        {
            _jumpBtn.onClick.AddListener(EmitJumpBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("JumpBtn");
        }

        if (_grabBtn != null)
        {
            _grabBtn.onClick.AddListener(EmitGrabBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("GrabBtn");
        }

        if (_shootBtn != null)
        {
            _shootBtn.onClick.AddListener(EmitShootBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("ShootBtn");
        }

        if (_shootBtn2 != null)
        {
            _shootBtn2.onClick.AddListener(EmitShootBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("ShootBtn2");
        }

        if (_reloadBtn != null)
        {
            _reloadBtn.onClick.AddListener(EmitReloadBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("ReloadBtn");
        }

        if (_pauseBtn != null)
        {
            _pauseBtn.onClick.AddListener(EmitPauseBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("PauseBtn");
        }

        if (_throwGrenadeBtn != null)
        {
            _throwGrenadeBtn.onClick.AddListener(EmitThrowGrenadeBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("ThrowGrenadeBtn");
        }

        if (_firstSlotWeaponBtn != null)
        {
            _firstSlotWeaponBtn.onClick.AddListener(EmitFirstSlotWeaponBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("FirstSlotWeaponBtn");
        }

        if (_secondSlotWeaponBtn != null)
        {
            _secondSlotWeaponBtn.onClick.AddListener(EmitSecondSlotWeaponBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("SecondSlotWeaponBtn");
        }

        if (_thirdSlotWeaponBtn != null)
        {
            _thirdSlotWeaponBtn.onClick.AddListener(EmitThirdSlotWeaponBtnClickedEvent);
        }
        else
        {
            Common.ObjectNotAssignedWarning("ThirdSlotWeaponBtn");
        }

        EventAgregator.Subscribe<ChangeGrabBtnVisibilityEvent>(SetGrabBtnVisibility);
        EventAgregator.Subscribe<ChangeHealthEvent>(SetNewHealthValue);
        EventAgregator.Subscribe<ChangeFirstSlotWeaponAmmoInClipVolumeEvent>(SetFirstSlotAmmoInClipVolume);
        EventAgregator.Subscribe<ChangeFirstSlotWeaponAmmoInStockVolumeEvent>(SetFirstSlotAmmoInStockVolume);
        EventAgregator.Subscribe<ChangeSecondSlotWeaponAmmoInClipVolumeEvent>(SetSecondSlotAmmoInClipVolume);
        EventAgregator.Subscribe<ChangeSecondSlotWeaponAmmoInStockVolumeEvent>(SetSecondSlotAmmoInStockVolume);
        EventAgregator.Subscribe<ChangeThirdSlotWeaponAmmoVolumeEvent>(SetThirdSlotAmmoVolume);
        EventAgregator.Subscribe<CurrentWeaponChangedEvent>(ChangeCurrentWeapon);
        EventAgregator.Subscribe<ChangeSlotWeaponIconEvent>(ChangeSlotWeaponIcon);
        EventAgregator.Subscribe<ChangeAliveEnemiesEvent>(ChangeAliveEnemies);
        EventAgregator.Subscribe<SoldierKilledEvent>(PrintKillData);
    }

    void OnDestroy()
    {
        EventAgregator.Unsubscribe<ChangeGrabBtnVisibilityEvent>(SetGrabBtnVisibility);
        EventAgregator.Unsubscribe<ChangeHealthEvent>(SetNewHealthValue);
        EventAgregator.Unsubscribe<ChangeFirstSlotWeaponAmmoInClipVolumeEvent>(SetFirstSlotAmmoInClipVolume);
        EventAgregator.Unsubscribe<ChangeFirstSlotWeaponAmmoInStockVolumeEvent>(SetFirstSlotAmmoInStockVolume);
        EventAgregator.Unsubscribe<ChangeSecondSlotWeaponAmmoInClipVolumeEvent>(SetSecondSlotAmmoInClipVolume);
        EventAgregator.Unsubscribe<ChangeSecondSlotWeaponAmmoInStockVolumeEvent>(SetSecondSlotAmmoInStockVolume);
        EventAgregator.Unsubscribe<ChangeThirdSlotWeaponAmmoVolumeEvent>(SetThirdSlotAmmoVolume);
        EventAgregator.Unsubscribe<CurrentWeaponChangedEvent>(ChangeCurrentWeapon);
        EventAgregator.Unsubscribe<ChangeSlotWeaponIconEvent>(ChangeSlotWeaponIcon);
        EventAgregator.Unsubscribe<ChangeAliveEnemiesEvent>(ChangeAliveEnemies);
        EventAgregator.Unsubscribe<SoldierKilledEvent>(PrintKillData);
    }

    private void PrintKillData(object sender, SoldierKilledEvent eventData)
    {
        _killInfoView.AddKillInfoItem(eventData.killerInfo);
        Debug.Log(eventData.killerInfo);
    }

    private void ChangeAliveEnemies(object sender, ChangeAliveEnemiesEvent eventData)
    {
        _aliveEnemiesNumber.SetText(eventData.enemiesNumber.ToString());
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
        EventAgregator.Post(this, new GrabBtnClickedEvent());
    }

    private void EmitShootBtnClickedEvent()
    {
        EventAgregator.Post(this, new AttacklickedEvent());
    }

    private void EmitFirstSlotWeaponBtnClickedEvent()
    {
        EventAgregator.Post(this, new FirstSlotWeaponBtnClickedEvent());
    }

    private void EmitSecondSlotWeaponBtnClickedEvent()
    {
        EventAgregator.Post(this, new SecondSlotWeaponBtnClickedEvent());
    }

    private void EmitThirdSlotWeaponBtnClickedEvent()
    {
        EventAgregator.Post(this, new ThirdSlotWeaponBtnClickedEvent());
    }
    
    private void EmitReloadBtnClickedEvent()
    {
        EventAgregator.Post(this, new ReloadBtnClickedEvent());
    }

    private void SetGrabBtnVisibility(object sender, ChangeGrabBtnVisibilityEvent eventData)
    {
        _grabBtn.gameObject.SetActive(eventData.NewVisibilityValue);
    }

    private void SetFirstSlotAmmoInClipVolume(object sender, ChangeFirstSlotWeaponAmmoInClipVolumeEvent eventData)
    {
        _firstSlotWeaponAmmoInClip.SetText(eventData.newVolume.ToString());
    }

    private void SetFirstSlotAmmoInStockVolume(object sender, ChangeFirstSlotWeaponAmmoInStockVolumeEvent eventData)
    {
        _firstSlotWeaponAmmoInStock.SetText("/ " + eventData.newVolume.ToString());
    }

    private void SetSecondSlotAmmoInClipVolume(object sender, ChangeSecondSlotWeaponAmmoInClipVolumeEvent eventData)
    {
        _secondSlotWeaponAmmoInClip.SetText(eventData.newVolume.ToString());
    }

    private void SetSecondSlotAmmoInStockVolume(object sender, ChangeSecondSlotWeaponAmmoInStockVolumeEvent eventData)
    {
        _secondSlotWeaponAmmoInStock.SetText("/ " + eventData.newVolume.ToString());
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
            _shootBtn2.gameObject.SetActive(true);            
            _throwGrenadeBtn.gameObject.SetActive(false);
        }
        else if (eventData.slotWeaponType == SlotWeaponType.ThirdSlotWeapon)
        {
            _reloadBtn.gameObject.SetActive(false);
            _shootBtn.gameObject.SetActive(false);
            _shootBtn2.gameObject.SetActive(false);
            _throwGrenadeBtn.gameObject.SetActive(true);
        }
    }
    

}
