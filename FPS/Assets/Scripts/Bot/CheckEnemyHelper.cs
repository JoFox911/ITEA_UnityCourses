﻿using UnityEngine;

public class CheckEnemyHelper : MonoBehaviour
{
    [SerializeField]
    private float _castDistance = 7f;
    [SerializeField]
    private LayerMask _enemyMask;

    private CharacterController _charController;
    private GameObject _target;

    private RaycastHit _hit;

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
        {
            _target = _hit.transform.gameObject;
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

    public float GetInSightDistance()
    {
        return _castDistance;
    }
}
