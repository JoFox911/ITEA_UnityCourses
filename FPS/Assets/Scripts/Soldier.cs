using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Soldier : MonoBehaviour, IShootable
{
    [SerializeField]
    private float _health = 50f;

    [SerializeField]
    private float _maxHealth = 50f;

    [SerializeField]
    private bool _isBot;


    [SerializeField]
    private string _name;

    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        if (_isBot)
        {
            _name = NamesGenerator.GenerateRandomName();
        }
        
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public void TakeDamage(AttackData attackData)
    {
        SetNewHealthValue(_health - attackData.damage);

        if (_health <= 0)
        {
            Die(attackData);
        }
    }

    private void Die(AttackData attackData)
    {
        _anim.SetTrigger("dead");
        EventAgregator.Post(this, new SoldierKilledEvent(attackData.shooterName, attackData.weaponName, _name));
        if (!_isBot)
        {
            EventAgregator.Post(this, new PlayerKilledEvent());
        }

        if (_isBot)
        {
            StartCoroutine(DestroyPlayer());
        }
    }

    public IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public string GetName()
    {
        return _name;
    }

    private void SetNewHealthValue(float newValue)
    {
        _health = newValue;
        if (!_isBot)
        {
            EventAgregator.Post(this, new ChangeHealthEvent(_health / _maxHealth));
        }
    }
}
