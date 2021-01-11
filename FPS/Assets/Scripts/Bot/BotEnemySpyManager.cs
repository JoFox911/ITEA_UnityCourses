using UnityEngine;

public class BotEnemySpyManager
{
    public bool IsAnyEnemySpyed => CurrentTarget != null;
    public GameObject CurrentTarget;

    private CheckEnemyHelper _checkEnemyHelper;

    public void Init(CheckEnemyHelper checkEnemyHelper)
    {
        _checkEnemyHelper = checkEnemyHelper;
    }

    public void UpdateState()
    {
        if (_checkEnemyHelper != null && _checkEnemyHelper.CheckEnemy())
        {
            CurrentTarget = _checkEnemyHelper.GetTarget();
        }
        else
        {
            CurrentTarget = null;
        }
    }
}