using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}


public class ShootBtnClickedEvent
{}

public class FirstSlotWeaponBtnClickedEvent
{ }

public class SecondSlotWeaponBtnClickedEvent
{ }


public class ReloadBtnClickedEvent
{ }


public class ChangeAmmoInClipVolumeEvent
{
    public int newVolume;
}

public class ChangeAmmoInStockVolumeEvent
{
    public int newVolume;
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