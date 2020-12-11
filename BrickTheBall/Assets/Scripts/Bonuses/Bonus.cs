using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bonus : MonoBehaviour
{
    [SerializeField]
    private float  _fallSpeed = 7;

    public static event Action<Bonus> OnBonusDestroy;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            ApplyEffect();
        }

        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "DeadZone")
        {
            Destroy(gameObject);
            OnBonusDestroy?.Invoke(this);
        }
    }

    void Update()
    {
        _rigidbody.velocity = Vector2.down * _fallSpeed;
    }

    protected abstract void ApplyEffect();
}
