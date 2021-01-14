using System.Collections.Generic;
using UnityEngine;

public class TeamsManager: MonoBehaviour
{
    [SerializeField]
    private List<Transform> _spawnPossitions;

    [SerializeField]
    private GameObject _soldiersContainer;

    [SerializeField]
    private GameObject _botPrefab;

    [SerializeField]
    private GameObject _player;

    private List<TeamData> _teamsList;


    //private List<GameObject> _soldiersList;

    private int _fistEmptySpawnIndex = 0;

    void Awake()
    {
        _teamsList = new List<TeamData>();
        ServiceLocator.Register<TeamsManager>(this);
    }

    public List<Transform> GetAllSpawns()
    {
        return _spawnPossitions;
    }

    public void InitAndSpawnTeam(TeamData command)
    {
        var spawnPossiton = _spawnPossitions[_fistEmptySpawnIndex];
        SpawnCommandOnPosition(command, spawnPossiton.position);
        _fistEmptySpawnIndex++;
    }

    public void SpawnCommandOnPosition(TeamData team, Vector3 pos)
    {
        team.Name = TeamNamesGenerator.GenerateRandomName();

        if (team.IsPlayerTeam)
        {
            _player.transform.position = pos;
            var soldierComponent = _player.GetComponent<Soldier>();
            if (soldierComponent != null)
            {
                team.memdersList.Add(soldierComponent);
            }
        }

        for (var i = 0; i < team.BotsNumber; i++)
        {
            var member = InstantiateBot(pos);

            var soldierComponent = member.GetComponent<Soldier>();
            if (soldierComponent != null)
            {
                team.memdersList.Add(soldierComponent);
            }
        }

        _teamsList.Add(team);
    }

    public GameObject InstantiateBot(Vector3 pos)
    {
        var soldier = Instantiate(_botPrefab, pos, Quaternion.identity);
        soldier.transform.SetParent(_soldiersContainer.transform);
        return soldier;
    }

    public bool IsAllBotTeamsKilled()
    {
        foreach(var team in _teamsList)
        {
            if (!team.IsPlayerTeam)
            { 
                foreach(var member in team.memdersList)
                {
                    if (member.IsAlive())
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}
