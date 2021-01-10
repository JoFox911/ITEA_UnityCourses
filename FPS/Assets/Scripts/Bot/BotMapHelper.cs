using System.Collections.Generic;
using UnityEngine;

public class BotMapHelper
{
    public List<Transform> EnemySearchingPoints = new List<Transform>();
    public List<Transform> ItemSpawnPoints = new List<Transform>();

    public void Init(List<Transform> enemySearchingPoints, List<Transform> itemSpawnPoints)
    {
        EnemySearchingPoints = enemySearchingPoints;
        ItemSpawnPoints = itemSpawnPoints;
    }
}