﻿using System.Collections.Generic;
using UnityEngine;
using BotLogic;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(PickUpHelper))]
[RequireComponent(typeof(CheckEnemyHelper))]
[RequireComponent(typeof(SoldierWeaponManager))]
[RequireComponent(typeof(BotMovementManager))]
public class ControllerBotFSM : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _enemySearchingPoints;

    [SerializeField]
    private BotSharedContext _botSharedContext = new BotSharedContext();

    [SerializeField]
    private GameObject _raycastSource;

    private FiniteStateMachine<BotSharedContext> _finiteStateMachine;

    private Dictionary<Type, BaseState<BotSharedContext>> _allBotStates = new Dictionary<Type, BaseState<BotSharedContext>>();



    private List<Transform> _itemSpawnPoints;

    private PickUpHelper _pickUpHelper;
    private SoldierWeaponManager _soldierWeaponManager;
    private SpawnManager _spawnManager;
    private CheckEnemyHelper _checkEnemyHelper;
    private BotMovementManager _botMovementManager;

    void Awake()
    {
        _pickUpHelper = gameObject.GetComponent<PickUpHelper>();
        _soldierWeaponManager = gameObject.GetComponent<SoldierWeaponManager>();
        _checkEnemyHelper = gameObject.GetComponent<CheckEnemyHelper>();
        _botMovementManager = gameObject.GetComponent<BotMovementManager>();

        _spawnManager = ServiceLocator.Resolved<SpawnManager>();
        
    }

    void Start()
    {
        if (_spawnManager != null)
        {
            _itemSpawnPoints = _spawnManager.GetAllSpawns();
        }

        InitFSM();
    }

    void Update()
    {
        if (_finiteStateMachine != null)
        {
            _finiteStateMachine.Update();
        }

        UpdateContext();
    }

    private void UpdateContext()
    {
        _botSharedContext.MovementHelper.UpdateState();
        _botSharedContext.WeaponManager.UpdateState();
        _botSharedContext.ItemDetectionManager.UpdateState();
        _botSharedContext.EnemySpyManager.UpdateState();
    }

    private void InitFSM()
    {
        _botSharedContext.MovementHelper.Init(_botMovementManager);
        _botSharedContext.MapHelper.Init(_enemySearchingPoints, _itemSpawnPoints);
        _botSharedContext.ItemDetectionManager.Init(_pickUpHelper);
        _botSharedContext.WeaponManager.Init(_soldierWeaponManager, _raycastSource);
        _botSharedContext.EnemySpyManager.Init(_checkEnemyHelper);

        _finiteStateMachine = new FiniteStateMachine<BotSharedContext>();

        _allBotStates[typeof(SearchingEnemyState)] = new SearchingEnemyState(_botSharedContext);
        _allBotStates[typeof(EnemyAttackState)] = new EnemyAttackState(_botSharedContext);
        _allBotStates[typeof(SearchingWeaponState)] = new SearchingWeaponState(_botSharedContext);

        _finiteStateMachine.InitStates(_allBotStates);

        _finiteStateMachine.Switch(typeof(SearchingWeaponState));
    }
}
