using UnityEngine;

public class ExpandOrShrinkPlatform : Bonus
{
    [SerializeField]
    private float _newWidthCoef = 2;
    protected override void ApplyEffect()
    {
        GameEvents.ChangePlatformWithCatchEvent(_newWidthCoef);
    }
}
