using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string _weaponName;

    [SerializeField]
    protected float _damage = 10f;

    [SerializeField]
    protected GameObject _impactEffect;

    [SerializeField]
    protected float _impactForce = 30f;

    [SerializeField]
    protected SlotWeaponType _slotType;

    [SerializeField]
    protected WeaponShootType _shootType;

    [SerializeField]
    protected AmmoType _ammoType;

    [SerializeField]
    protected int _ammoVolume = 10;

    [SerializeField]
    protected Sprite _weaponIcon;

    protected bool _isOutOfAmmo;


    public abstract void Shoot(GameObject _raycastSource, string shooterName);
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

    public bool GetIsOutOfAmmo()
    {
        return _isOutOfAmmo;
    }

    public Sprite GetWeaponIcon()
    {
        return _weaponIcon;
    }

    public abstract bool IsWeaponReady();
    public abstract int GetCurrentAmmo();
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
    SecondSlotWeapon,
    ThirdSlotWeapon
}

public class AttackData
{
    public float damage;
    public string shooterName;
    public string weaponName;
    public Sprite weaponIcon;

    public AttackData(float _damage, string _shooterName, string _weaponName, Sprite _weaponIcon)
    {
        damage = _damage;
        shooterName = _shooterName;
        weaponName = _weaponName;
        weaponIcon = _weaponIcon;
    }
}
