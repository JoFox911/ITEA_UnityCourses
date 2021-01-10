public class BotWeaponManager
{
    public bool IsWeaponSelected;
    public bool IsNoAmmoOnAllWeapons;

    private SoldierWeaponManager _soldierWeaponManager;

    public void UpdateState()
    {
        if (IsWeaponSelected && IsCurrentWeaponOutOfAmmo())
        {
            if (IsCurrentWeaponAvailableAmmoExists())
            {
                _soldierWeaponManager.Reload(null);
            }
            else
            {
                _soldierWeaponManager.TryToSelectWeaponWithAvailableAmmo(out IsNoAmmoOnAllWeapons);
            }
        }
    }

    public void Init(SoldierWeaponManager soldierWeaponManager)
    {
        _soldierWeaponManager = soldierWeaponManager;
        _soldierWeaponManager.Initialize(null);
    }

    public void AddWeapon(Weapon weapon)
    {
        _soldierWeaponManager.AddWeapon(weapon, null);
        IsWeaponSelected = _soldierWeaponManager.IsWeaponSelected();
    }

    public void AddAmmo(Ammo ammo)
    {
        _soldierWeaponManager.AddAmmo(ammo, null);
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
