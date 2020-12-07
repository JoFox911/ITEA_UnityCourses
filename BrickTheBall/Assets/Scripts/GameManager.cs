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

        _gameController = new GameController();

        GameEvents.OnAllBallsWasted += OnAllBallsWasted;
        GameEvents.OnGameRestart += RestartGame;
        GameEvents.OnAllBricksDestroyed += OnAllBricksDestroyed;
    }

    void Start()
    {
        GameEvents.ResetGameStateEvent();
        _gameController.SetLives(_initialLives);

        // todo в дфльнейшем сюда можно будет подгружать из сохраненки?
        _gameController.SetLevel(_initialLevel);
    }

    void OnDestroy()
    {
        GameEvents.OnAllBallsWasted -= OnAllBallsWasted;
        GameEvents.OnAllBricksDestroyed -= OnAllBricksDestroyed;
        GameEvents.OnAllBricksDestroyed -= OnAllBricksDestroyed;
    }

    public void RestartGame()
    {
        GameEvents.ResetGameStateEvent();
        _gameController.SetIsGameStarted(false);
        _gameController.SetLives(_initialLives);
        _gameController.SetLevel(0);
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
            _gameController.SetIsGameDisabled(true);
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

    public static bool IsGameDisabled()
    {
        return _instance._gameController.IsGameDisabled;
    }

}

public class GameController
{
    public bool IsGameStarted = false;
    public bool IsGameDisabled = false;

    public int CurrentLevel = 0;
    public int CurrentLives = 3;

    public List<int[,]> LevelsData;
    public int maxRows = 20;
    public int maxCols = 12;

    public GameController()
    {
        LevelsData = LoadLevelsData();
    }

    public void SetIsGameStarted(bool isStarted)
    {
        IsGameStarted = isStarted;
    }

    public void SetIsGameDisabled(bool isDisabled)
    {
        IsGameDisabled = isDisabled;
    }

    public void SetLives(int lives)
    {
        CurrentLives = lives;
        GameEvents.ChangeLivesEvent(CurrentLives);

        if (CurrentLives <= 0)
        {
            GameEvents.GameOverEvent();
        }
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        GameEvents.ChangeLevelEvent(CurrentLevel);  
    }

    public void IncreaseLevel()
    {
        SetLevel(CurrentLevel + 1);
    }

    public void ReduceLive()
    {
        SetLevel(CurrentLives - 1);
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
