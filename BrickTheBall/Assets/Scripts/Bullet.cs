using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    private Rigidbody2D _rigidbody;

    private Action<Bullet> _destroyBulletCallBack;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        DestroyCallback();
        Destroy(gameObject);
    }
    
    public void StartMoving()
    {
        _rigidbody.velocity = Vector2.up * _speed;
    }

    public void InitCallback(Action<Bullet> destroyBulletCallBack)
    {
        _destroyBulletCallBack = destroyBulletCallBack;
    }

    private void DestroyCallback()
    {
        _destroyBulletCallBack?.Invoke(this);
    }
}
