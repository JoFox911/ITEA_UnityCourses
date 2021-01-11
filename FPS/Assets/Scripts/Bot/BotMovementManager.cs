using UnityEngine;
using UnityEngine.AI;

public class BotMovementManager
{
    public bool IsMovementCompleted;
    public bool IsRun;

    private float _folowingDistance = 0f;
    private Vector3 _target;
    private NavMeshAgent _navMeshAgent;

    public void UpdateState()
    {
        if (_navMeshAgent != null)
        {
            if ((_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance + 0.1f) ||
                (Vector3.Distance(_target, _navMeshAgent.nextPosition) < _folowingDistance))
            {
                IsMovementCompleted = true;
                _navMeshAgent.isStopped = true;
            }
        }       
    }

    public void Init(NavMeshAgent navMeshAgent)
    {
        _navMeshAgent = navMeshAgent;
        SetIsRunState(false);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        FolowTarget(0f);
    }

    public void FolowTarget(float distance)
    {
        _folowingDistance = distance;
        if (Vector3.Distance(_target, _navMeshAgent.nextPosition) > _folowingDistance)
        {
            //Debug.Log("MoveToTarget " + _target);
            _navMeshAgent.isStopped = false;
            IsMovementCompleted = false;
            _navMeshAgent.SetDestination(_target);
        }        
    }

    public void LookAtTarget()
    {
        _navMeshAgent.transform.LookAt(_target);
    }

    public float DistanceToTarget()
    {
        return _navMeshAgent.remainingDistance;
    }

    public void SetIsRunState(bool value)
    { 
        IsRun = value;
        if (IsRun)
        {
            _navMeshAgent.speed = 8;
        }
        else
        {
            _navMeshAgent.speed = 4;
        }
    }
}
