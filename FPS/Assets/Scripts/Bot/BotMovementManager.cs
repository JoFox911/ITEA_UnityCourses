using UnityEngine;
using UnityEngine.AI;

public class BotMovementManager
{
    public bool IsMovementCompleted;
    private Vector3 _target;
    private NavMeshAgent _navMeshAgent;


    public void UpdateState()
    {
        if (_navMeshAgent != null && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance + 0.1f)
        {
            IsMovementCompleted = true;
        }
    }

    public void Init(NavMeshAgent navMeshAgent)
    {
        _navMeshAgent = navMeshAgent;
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        Debug.Log("MoveToTarget " + _target);
        IsMovementCompleted = false;
        _navMeshAgent.SetDestination(_target);
    }
}
