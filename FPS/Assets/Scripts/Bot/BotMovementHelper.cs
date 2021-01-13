using UnityEngine;

public class BotMovementHelper
{
    private BotMovementManager _movementManager;
    private Vector3 _target;

    public bool IsMovementCompleted;

    public void Init(BotMovementManager movementManager)
    {
        _movementManager = movementManager;
    }

    public void UpdateState()
    {
        if (_movementManager.IsNearDestinationPosition())
        {
            IsMovementCompleted = true;
            _movementManager.StopMovement();
        }
        else 
        {
            IsMovementCompleted = false;
        }      
    }

    

    public void MoveToTarget(Vector3 target)
    {
        _target = target;
        _movementManager.SetTarget(target, 0);
    }

    public void FolowTarget(Vector3 target, float distance)
    {
        _target = target;
        _movementManager.SetTarget(target, distance);      
    }

    public void LookAtTarget()
    {
        _movementManager.LookAtTarget(_target);
    }



    public Vector3 GetCurrentPossition()
    {
        return _movementManager.GetCurrentPossition();
    }
}
