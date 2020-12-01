using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private bool _isImmortal;

    [SerializeField]
    private int _hitponts;

    [SerializeField]
    private List<Sprite> _sprites;

    public static event Action<Brick> OnBrickDestruction;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (_sprites.Count != _hitponts)
        {
            Debug.LogWarning("The number of sprites does not match the number of brick hitpoints!");
        }
        _spriteRenderer.sprite = ActualSprite();

    }

    private Sprite ActualSprite()
    {
        // если вдруг выйдет так, что кол-во спрайтов меньше чем кол-во жизней блока,
        // например жизней 3, а у блока всего 2 спрайта, то берем не 3й спрайт из списка(которого нет), 
        // а берем 2й и когда будет 2 жизни будет использоваться тот де 2й спрайт
        var index = Mathf.Clamp(_hitponts, 1, _sprites.Count);
        return _sprites[index - 1];
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision brick and " + col.gameObject.tag);
        if (!_isImmortal && col.gameObject.tag == "Ball")
        {
            Ball ball = col.gameObject.GetComponent<Ball>();
            Debug.Log("ball " + ball);
            ApplyCollisionLogic(ball);
        }
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        _hitponts--;
        if (_hitponts <= 0)
        {
            Debug.Log("this " + this);
            OnBrickDestruction?.Invoke(this);
            Destroy(gameObject);
        }
        else 
        {
            _spriteRenderer.sprite = ActualSprite();
        }
    }
}

//public enum BrickType
//{
//    Easy,
//    Medium,
//    Hard,
//    Immortal
//}
