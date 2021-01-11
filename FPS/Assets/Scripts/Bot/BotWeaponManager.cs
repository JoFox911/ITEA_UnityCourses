using UnityEngine;

public class BotWeaponManager
{
    public bool IsReload;
    public bool IsWeaponSelected;
    public bool IsNoAmmoOnAllWeapons;

    private SoldierWeaponManager _soldierWeaponManager;

    public void UpdateState()
    {
        IsReload = _soldierWeaponManager.GetReloadState();
        IsWeaponSelected = _soldierWeaponManager.IsWeaponSelected();

        if (IsWeaponSelected && !IsReload && IsCurrentWeaponOutOfAmmo())
        {
            if (IsCurrentWeaponAvailableAmmoExists())
            {
                IsNoAmmoOnAllWeapons = false;
                _soldierWeaponManager.Reload(null);
            }
            else
            {
                _soldierWeaponManager.TryToSelectWeaponWithAvailableAmmo(out IsNoAmmoOnAllWeapons);
            }
        }
        //Debug.Log("UpdateState weapon r s a " + IsReload + " " + IsWeaponSelected +" " + IsNoAmmoOnAllWeapons);
    }

    public void Init(SoldierWeaponManager soldierWeaponManager, GameObject raycastSource)
    {
        _soldierWeaponManager = soldierWeaponManager;
        _soldierWeaponManager.Initialize(raycastSource);
    }

    public void AddWeapon(Weapon weapon)
    {
        IsNoAmmoOnAllWeapons = false;
        _soldierWeaponManager.AddWeapon(weapon, null);
        UpdateState();
    }

    public void AddAmmo(Ammo ammo)
    {
        IsNoAmmoOnAllWeapons = false;
        _soldierWeaponManager.AddAmmo(ammo, null);
        UpdateState();
    }

    public void Shoot()
    {
        _soldierWeaponManager.Shoot(null);
        UpdateState();
    }

    public bool IsCurrentWeaponOutOfAmmo()
    {
        return _soldierWeaponManager.GetCurrentWeapon().GetIsOutOfAmmo();
    }

    public bool IsCurrentWeaponAvailableAmmoExists()
    {
        return _soldierWeaponManager.IsCurrentWeaponAmmoAvailable();
    }


}
