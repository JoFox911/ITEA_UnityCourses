using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string _weaponName;

    [SerializeField]
    protected float _damage = 10f;

    [SerializeField]
    protected float _range = 100f;

    [SerializeField]
    protected float _impactForce = 30f;

    [SerializeField]
    protected SlotWeaponType _slotType;

    [SerializeField]
    protected WeaponShootType _shootType;

    [SerializeField]
    protected float _reloadTime = 1f;

    [SerializeField]
    protected AmmoType _ammoType;

    [SerializeField]
    protected int _ammoVolume = 10;


    public abstract void Shoot(GameObject _raycastSource);
    public abstract void Reload(int enabledAmmoNumber, out int remaining);

    public SlotWeaponType GetWeaponSlotType()
    {
        return _slotType;
    }

    public AmmoType GetWeaponAmmoType()
    {
        return _ammoType;
    }

    public int GetWeaponAmmoVolume()
    {
        return _ammoVolume;
    }
}


// для гранат - Charge, когда автоматически перезаряжаем после броска?
public enum WeaponShootType
{
    Manual,
    Automatic,
    Charge,
}

public enum SlotWeaponType
{
    FirstSlotWeapon,
    SecondSlotWeapon
}
