using UnityEngine;

public class MultiBall : Bonus
{
    [SerializeField]
    private int _generatedBallsNumber = 2;

    protected override void ApplyEffect()
    {
        GameEvents.MultiBallCatchEvent(_generatedBallsNumber);
    }
}
