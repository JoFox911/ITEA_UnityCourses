using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _endGameDelay = 2f;
    
    private TeamsManager _teamsManager;
    private GameController _gameController;



    void Awake()
    {
        UnpauseGame();

        _gameController = new GameController();

        GameTypes gameType = GameTypes.DeathMatch;

        if (PlayerPrefs.HasKey("GameType"))
        {
            gameType = PlayerPrefs.GetString("GameType").ToEnum<GameTypes>();
        }

        _gameController.SetGameType(gameType);




        EventAgregator.Subscribe<PlayerKilledEvent>(GameOver);
        EventAgregator.Subscribe<PauseClickedEvent>(PauseGame);
        EventAgregator.Subscribe<UnpauseClickedEvent>(UnpauseGame);

        ServiceLocator.Register<GameController>(_gameController);
    }

    void Update()
    {
        _gameController.SetAliveEnemies(_teamsManager.GetAliveEnemiesNumber());
        if (_gameController.AliveEnemiesNumber < 1)
        {
            StartCoroutine(StopGameWithDelay());
        }
    }

    void OnDestroy()
    {
        EventAgregator.Unsubscribe<PlayerKilledEvent>(GameOver);
        EventAgregator.Unsubscribe<PauseClickedEvent>(PauseGame);
        EventAgregator.Unsubscribe<UnpauseClickedEvent>(UnpauseGame);

        ServiceLocator.Unregister<GameController>();
    }

    private IEnumerator StopGameWithDelay()
    {
        yield return new WaitForSeconds(_endGameDelay);
        PauseGame();
        EventAgregator.Post(this, new GameFinishedEvent());
    }

    private void PauseGame(object sender, PauseClickedEvent eventData)
    {
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnpauseGame(object sender, UnpauseClickedEvent eventData)
    {
        UnpauseGame();
    }
    private void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    private void GameOver(object sender, PlayerKilledEvent eventData)
    {
        StartCoroutine(StopGameWithDelay());
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadTestMatch();
        //if (_gameController.gameType == GameTypes.TeamMatch)
        //{
        //    LoadTeamMatch();
        //}
        //else
        //{
        //    LoadDeathMatch();
        //}
    }

    private void LoadTeamMatch()
    {
        _teamsManager = ServiceLocator.Resolved<TeamsManager>();

        TeamData comand1 = new TeamData();
        comand1.Tag = "TeamA";
        comand1.IsPlayerTeam = true;
        comand1.BotsNumber = 1;

        TeamData comand2 = new TeamData();
        comand2.BotsNumber = 2;
        comand2.Tag = "TeamB";
        comand2.IsPlayerTeam = false;

        _teamsManager.InitAndSpawnTeam(comand1);
        _teamsManager.InitAndSpawnTeam(comand2);
    }

    private void LoadDeathMatch()
    {
        _teamsManager = ServiceLocator.Resolved<TeamsManager>();

        TeamData comand1 = new TeamData();
        comand1.Tag = "TeamA";
        comand1.IsPlayerTeam = true;
        comand1.BotsNumber = 0;

        TeamData comand2 = new TeamData();
        comand2.Tag = "TeamB";
        comand2.BotsNumber = 1;
        comand2.IsPlayerTeam = false;

        TeamData comand3 = new TeamData();
        comand3.Tag = "TeamC";
        comand3.BotsNumber = 1;
        comand3.IsPlayerTeam = false;

        TeamData comand4 = new TeamData();
        comand4.Tag = "TeamD";
        comand4.BotsNumber = 1;
        comand4.IsPlayerTeam = false;


        _teamsManager.InitAndSpawnTeam(comand1);
        _teamsManager.InitAndSpawnTeam(comand2);
        _teamsManager.InitAndSpawnTeam(comand3);
        _teamsManager.InitAndSpawnTeam(comand4);
    }

    private void LoadTestMatch()
    {
        _teamsManager = ServiceLocator.Resolved<TeamsManager>();

        TeamData comand1 = new TeamData();
        comand1.Tag = "TeamA";
        comand1.IsPlayerTeam = true;
        comand1.BotsNumber = 0;

        TeamData comand2 = new TeamData();
        comand2.BotsNumber = 1;
        comand2.Tag = "TeamB";
        comand2.IsPlayerTeam = false;

        _teamsManager.InitAndSpawnTeam(comand1);
        _teamsManager.InitAndSpawnTeam(comand2);
    }

}

public class GameController
{
    public int KilledEnemiesNumber = 0;
    public int AliveEnemiesNumber = 0;

    public GameTypes gameType;

    public void SetAliveEnemies(int number)
    {
        if (AliveEnemiesNumber != number)
        {
            AliveEnemiesNumber = number;
            EventAgregator.Post(this, new ChangeAliveEnemiesEvent(AliveEnemiesNumber));
        }
    }

    public void AddKilledEnemy()
    {
        KilledEnemiesNumber ++;
        EventAgregator.Post(this, new ChangeKilledEnemiesEvent(KilledEnemiesNumber));
    }

    public void SetGameType(GameTypes type)
    {
        gameType = type;
    }
}

public enum GameTypes
{
    TeamMatch,
    DeathMatch
}