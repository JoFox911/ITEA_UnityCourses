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



    [SerializeField]
    private Platform _platform;
    [SerializeField]
    private BricksManager _bricksManager;
    [SerializeField]
    private BallsManager _ballsManager;
    [SerializeField]
    private int _initialLives = 3;
    [SerializeField]
    private int _initialLevel = 0;

    private List<int[,]> LevelsData { get; set; } 
    public int Lives { get; set; }
    public bool IsGameStarted { get; set; }
    public bool IsGameDisabled { get; set; }
    public int CurrentLevel { get; set; }

    public int maxRows = 20;
    public int maxCols = 12;

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
 
    void Awake()
    {
        if (_instanceInner == null)
        {
            _instanceInner = this;
        }
    }
    #endregion

    void Start()
    {
        Lives = _initialLives; 
        LevelsData = LoadLevelsData();
        //todo в дфльнейшем сюда можно будет подгружать из сохраненки?
        PrepareLevel(_initialLevel);
        BallsManager.OnAllBallsWasted += OnAllBallsWasted;
        BricksManager.OnAllBricksDestroyed += OnAllBricksDestroyed;

        _tryAgainBtn.onClick.AddListener(RestartGame);
    }

    private void OnAllBallsWasted(BallsManager ballsManager)
    {
        Lives--;
        if (Lives < 1)
        {
            // game over
            //temp
            _gameOverScreen.SetActive(true);
        }
        else
        {
            IsGameStarted = false;
            ballsManager.ResetState();
        }
    }

    private void OnAllBricksDestroyed(BricksManager bricksManager)
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

    public void RestartGame()
    {
        //temp 
        _gameOverScreen.SetActive(false);
        IsGameStarted = false;
        Lives = _initialLives;
        PrepareLevel(0);
    }

    private void PrepareLevel(int level) {
        CurrentLevel = level;
        int[,] currentLevelMap = LevelsData[CurrentLevel];
        if (_bricksManager != null && _ballsManager != null && _platform != null)
        {
            _bricksManager.ResetState(currentLevelMap, maxRows, maxCols);
            _ballsManager.ResetState();
            _platform.ResetState();
        }
        else 
        {
            Debug.LogError("Brick manager/Ball manager/Platform not assigned");
        }
        
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
