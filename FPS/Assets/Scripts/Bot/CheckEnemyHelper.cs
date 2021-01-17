using UnityEngine;

public class CheckEnemyHelper : MonoBehaviour
{
    [SerializeField]
    private float _castDistance = 7f;
    [SerializeField]
    private LayerMask _enemyMask;

    private CharacterController _charController;
    private GameObject _target;

    private RaycastHit _hit;

    private string _teamTag;

    public bool IsAnyEnemySpyed => _target != null;

    void Awake()
    {
        _charController = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckEnemy();
    }

    public void CheckEnemy()
    {
        if (Physics.SphereCast(transform.position + _charController.center, _charController.radius * _castDistance,
                               transform.forward, out _hit, _castDistance, _enemyMask))
        //var hitsInfoArray = Physics.OverlapSphere(transform.position, _castDistance, _enemyMask);
        //var objects = Physics.SphereCast(own.position, radius, own.forward);
        //if (Physics.SphereCast(transform.position + _charController.center, _castRadius,
        //                   transform.forward, out _hit, 0.1f, _enemyMask))
        {
            if (_teamTag == string.Empty || !_hit.transform.gameObject.CompareTag(_teamTag))
            {
                _target = _hit.transform.gameObject;
            }
            else 
            {
                _target = null;
            }
        }
        else
        {
            _target = null;

        }
    }

    public GameObject GetTarget()
    {
        return _target;
    }

    public void SetTeamTag(string tag)
    {
        _teamTag = tag;
    }
}
