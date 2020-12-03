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

    public void StartMoving(float initialSpeed, Vector2 direction)
    {
        Debug.Log("StartMoving dir " + direction.x + " " + direction.y);
        _speed = initialSpeed;
        _rigidbody.velocity = direction * _speed;
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

    public Vector2 GetVelocity()
    {
        return _rigidbody.velocity.normalized;
    }
}
