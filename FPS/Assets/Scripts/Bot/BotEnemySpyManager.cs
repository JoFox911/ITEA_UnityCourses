using UnityEngine;

public class BotEnemySpyManager
{
    public bool IsAnyEnemySpyed => CurrentTarget != null;
    public Transform CurrentTarget;
}