using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton
    private static BricksManager _instanceInner;

    public static BricksManager Instance
    {
        get
        {
            if (_instanceInner == null)
            {
                var go = new GameObject("BricksManager");
                _instanceInner = go.AddComponent<BricksManager>();
                DontDestroyOnLoad(_instanceInner.gameObject);
            }
            return _instanceInner;
        }
    }
    #endregion

    [SerializeField]
    private List<BrickTypeData> _brickTypesData;

    [SerializeField]
    private Brick _brickPrefab;

    [SerializeField]
    private Vector3 _initialBricksSpawnPossition;



    private GameObject _bricksContainer;

    private List<Brick> _remainingBricks;

    public static event Action<BricksManager> OnAllBricksDestroyed;

    private  


    void Awake()
    {
        _bricksContainer = new GameObject("BricksContainer");
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    public void ResetState(int[,] levelMap, int maxRows, int maxCols)
    {
        if (_remainingBricks != null) 
        {
            foreach (var brick in _remainingBricks.ToList())
            {
                Destroy(brick.gameObject);
            }
        }
        GenerateLevelBricks(levelMap, maxRows, maxCols);
    }

    private void OnBrickDestruction(Brick brick)
    {
        _remainingBricks.Remove(brick);
        if (_remainingBricks.Count <= 0)
        {
            OnAllBricksDestroyed?.Invoke(this);
        }
    }

    private void GenerateLevelBricks(int[,] levelMap, int maxRows, int maxCols)
    {
        _remainingBricks = new List<Brick>();
        float brickSpawnPositionX = _initialBricksSpawnPossition.x;
        float brickSpawnPositionY = _initialBricksSpawnPossition.y;
        float brickSpawnPositionZ = _initialBricksSpawnPossition.z;

        for (var row = 0; row < maxRows; row++)
        {
            brickSpawnPositionX = _initialBricksSpawnPossition.x;

            for (var col = 0; col < maxCols; col++)
            {
                if (levelMap[row, col] != 0)
                {
                    var brickType = (BrickType)levelMap[row, col];
                    var brickTypeData = BrickDataByType(brickType);
                    var newBrick = Instantiate<Brick>(_brickPrefab, 
                                                        new Vector3(brickSpawnPositionX, brickSpawnPositionY, brickSpawnPositionZ), Quaternion.identity);
                    newBrick.Init(_bricksContainer.transform, brickTypeData);
                    _remainingBricks.Add(newBrick);

                }
                // todo надо добавлять ширину блока. могу попробовать через спрайт?
                brickSpawnPositionX += 0.8f;
            }
            brickSpawnPositionY -= 0.4f;
        }
    }

    private BrickTypeData BrickDataByType(BrickType brickType)
    {
        BrickTypeData brickTypeData = null;
        if (_brickTypesData != null)
        {
            brickTypeData = _brickTypesData.FirstOrDefault(data => data.Type == brickType);
            if (brickTypeData == null)
            {
                Debug.LogError("THERE IS NO BRICK TYPE DATA FOR BRICK TYPE: " + brickType);
            }
        }
        
        return brickTypeData;
    }

    
}


[Serializable]
public class BrickTypeData
{
    public BrickType Type;
    public int Hitpoints;
    public List<Sprite> Sprites;
}

// каждому типу блока соответствует значение (1, 2, 3..) 
// которое указазывается на матрице уровня какой блок отрисовывать
public enum BrickType
{
    Easy = 1,
    Medium = 2,
    Hard = 3,
    Immortal = 4,
    Boss1 = 5
}
