using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Soldier : MonoBehaviour, IShootable
{
    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    private bool _isBot;

    [SerializeField]
    private string _name;

    [SerializeField]
    private GameObject _friendIndicator;

    [SerializeField]
    private float _hideDeadBotTimeout = 2f;

    private Animator _anim;
    private float _maxHealth;
    private bool _isAlive = true;


    void Awake()
    {
        _anim = GetComponent<Animator>();

        _maxHealth = _health;
        if (_isBot)
        {
            _name = NamesGenerator.GenerateRandomName();
        }
        else 
        {
            _name = PlayerPrefs.GetString("PlayerName", "Nemo");
        }
    }
        
    public void TakeDamage(AttackData attackData)
    {
        SetNewHealthValue(_health - attackData.damage);

        if (_isAlive && _health <= 0)
        {
            if (_health <= 0)
            {
                Die(attackData);
            }
        }
        else if (_isBot)
        {
            _anim.SetTrigger("damage");
        }
    }

    public bool IsAlive()
    {
        return _isAlive;
    }

    public string GetName()
    {
        return _name;
    }

    public bool GetIsBot()
    {
        return _isBot;
    }

    public void ShowFriendIndicator()
    {
        _friendIndicator.SetActive(true);
    }

    public void ApplyHeal(Heal heal)
    {
        SetNewHealthValue(Mathf.Clamp(_health + heal.GetHealpoints(), 0, _maxHealth));
        Destroy(heal.transform.gameObject);
    }

    private void SetNewHealthValue(float newValue)
    {
        _health = newValue;
        if (!_isBot)
        {
            EventAgregator.Post(this, new ChangeHealthEvent(_health / _maxHealth));
        }
    }

    private void Die(AttackData attackData)
    {
        _isAlive = false;
        _anim.SetTrigger("dead");

        EventAgregator.Post(this, new SoldierKilledEvent(new KillInfoData(attackData.shooterName, _name, attackData.weaponIcon, attackData.weaponName)));

        if (!_isBot)
        {
            EventAgregator.Post(this, new PlayerKilledEvent());
        }
        else
        {
            Destroy(gameObject, _hideDeadBotTimeout);
        }
    }
}
