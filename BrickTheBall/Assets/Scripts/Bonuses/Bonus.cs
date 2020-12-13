using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bonus : MonoBehaviour
{
    [SerializeField]
    private float  _fallSpeed = 1.5f;

    private Action<Bonus> _destroyBonusCallback;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.down * _fallSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            AudioManager.PlaySFX(SFXType.BonusCatched);
            ApplyEffect();
        }

        if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("DeadZone"))
        {
            DestroyCallback();
            Destroy(gameObject);
        }
    }

    public void SetDestroyCallback(Action<Bonus> destroyBonusCallBack)
    {
        _destroyBonusCallback = destroyBonusCallBack;
    }

    private void DestroyCallback()
    {
        _destroyBonusCallback?.Invoke(this);
    }

    protected abstract void ApplyEffect();
}
