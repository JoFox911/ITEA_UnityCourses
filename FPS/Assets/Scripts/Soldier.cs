using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Soldier : MonoBehaviour, IShootable
{
    [SerializeField]
    private float _health = 50f;

    [SerializeField]
    private bool _isBot;

    [SerializeField]
    private string _name;

    private Animator _anim;
    private float _maxHealth = 50f;
    private bool _isAlive = true;


    void Awake()
    {
        _anim = GetComponent<Animator>();
        _maxHealth = _health;
        if (_isBot)
        {
            _name = NamesGenerator.GenerateRandomName();
        }        
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public void TakeDamage(AttackData attackData)
    {
        SetNewHealthValue(_health - attackData.damage);

        if (_isAlive && _health <= 0)
        {
            Die(attackData);
        }
    }

    private void Die(AttackData attackData)
    {
        _isAlive = false;
        _anim.SetTrigger("dead");

        EventAgregator.Post(this, new SoldierKilledEvent( new KillInfoData(attackData.shooterName, _name, attackData.weaponIcon, attackData.weaponName)));
        
        if (!_isBot)
        {
            EventAgregator.Post(this, new PlayerKilledEvent());
        } 
        else
        {
            // not correct. each bor kill be summed there
            ServiceLocator.Resolved<GameController>()?.AddKilledEnemy();
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

    public bool GetIsBot()
    {
        return _isBot;
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
