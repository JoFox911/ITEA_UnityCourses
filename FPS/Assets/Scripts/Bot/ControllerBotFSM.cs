using System.Collections.Generic;
using UnityEngine;
using BotLogic;
using System;

[RequireComponent(typeof(PickUpHelper))]
[RequireComponent(typeof(CheckEnemyHelper))]
[RequireComponent(typeof(SoldierWeaponManager))]
[RequireComponent(typeof(BotMovementManager))]
public class ControllerBotFSM : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _enemySearchingPoints;

    [SerializeField]
    private BotSharedContext _botSharedContext;

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

        if (_spawnManager != null)
        {
            _itemSpawnPoints = _spawnManager.GetAllSpawns();
        }

        var _mapHelper = new BotMapHelper(_enemySearchingPoints, _itemSpawnPoints);

        _soldierWeaponManager.Initialize(_raycastSource);

        _botSharedContext = new BotSharedContext(_botMovementManager, _pickUpHelper, _mapHelper, _checkEnemyHelper, _soldierWeaponManager);


    }

    void Start()
    {
        InitFSM();
    }

    void Update()
    {
        if (_finiteStateMachine != null)
        {
            _finiteStateMachine.Update();
        }
    }

    private void InitFSM()
    {

        _finiteStateMachine = new FiniteStateMachine<BotSharedContext>();

        _allBotStates[typeof(SearchingEnemyState)] = new SearchingEnemyState(_botSharedContext);
        _allBotStates[typeof(EnemyAttackState)] = new EnemyAttackState(_botSharedContext);
        _allBotStates[typeof(SearchingWeaponState)] = new SearchingWeaponState(_botSharedContext);

        _finiteStateMachine.InitStates(_allBotStates);

        _finiteStateMachine.Switch(typeof(SearchingWeaponState));
    }
}
