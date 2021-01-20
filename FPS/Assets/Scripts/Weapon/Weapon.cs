using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string _weaponName;

    [SerializeField]
    protected float _damage = 10f;

    [SerializeField]
    protected GameObject _impactEffect;

    [SerializeField]
    protected float _destroyImpactTimeout = 2f;

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

    [SerializeField]
    protected SFXType _weaponSound;

    protected int _currentAmmo;
    


    public abstract void Shoot(GameObject _raycastSource, string shooterName);
    public abstract bool IsWeaponReady();

    public void Reload(int availableAmmoForWeaponClip)
    {
        _currentAmmo += availableAmmoForWeaponClip;
    }

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

    public bool IsOutOfAmmo()
    {
        return _currentAmmo <= 0;
    }

    public Sprite GetWeaponIcon()
    {
        return _weaponIcon;
    }

    public SFXType GetWeaponSound()
    {
        return _weaponSound;
    }

    public int MissingNumberOfAmmo()
    {
        return _ammoVolume - _currentAmmo;
    }

    public int GetCurrentAmmo()
    {
        return _currentAmmo;
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
    SecondSlotWeapon,
    ThirdSlotWeapon
}

public class AttackData
{
    public string shooterName;
    public float damage;
    public string weaponName;
    public Sprite weaponIcon;

    public AttackData(string _shooterName, float _damage, string _weaponName, Sprite _weaponIcon)
    {
        shooterName = _shooterName;
        damage = _damage;
        weaponName = _weaponName;
        weaponIcon = _weaponIcon;
    }
}
