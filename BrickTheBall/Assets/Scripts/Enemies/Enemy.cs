using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour, IDestroyableOnCollisionWithDeadZone
{
    [SerializeField]
    private int _points = 100;

    [SerializeField]
    private float _speed = 2.0f;

    private Action<Enemy> _destroyEnemyCallBack;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SetRandomDirection();
        ChangeDirectionWithTimeout();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // если мячик еще не запущен то при косании мяча или платформы фраг будет отскакивать
        if (GameManager.IsGameStarted() && (col.gameObject.CompareTag("Ball") || col.gameObject.CompareTag("Bullet") || col.gameObject.CompareTag("Platform")))
        {
            if (col.gameObject.CompareTag("Ball"))
            {
                ApplyEnemyEffect(col);
                // если враг был убит мячиком - надо сгенерировать бонус
                BonusManager.GenerateBonus(gameObject.transform, BonusSource.Enemy);
            }
            ScoreManager.AddScore(_points);
            AudioManager.PlaySFX(SFXType.EnemyKilled);
            DestroyEnemy();
        }
        else
        {
            // отбиваемся от объекта
            _rigidbody.velocity = (col.contacts[0].normal).normalized * _speed;
        }
    }

    public void DestroyOnCollisionWithDeadZone()
    {
        DestroyEnemy();
    }

    public void SetDestroyCallback(Action<Enemy> destroyEnemyCallBack)
    {
        _destroyEnemyCallBack = destroyEnemyCallBack;
    }

    private void DestroyEnemy()
    {
        DestroyCallback();
        Destroy(gameObject);
    }

    private void DestroyCallback()
    {
        _destroyEnemyCallBack?.Invoke(this);
    }

    private Vector2 GenerateRandomDirication()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
    }

    private void ChangeDirectionWithTimeout()
    {
        StartCoroutine(ChangeDirectionWithDelay(2f));
    }

    IEnumerator ChangeDirectionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetRandomDirection();
        StartCoroutine(ChangeDirectionWithDelay(UnityEngine.Random.Range(.2f, 3f)));
    }

    private void SetRandomDirection()
    {
        _rigidbody.velocity = GenerateRandomDirication().normalized * _speed;
    }

    protected abstract void ApplyEnemyEffect(Collision2D col);
}
