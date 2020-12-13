using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _availableEnemies;

    [SerializeField]
    private Vector3 _leftPossition;

    [SerializeField]
    private Vector3 _rightPossition;

    public static bool _autoSpawnEnemies;

    private List<Enemy> _displayingEnemies;

    private IEnumerator _generateEnemiesCoroutine;    

    private static EnemiesManager _instance;

    void Awake()
    {
        _instance = this;

        _autoSpawnEnemies = true;
        _displayingEnemies = new List<Enemy>();

        GameEvents.OnChangeLevel += OnChangeLevel;
    }

    void OnDestroy()
    {
        GameEvents.OnChangeLevel -= OnChangeLevel;
    }

    public void OnChangeLevel(int level)
    {
        ResetState();
        GenerateEnemies();
    }

    public void ResetState()
    {
        if (_generateEnemiesCoroutine != null)
        { 
            StopCoroutine(_generateEnemiesCoroutine);
        }
        if (_displayingEnemies != null)
        {
            foreach (var enemy in _displayingEnemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
        _displayingEnemies = new List<Enemy>();
    }

    public static void SetIsAutoSpawnEnemies(bool value)
    {
        _autoSpawnEnemies = value;
    }

    public static void GenerateEnemy(Vector3 position)
    {
        _instance.GenerateEnemyInner(position);
    }

    private void GenerateEnemyInner(Vector3 position)
    {
        var enemyPrefab = _availableEnemies[Random.Range(0, _availableEnemies.Count)];

        var newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        newEnemy.SetDestroyCallback(OnEnemyDestroy);
        _displayingEnemies.Add(newEnemy);
    }

    private void GenerateEnemies()
    {
        if (_autoSpawnEnemies)
        {
            GenerateEnemy(new Vector2(Random.Range(_leftPossition.x, _rightPossition.x), _leftPossition.y));
            GenerateEnemy(new Vector2(Random.Range(_leftPossition.x, _rightPossition.x), _leftPossition.y));
        }
    }


    private void OnEnemyDestroy(Enemy enemy)
    {
        _displayingEnemies.Remove(enemy);

        if (_displayingEnemies.Count <= 0)
        {
            _generateEnemiesCoroutine = GenerateNewEnemies(Random.Range(1, 3));
            StartCoroutine(_generateEnemiesCoroutine);
        }
    }

    IEnumerator GenerateNewEnemies(float delay)
    {
        yield return new WaitForSeconds(delay);
        GenerateEnemies();
    }
}
