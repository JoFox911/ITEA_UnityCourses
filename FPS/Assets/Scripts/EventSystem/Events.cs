using UnityEngine;

public class JumpBtnClickedEvent
{ }

public class GrabBtnClickedEvent
{ }

public class PauseClickedEvent
{ }

public class UnpauseClickedEvent
{ }
public class GameFinishedEvent
{ }


public class ChangeGrabBtnVisibilityEvent
{
    public bool NewVisibilityValue;

    public ChangeGrabBtnVisibilityEvent(bool _newValue)
    {
        NewVisibilityValue = _newValue;
    }
}

public class ChangeSlotWeaponIconEvent
{
    public SlotWeaponType slotWeaponType;
    public Sprite weaponIcon;

    public ChangeSlotWeaponIconEvent(SlotWeaponType _slotWeaponType, Sprite _weaponIcon)
    {
        slotWeaponType = _slotWeaponType;
        weaponIcon = _weaponIcon;
    }
}



public class AttacklickedEvent
{ }

public class FirstSlotWeaponBtnClickedEvent
{ }

public class SecondSlotWeaponBtnClickedEvent
{ }

public class ThirdSlotWeaponBtnClickedEvent
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

public class ChangeThirdSlotWeaponAmmoVolumeEvent
{
    public int newVolume;

    public ChangeThirdSlotWeaponAmmoVolumeEvent(int _newVolume)
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

    public KillInfoData killerInfo;

    public SoldierKilledEvent(KillInfoData _killerInfo)
    {
        killerInfo = _killerInfo;
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

public class CurrentWeaponChangedEvent
{
    public SlotWeaponType slotWeaponType;

    public CurrentWeaponChangedEvent(SlotWeaponType _newType)
    {
        slotWeaponType = _newType;
    }
}



public class ChangeAliveEnemiesEvent
{
    public int enemiesNumber;

    public ChangeAliveEnemiesEvent(int _enemiesNumber)
    {
        enemiesNumber = _enemiesNumber;
    }
}

public class ChangeKilledEnemiesEvent
{
    public int enemiesNumber;

    public ChangeKilledEnemiesEvent(int _enemiesNumber)
    {
        enemiesNumber = _enemiesNumber;
    }
}