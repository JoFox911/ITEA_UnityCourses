using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public static event Action<Ball> OnBallDestroy;

    private float _speed = 7.0f;
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void StartMoving(float initialSpeed)
    {
        _speed = initialSpeed;
        _rigidbody.velocity = Vector2.up * _speed;
    }

    
    public void SetDirection(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }

    public void Die()
    {
        OnBallDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}
