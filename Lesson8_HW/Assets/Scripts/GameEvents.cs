using System;

public static class GameEvents
{
    public static event Action OnButtonPressed;

    public static event Action OnTimeOut;

    public static event Action<int> OnChangeSecondsResidue;

    public static void ButtonPressedEvent()
    {
        OnButtonPressed?.Invoke();
    }

    public static void TimeOutEvent()
    {
        OnTimeOut?.Invoke();
    }

    public static void ChangeSecondsResidueEvent(int seconds)
    {
        OnChangeSecondsResidue?.Invoke(seconds);
    }

}
