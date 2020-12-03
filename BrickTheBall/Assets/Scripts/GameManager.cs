using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //todo temp 
    [SerializeField]
    private Button _tryAgainBtn;
    [SerializeField]
    private GameObject _gameOverScreen;
    [SerializeField]
    private GameObject _victoryScreen;
    //----------------------------------


    [SerializeField]
    private BricksManager _bricksManager;
    [SerializeField]
    private BallsManager _ballsManager;
    [SerializeField]
    private int _initialLives = 3;
    [SerializeField]
    private int _initialLevel = 0;

    
    public int Lives { get; set; }
    public bool IsGameStarted { get; set; }
    public bool IsGameDisabled { get; set; }
    public int CurrentLevel { get; set; }
    public int CurrentScore { get; set; }

    private int maxRows = 20;
    private int maxCols = 12;

    private List<int[,]> LevelsData { get; set; }

    #region Singleton
    private static GameManager _instanceInner;

    public static GameManager Instance
    {
        get
        {
            if (_instanceInner == null)
            {
                var go = new GameObject("GameManager");
                _instanceInner = go.AddComponent<GameManager>();
                DontDestroyOnLoad(_instanceInner.gameObject);
            }
            return _instanceInner;
        }
    }
    #endregion

    void Awake()
    {
        if (_instanceInner == null)
        {
            _instanceInner = this;
        }
    }

    void Start()
    {
        SetLives(_initialLives);
        LevelsData = LoadLevelsData();
        //todo в дфльнейшем сюда можно будет подгружать из сохраненки?
        PrepareLevel(_initialLevel);
        GameEvents.OnAllBallsWasted += OnAllBallsWasted;
        GameEvents.OnAllBricksDestroyed += OnAllBricksDestroyed;

        _tryAgainBtn.onClick.AddListener(RestartGame);
    }

    void OnDestroy()
    {
        GameEvents.OnAllBallsWasted -= OnAllBallsWasted;
        GameEvents.OnAllBricksDestroyed -= OnAllBricksDestroyed;
    }

    public void RestartGame()
    {
        //temp 
        _gameOverScreen.SetActive(false);
        IsGameStarted = false;
        SetLives(_initialLives);
        PrepareLevel(0);
    }

    private void OnAllBallsWasted()
    {
        SetLives(Lives - 1);
        if (Lives < 1)
        {
            // game over
            //temp
            _gameOverScreen.SetActive(true);
        }
        else
        {
            IsGameStarted = false;
            _ballsManager.ResetState();
        }
    }

    private void OnAllBricksDestroyed()
    {
       
        if (CurrentLevel + 1 < LevelsData.Count)
        {
            IsGameStarted = false;
            PrepareLevel(CurrentLevel + 1);
        }
        else
        {
            // temp
            IsGameStarted = false;
            IsGameDisabled = true;
            _victoryScreen.SetActive(true);
            _ballsManager.ResetState();
        }
    }

    

    private void PrepareLevel(int level) {
        GameEvents.ResetGameStateEvent();
        SetLevel(level);
        int[,] currentLevelMap = LevelsData[CurrentLevel];
        if (_bricksManager != null)
        { 
            _bricksManager.GenerateLevelBricks(currentLevelMap, maxRows, maxCols);
        }
        else 
        {
            Debug.LogError("BrickManager not assigned");
        }
        
    }

    private void SetLives(int lives)
    {
        Lives = lives;
        GameEvents.ChangeLivesEvent(Lives);
    }

    private void SetLevel(int level)
    {
        CurrentLevel = level;
        GameEvents.ChangeLevelEvent(CurrentLevel + 1);
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
