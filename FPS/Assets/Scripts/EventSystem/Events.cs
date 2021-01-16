using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeWeaponEvent
{ }

public class JumpBtnClickedEvent
{ }

public class GrabBtnClickedEvent
{ }


public class ChangeGrabBtnVisibilityEvent
{
    public bool NewVisibilityValue;
}

public class ChangeIsWeaponSelectedUIVisibleElementsEvent
{
    public bool NewIsWeaponSelectedValue;

    public ChangeIsWeaponSelectedUIVisibleElementsEvent(bool _newIsWeaponSelectedValue)
    {
        NewIsWeaponSelectedValue = _newIsWeaponSelectedValue;
    }
}

public class ChangeFirstSlotWeaponIconEvent
{
    public Sprite NewIcon;

    public ChangeFirstSlotWeaponIconEvent(Sprite _newIcon)
    {
        NewIcon = _newIcon;
    }
}

public class ChangeSecondSlotWeaponIconEvent
{
    public Sprite NewIcon;

    public ChangeSecondSlotWeaponIconEvent(Sprite _newIcon)
    {
        NewIcon = _newIcon;
    }
}




public class ShootBtnClickedEvent
{}

public class FirstSlotWeaponBtnClickedEvent
{ }

public class SecondSlotWeaponBtnClickedEvent
{ }


public class ReloadBtnClickedEvent
{ }


public class ChangeFirstSlotWeaponAmmoInClipVolumeEvent
{
    public int newVolume;

    public ChangeFirstSlotWeaponAmmoInClipVolumeEvent(int _newVolume)
    {
        newVolume = _newVolume;
    }
}

public class ChangeFirstSlotWeaponAmmoInStockVolumeEvent
{
    public int newVolume;

    public ChangeFirstSlotWeaponAmmoInStockVolumeEvent(int _newVolume)
    {
        newVolume = _newVolume;
    }
}

public class ChangeSecondSlotWeaponAmmoInClipVolumeEvent
{
    public int newVolume;

    public ChangeSecondSlotWeaponAmmoInClipVolumeEvent(int _newVolume)
    {
        newVolume = _newVolume;
    }
}

public class ChangeSecondSlotWeaponAmmoInStockVolumeEvent
{
    public int newVolume;

    public ChangeSecondSlotWeaponAmmoInStockVolumeEvent(int _newVolume)
    {
        newVolume = _newVolume;
    }
}

public class ChangeHealthEvent
{
    public float newValue;

    public ChangeHealthEvent(float value)
    {
        newValue = value;
    }
}

public class PlayerKilledEvent
{
    public int newVolume;
}


public class SoldierKilledEvent
{
    public string killerName;
    public string killerWeaponName;
    public string victimName;

    public SoldierKilledEvent(string _killerName, string _killerWeaponName, string _victimName)
    {
        killerName = _killerName;
        killerWeaponName = _killerWeaponName;
        victimName = _victimName;
    }
}

public class MasterVolumeChangedEvent
{
}

public class MusicVolumeChangedEvent
{
}

public class SFXVolumeChangedEvent
{
}