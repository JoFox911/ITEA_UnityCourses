using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BricksManager _bricksManager;

    private List<int[,]> LevelsData { get; set; }

    public bool isGameStarted { get; set; }
    public int currentLevel { get; set; }
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
        LevelsData = LoadLevelsData();
        //todo в дфльнейшем сюда можно будет подгружать из сохраненки?
        PrepareLevel(currentLevel);
    }

    private void PrepareLevel(int level) {
        currentLevel = level;
        int[,] currentLevelMap = LevelsData[currentLevel];
        if (_bricksManager != null)
        {
            _bricksManager.GenerateBricks(currentLevelMap, maxRows, maxCols);
        }
        else 
        {
            Debug.LogError("Brick manager not assigned");
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
                        Debug.Log("Test" + levelRow + col);
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
