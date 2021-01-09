
using UnityEngine;

public class Ammo: MonoBehaviour
{
    [SerializeField]
    private AmmoType _type;

    [SerializeField]
    private int _volume = 10;

    public AmmoType GetAmmoType() 
    {
        return _type;
    }

    public int GetAmmoVolume()
    {
        return _volume;
    }
}

public enum AmmoType
{
    Pistol,
    Shotgun,
    Submachinegun,
    Rifle
}