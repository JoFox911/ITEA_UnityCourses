using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 _initialBricksSpawnPossition;
    [SerializeField]
    private Brick _brickPrefab;
    [SerializeField]
    private GameObject _bricksContainer;

    private List<Brick> _remainingBricks;

    void Awake()
    {
        GameEvents.OnResetGameState += ResetState;
        GameEvents.OnChangeLevel += OnChangeLevel;
    }

    void OnDestroy()
    {
        GameEvents.OnResetGameState -= ResetState;
        GameEvents.OnChangeLevel -= OnChangeLevel;
    }

    private void OnChangeLevel(int level)
    {
        GenerateLevelBricks(GameManager.GetLevelMap(level));
    }

    private void GenerateLevelBricks(List<List<int>> levelMap)
    {
        _remainingBricks = new List<Brick>();
        var bricksOffset = .01f;
        var isEnemiesGeneratorExists = false;

        float brickSpawnPositionX, brickSpawnPositionY;
        brickSpawnPositionY = _initialBricksSpawnPossition.y;

        foreach (var levelRow in levelMap)
        {
            float rowWidth = 0.4f;
            brickSpawnPositionX = _initialBricksSpawnPossition.x;
            foreach (var item in levelRow)
            {
                float columnWidth = 0.8f;
                if (item != 0)
                {
                    var brickType = (BrickType)item;
                    var brickData = BricksConfiguration.BrickDataByType(brickType);
                    isEnemiesGeneratorExists = isEnemiesGeneratorExists || brickData.IsEnemyGenerator;
                    columnWidth = columnWidth < brickData.SizeX ? brickData.SizeX : columnWidth;
                    rowWidth = rowWidth < brickData.SizeY ? brickData.SizeY : rowWidth;

                    var newBrick = Instantiate(_brickPrefab, new Vector3(brickSpawnPositionX, brickSpawnPositionY, _initialBricksSpawnPossition.z), Quaternion.identity);
                    newBrick.Init(_bricksContainer.transform, brickData);
                    newBrick.SetDestroyCallback(OnBrickDestruction);
                    _remainingBricks.Add(newBrick);
                }
                brickSpawnPositionX += columnWidth + bricksOffset;
            }
            brickSpawnPositionY -= rowWidth + bricksOffset;
        }

        // если на уровне нет блоков которые генерируют врагов,
        // то менеджер врагов будет генерировать их сам 
        EnemiesManager.SetIsAutoSpawnEnemies(!isEnemiesGeneratorExists);
    }

    private void OnBrickDestruction(Brick brick)
    {
        _remainingBricks.Remove(brick);
        if (IsAllPossibleBricksDestroyed())
        {
            GameManager.OnAllBricksDestroyed();
        }
    }

    private bool IsAllPossibleBricksDestroyed()
    {
        if (_remainingBricks.Count <= 0) 
        {
            return true;
        }
        var numberOfUnImmortalBricks = _remainingBricks.Count(brick => brick.GetBrickType() != BrickType.Immortal);
        return numberOfUnImmortalBricks <= 0;
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
        _remainingBricks = new List<Brick>();
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
