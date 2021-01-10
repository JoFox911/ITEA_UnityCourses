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
    private int _ammoInClip = int.MinValue;
    private int _ammoInStock = int.MinValue;

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
        Debug.Log("UpdateWeaponUI");


        var newIsWeaponSelectedValue = _soldierWeaponManager.IsWeaponSelected();
        if (_isWeaponSelected != newIsWeaponSelectedValue)
        {
            _isWeaponSelected = newIsWeaponSelectedValue;
            var ChangeIsWeaponSelectedUIVisibleElementsEvent = new ChangeIsWeaponSelectedUIVisibleElementsEvent();
            ChangeIsWeaponSelectedUIVisibleElementsEvent.NewIsWeaponSelectedValue = newIsWeaponSelectedValue;
            EventAgregator.Post(this, ChangeIsWeaponSelectedUIVisibleElementsEvent);
        }

        var weapon = (ShootingWeapon)_soldierWeaponManager.GetCurrentWeapon();
        if (weapon != null)
        {
            var newAmmoInClipValue = weapon.GetCurrentAmmo();
            if (_ammoInClip != newAmmoInClipValue)
            {
                _ammoInClip = newAmmoInClipValue;
                var ChangeAmmoInClipVolumeEvent = new ChangeAmmoInClipVolumeEvent();
                ChangeAmmoInClipVolumeEvent.newVolume = newAmmoInClipValue;
                EventAgregator.Post(this, ChangeAmmoInClipVolumeEvent);
            }
        }
       


        var newAmmoInStockValue = _soldierWeaponManager.GetCurrentWeaponAvailableAmmo();
        if (_ammoInStock != newAmmoInStockValue)
        {
            _ammoInStock = newAmmoInStockValue;
            var ChangeAmmoInStockVolumeEvent = new ChangeAmmoInStockVolumeEvent();
            ChangeAmmoInStockVolumeEvent.newVolume = newAmmoInStockValue;
            EventAgregator.Post(this, ChangeAmmoInStockVolumeEvent);
        }
    }

}


