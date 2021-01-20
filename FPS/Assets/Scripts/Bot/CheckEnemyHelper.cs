using UnityEngine;

public class CheckEnemyHelper : MonoBehaviour
{
    [SerializeField]
    private float _castDistance = 7f;
    [SerializeField]
    private LayerMask _enemyMask;
    [SerializeField]
    private float _lostEnemyDelay;

    public bool IsAnyEnemySpyed => _target != null;

    private GameObject _target;
    private GameObject _tempTarget;

    private Collider[] _hitsInfoArray;
    private float _lostEnemyTime = 0;

    void Update()
    {
        CheckEnemy();
    }

    private void CheckEnemy()
    {
        _tempTarget = null;
        _hitsInfoArray = Physics.OverlapSphere(transform.position, _castDistance, _enemyMask);
        foreach (var hitCollider in _hitsInfoArray)
        {
            if (!hitCollider.transform.gameObject.CompareTag(gameObject.tag))
            {
                _tempTarget = hitCollider.transform.gameObject;
            }
        }

        if (Time.time > _lostEnemyTime && _tempTarget == null)
        {
            _target = null;
        }
        else if (_tempTarget != null)
        {
            _target = _tempTarget;
            _lostEnemyTime = Time.time + _lostEnemyDelay;
        }
    }

    public GameObject GetTarget()
    {
        return _target;
    }
}
