using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float _speed = 10.0f;

    private Rigidbody2D _rigidbody;

    public static event Action<Ball> OnBallDestroy;

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
