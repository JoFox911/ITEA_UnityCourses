using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public int Points { get; set; }

    private SpriteRenderer _spriteRenderer;    
    private BrickType _type;
    private List<Sprite> _sprites;
    private int _hitponts;
   
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("collision brick and " + col.gameObject.tag);
        if (_type != BrickType.Immortal && col.gameObject.tag == "Ball")
        {
            ApplyCollisionLogic();
        }
    }

    public void Init(Transform containerTransform, BrickTypeData brickTypeData)
    {
        transform.SetParent(containerTransform);
        _sprites = brickTypeData.Sprites;
        _hitponts = brickTypeData.Hitpoints;
        _type = brickTypeData.Type;
        Points = brickTypeData.Points;
        SetActualSprite();
    }

    private void SetActualSprite()
    {
        // если вдруг выйдет так, что кол-во спрайтов меньше чем кол-во жизней блока,
        // например жизней 3, а у блока всего 2 спрайта, то берем не 3й спрайт из списка(которого нет), 
        // а берем 2й и когда будет 2 жизни будет использоваться тот де 2й спрайт
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
