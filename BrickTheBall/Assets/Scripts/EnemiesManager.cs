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

    private List<Enemy> _displayingEnemies;

    void Awake()
    {
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
        GenerateEnemy(_leftPossition);
        GenerateEnemy(_rightPossition);
    }

    public void ResetState()
    {
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

    public void GenerateEnemy(Vector3 position)
    {
        var enemyPrefab = _availableEnemies[Random.Range(0, _availableEnemies.Count)];

        var newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        newEnemy.SetDestroyCallback(OnEnemyDestroy);
        _displayingEnemies.Add(newEnemy);
    }

    private void OnEnemyDestroy(Enemy enemy)
    {
        _displayingEnemies.Remove(enemy);

        if (_displayingEnemies.Count <= 0)
        {
            StartCoroutine(GenerateNewEnemies(1.5f));
        }
    }

    IEnumerator GenerateNewEnemies(float delay)
    {
        yield return new WaitForSeconds(delay);
        GenerateEnemy(_leftPossition);
        GenerateEnemy(_rightPossition);
    }
}
