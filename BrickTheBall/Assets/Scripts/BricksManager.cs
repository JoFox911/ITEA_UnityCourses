using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    [SerializeField]
    private List<Brick> _availableBricks;
    [SerializeField]
    private Vector3 _initialBricksSpawnPossition;

    private GameObject _bricksContainer;
    private List<Brick> _remainingBricks;

    void Awake()
    {
        _bricksContainer = new GameObject("BricksContainer");
        GameEvents.OnBrickDestructed += OnBrickDestruction;
        GameEvents.OnResetGameState += ResetState;
    }

    void OnDestroy()
    {
        GameEvents.OnBrickDestructed -= OnBrickDestruction;
        GameEvents.OnResetGameState -= ResetState;
    }

    public void GenerateLevelBricks(int[,] levelMap, int maxRows, int maxCols)
    {
        _remainingBricks = new List<Brick>();

        float brickSpawnPositionX, brickSpawnPositionY;
        brickSpawnPositionY = _initialBricksSpawnPossition.y;

        for (var row = 0; row < maxRows; row++)
        {
            brickSpawnPositionX = _initialBricksSpawnPossition.x;

            for (var col = 0; col < maxCols; col++)
            {
                if (levelMap[row, col] != 0)
                {
                    var brickType = (BrickType)levelMap[row, col];
                    var brickPrefab = BrickPrefabByType(brickType);
                    var newBrick = Instantiate(brickPrefab, new Vector3(brickSpawnPositionX, brickSpawnPositionY, _initialBricksSpawnPossition.z), Quaternion.identity);
                    newBrick.Init(_bricksContainer.transform);
                    _remainingBricks.Add(newBrick);
                }
                // todo надо добавлять ширину блока. могу попробовать через спрайт?
                brickSpawnPositionX += 0.8f;
            }
            brickSpawnPositionY -= 0.4f;
        }
    }

    private void OnBrickDestruction(Brick brick)
    {
        _remainingBricks.Remove(brick);
        if (_remainingBricks.Count <= 0)
        {
            GameEvents.AllBricksDestroyedEvent();
        }
    }

    private void ResetState()
    {
        if (_remainingBricks != null)
        {
            foreach (var brick in _remainingBricks.ToList())
            {
                Destroy(brick.gameObject);
            }
        }
    }

    private Brick BrickPrefabByType(BrickType brickType)
    {
        Brick brickPrefab = null;
        if (_availableBricks != null)
        {
            brickPrefab = _availableBricks.FirstOrDefault(brick => brick.GetBrickType() == brickType);
            if (brickPrefab == null)
            {
                Debug.LogError("THERE IS NO BRICK PREFAB FOR BRICK TYPE: " + brickType);
            }
        }
        
        return brickPrefab;
    }    
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
