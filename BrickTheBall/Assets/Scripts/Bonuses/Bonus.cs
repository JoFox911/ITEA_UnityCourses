using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bonus : MonoBehaviour
{
    [SerializeField]
    private float  _fallSpeed = 2.5f;

    public static event Action<Bonus> OnBonusDestroy;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.velocity = Vector2.down * _fallSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            ApplyEffect();
        }

        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
            OnBonusDestroy?.Invoke(this);
        }
    }

    

    protected abstract void ApplyEffect();
}
