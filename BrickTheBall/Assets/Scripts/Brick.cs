using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    
    [SerializeField]
    private List<Sprite> _sprites;
    [SerializeField]
    private int _hitponts;
    [SerializeField]
    public BrickType _type;
    [SerializeField]
    public int _points;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (_type != BrickType.Immortal && col.gameObject.tag == "Ball")
        {
            AudioManager.PlaySFX(SFXType.BallAndBrickCollision);
            ApplyCollisionLogic();
        }
    }

    public int GetPoints()
    {
        return _points;
    }

    public BrickType GetBrickType()
    {
        return _type;
    }

    public void Init(Transform containerTransform)
    {
        transform.SetParent(containerTransform);
        SetActualSprite();
    }

    private void SetActualSprite()
    {
        // если вдруг выйдет так, что кол-во спрайтов меньше чем кол-во жизней блока,
        // например жизней 3, а у блока всего 2 спрайта, то берем не 3й спрайт из списка(которого нет), 
        // а берем 2й и когда будет 2 жизни будет использоваться тот же 2й спрайт
        var index = Mathf.Clamp(_hitponts, 1, _sprites.Count);
        _spriteRenderer.sprite = _sprites[index - 1];
    }

    private void ApplyCollisionLogic()
    {
        _hitponts--;
        if (_hitponts <= 0)
        {
            GameEvents.BrickDestructedEvent(this);
            Destroy(gameObject);
        }
        else 
        {
            SetActualSprite();
        }
    }
}

public class BrickData
{
    public List<Sprite> Sprites;
    public int Hitponts;
    public BrickType Type;
    public int Points;
}
