using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TeamsManager _teamsManager;

    void Awake()
    {
        EventAgregator.Subscribe<SoldierKilledEvent>(CheckGameState);
        EventAgregator.Subscribe<PlayerKilledEvent>(GameOver);
    }

    private void CheckGameState(object sender, SoldierKilledEvent eventData)
    {
        Debug.Log("CheckGameState after kill");

        if (_teamsManager.IsAllBotTeamsKilled())
        {
            Debug.Log("YOU WIN");
        }
    }

    private void GameOver(object sender, PlayerKilledEvent eventData)
    {
        Debug.Log("GAME OVER");
    }

    // Start is called before the first frame update
    void Start()
    {
        _teamsManager = ServiceLocator.Resolved<TeamsManager>();

        TeamData comand1 = new TeamData();
        comand1.IsPlayerTeam = true;
        comand1.BotsNumber = 0;

        TeamData comand2 = new TeamData();
        comand2.BotsNumber = 1;
        comand2.IsPlayerTeam = false;

        _teamsManager.InitAndSpawnTeam(comand1);
        _teamsManager.InitAndSpawnTeam(comand2);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartAllAgainstAllGame()
    { 
        
    }
}
