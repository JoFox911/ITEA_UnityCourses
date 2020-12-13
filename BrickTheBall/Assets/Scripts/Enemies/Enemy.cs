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

    private static event Action<Enemy> _destroyEnemyCallBack;

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
        if (col.gameObject.CompareTag("Ball"))
        {
            GameEvents.EnemyDestroyedEvent(_points);
            ApplyEnemyEffect();
            DestroyEnemy();
        }
        if (col.gameObject.CompareTag("Bullet") || col.gameObject.CompareTag("Platform"))
        {
            GameEvents.EnemyDestroyedEvent(_points);
            DestroyEnemy();
        }
        else
        {
            // отбиваемся от объекта
            _rigidbody.velocity = (col.contacts[0].normal).normalized * _speed;
            

            ////todo в корутину?
            //var newDirection = (col.contacts[0].normal + new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f)));

            //for (int i = 0; i < 50; i++)
            //{
            //    if (IsDetectedCollisionInThisDerection(newDirection))
            //    {
            //        newDirection += new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f));
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //_rigidbody.velocity = newDirection.normalized * _speed;
        }
    }

    //private bool IsDetectedCollisionInThisDerection(Vector2 derection)
    //{
    //    // Cast a ray straight to the new direction.
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, derection);

    //    // If it hits something...
    //    if (hit.collider != null)
    //    {
    //        // Calculate the distance from the surface
    //        float distance = Mathf.Abs(hit.point.y - transform.position.y);
    //        Debug.Log("distance " + distance);
    //        return distance < 0.5f;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    public void ChangeDirectionWithTimeout()
    {
        StartCoroutine(ChangeDirectionWithDelay(2f));
    }

    
    IEnumerator ChangeDirectionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetRandomDirection();
        StartCoroutine(ChangeDirectionWithDelay(UnityEngine.Random.Range(0.2f, 3f)));
    }

    public void SetRandomDirection()
    {
        _rigidbody.velocity = GenerateRandomDirication() * _speed;
    }


    private Vector2 GenerateRandomDirication()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public void DestroyOnCollisionWithDeadZone()
    {
        DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        DestroyCallback();
        Destroy(gameObject);
    }

    public void SetDestroyCallback(Action<Enemy> destroyEnemyCallBack)
    {
        _destroyEnemyCallBack = destroyEnemyCallBack;
    }

    public void DestroyCallback()
    {
        _destroyEnemyCallBack?.Invoke(this);
    }

    protected abstract void ApplyEnemyEffect();
}
