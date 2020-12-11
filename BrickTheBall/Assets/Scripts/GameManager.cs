using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _initialLives = 3;
    [SerializeField]
    private int _initialLevel = 0;

    private GameController _gameController;

    private static GameManager _instance;

    void Awake()
    {
        _instance = this;

        _gameController = new GameController(_initialLives, _initialLevel);

        GameEvents.OnNewGameClickedEvent += StartNewGame;
        GameEvents.OnGamePaused += PauseGame;
        GameEvents.OnGameUnpaused += UnpauseGame;
        GameEvents.OnAllBallsWasted += OnAllBallsWasted;
        GameEvents.OnAllBricksDestroyed += OnAllBricksDestroyed;
    }

    void Start()
    {
        UnpauseGame();
        GameEvents.ResetGameStateEvent();
        _gameController.SetIsGameStarted(false);
        _gameController.SetLives(_initialLives);
        _gameController.SetLevel(_gameController.CurrentLevel);
    }

    void OnDestroy()
    {
        //GameEvents.OnAllBallsWasted -= OnAllBallsWasted;
        //GameEvents.OnAllBricksDestroyed -= OnAllBricksDestroyed;
        //GameEvents.OnAllBricksDestroyed -= OnAllBricksDestroyed;
    }


    public void StartNewGame()
    {
        UnpauseGame();
        GameEvents.ResetGameStateEvent();
        _gameController.SetIsGameStarted(false);
        _gameController.SetLives(_initialLives);
        _gameController.SetLevel(_initialLevel);
    }

    private void PauseGame()
    {
        _gameController.SetIsGamePaused(true);
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        _gameController.SetIsGamePaused(false);
        Time.timeScale = 1;
    }

    private void OnAllBallsWasted()
    {
        _gameController.SetIsGameStarted(false);
        _gameController.ReduceLive();
    }

    private void OnAllBricksDestroyed()
    {
        _gameController.SetIsGameStarted(false);

        if (_gameController.IsCurrentLevelTheLast())
        {
            GameEvents.GameFinishedEvent();
        }
        else
        {
            GameEvents.ResetGameStateEvent();
            _gameController.IncreaseLevel();
        }
    }

    public static (int[,] map, int rowsNumber, int colsNumber) GetLevelMap(int level)
    {
        return _instance._gameController.GetLevelMap(level);
    }

    public static void StartPlaying()
    {
        _instance._gameController.SetIsGameStarted(true);
    }

    public static bool IsGameStarted()
    {
        return _instance._gameController.IsGameStarted;
    }

    public static bool IsGamePaused()
    {
        return _instance._gameController.IsGamePaused;
    }
}

public class GameController
{
    public bool IsGameStarted = false;
    public bool IsGamePaused = false;

    public int CurrentLevel = 0;
    public int CurrentLives = 3;

    public int InitialLevel = 0;
    public int InitialLives = 3;

    public List<int[,]> LevelsData;
    public int maxRows = 20;
    public int maxCols = 10;

    public GameController(int initialLives, int initialLevel)
    {
        LevelsData = LoadLevelsData();
        InitialLives = initialLives;
        InitialLevel = initialLevel;

        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", InitialLevel);
        CurrentLives = PlayerPrefs.GetInt("CurrentLives", InitialLives);
    }

    public void SetIsGameStarted(bool isStarted)
    {
        IsGameStarted = isStarted;
    }

    public void SetIsGamePaused(bool isPaused)
    {
        IsGamePaused = isPaused;
    }

    public void SetLives(int lives)
    {
        CurrentLives = lives;
        GameEvents.ChangeLivesEvent(CurrentLives);

        if (CurrentLives <= 0)
        {
            GameEvents.GameOverEvent();
        }
        PlayerPrefs.SetInt("CurrentLives", CurrentLives);
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        AudioManager.PlaySFX(SFXType.LevelStart);
        GameEvents.ChangeLevelEvent(CurrentLevel);
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
    }

    public void IncreaseLevel()
    {
        SetLevel(CurrentLevel + 1);
    }

    public void ReduceLive()
    {
        SetLives(CurrentLives - 1);
    }

    public bool IsCurrentLevelTheLast()
    {
        return CurrentLevel + 1 == LevelsData.Count;
    }

    public bool IsAllLivesWasted()
    {
        return CurrentLives <= 0;
    }

    public (int[,] map, int rowsNumber, int colsNumber) GetLevelMap(int level)
    {
        return (map: LevelsData[level], rowsNumber: maxRows, colsNumber: maxCols);
    }

    private List<int[,]> LoadLevelsData()
    {
        List<int[,]> levelsData = new List<int[,]>();
        TextAsset text = Resources.Load("Levels") as TextAsset;
        if (text != null)
        {
            string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int[,] proccedLevel = new int[maxRows, maxCols];
            int levelRow = 0;

            foreach (var line in rows)
            {
                if (line.StartsWith("#"))
                {
                    // this is comment line - ignore it
                    continue;
                }
                if (line.IndexOf("=====") == -1)
                {
                    // this is line with level discriptions
                    string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (var col = 0; col < bricks.Length; col++)
                    {
                        proccedLevel[levelRow, col] = int.Parse(bricks[col]);
                    }
                    levelRow++;
                }
                else
                {
                    // end of level
                    levelRow = 0;
                    levelsData.Add(proccedLevel);
                    proccedLevel = new int[maxRows, maxCols];
                }
            }
        }
        else
        {
            Debug.LogError("THERE IS NO LEVELS FILE");
        }
        return levelsData;
    }
}
