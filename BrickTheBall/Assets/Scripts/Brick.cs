using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    private List<Sprite> _sprites;
    private int _hitponts;
    private BrickType _type;
    private int _points;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            if (_type == BrickType.Immortal)
            {
                AudioManager.PlaySFX(SFXType.BallAnImmortaldBrickCollision);
            }
            else
            {
                AudioManager.PlaySFX(SFXType.BallAndBrickCollision);
                ApplyCollisionLogic();
            }
        }


        if (_type != BrickType.Immortal && (col.gameObject.CompareTag("Bullet")))
        {
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

    public void Init(Transform containerTransform, BrickData brickData)
    {
        _sprites = brickData.Sprites;
        _hitponts = brickData.Hitponts;
        _type = brickData.Type;
        _points = brickData.Points;

        transform.SetParent(containerTransform);
        SetActualSprite();
        SetSize(brickData.SizeX, brickData.SizeY);
    }

    private void SetActualSprite()
    {
        // если вдруг выйдет так, что кол-во спрайтов меньше чем кол-во жизней блока,
        // например жизней 3, а у блока всего 2 спрайта, то берем не 3й спрайт из списка(которого нет), 
        // а берем 2й и когда будет 2 жизни будет использоваться тот же 2й спрайт
        var index = Mathf.Clamp(_hitponts, 1, _sprites.Count);
        _spriteRenderer.sprite = _sprites[index - 1];
    }

    private void SetSize(float sizeX, float sizeY)
    {
        _spriteRenderer.size = new Vector2(sizeX, sizeY);
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


