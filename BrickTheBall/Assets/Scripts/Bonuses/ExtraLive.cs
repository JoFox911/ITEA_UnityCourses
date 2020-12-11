using UnityEngine;

public class ExtraLive: Bonus
{
    protected override void ApplyEffect()
    {
        GameEvents.ExtraLiveCatchEvent();
    }
}
