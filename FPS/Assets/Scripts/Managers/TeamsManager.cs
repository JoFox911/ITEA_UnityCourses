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


    private int _fistEmptySpawnIndex = 0;

    void Awake()
    {
        _teamsList = new List<TeamData>();
        ServiceLocator.Register<TeamsManager>(this);
    }

    void OnDestroy()
    {
        ServiceLocator.Unregister<TeamsManager>();
    }

    public List<Transform> GetAllSpawns()
    {
        return _spawnPossitions;
    }

    public void InitAndSpawnTeam(TeamData team)
    {
        var spawnPossiton = _spawnPossitions[_fistEmptySpawnIndex];
        SpawnCommandOnPosition(team, spawnPossiton.position);
        _fistEmptySpawnIndex++;
    }

    public void SpawnCommandOnPosition(TeamData team, Vector3 pos)
    {
        team.Name = TeamNamesGenerator.GenerateRandomName();

        if (team.IsPlayerTeam)
        {
            _player.SetActive(false);
            _player.transform.position = pos;
            _player.SetActive(true);
            _player.tag = team.Tag;
            var soldierComponent = _player.GetComponent<Soldier>();
            if (soldierComponent != null)
            {
                team.memdersList.Add(soldierComponent);
            }
        }

        for (var i = 0; i < team.BotsNumber; i++)
        {
            var member = InstantiateBot(pos);
            member.gameObject.tag = team.Tag;

            var soldierComponent = member.GetComponent<Soldier>();
            if (soldierComponent != null)
            {
                team.memdersList.Add(soldierComponent);
            }

            var checkEnemyComponent = member.GetComponent<CheckEnemyHelper>();
            if(checkEnemyComponent != null)
            {
                checkEnemyComponent.SetTeamTag(team.Tag);
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

    public int GetAliveEnemiesNumber()
    {
        var number = 0;
        foreach (var team in _teamsList)
        {
            if (!team.IsPlayerTeam)
            {
                foreach (var member in team.memdersList)
                {
                    if (member.IsAlive())
                    {
                        number++;
                    }
                }
            }
        }
        return number;
    }
}

public class TeamData
{
    public string Name;
    public string Tag;
    public bool IsPlayerTeam;
    public int BotsNumber;
    public List<Soldier> memdersList = new List<Soldier>();
};
