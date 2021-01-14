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

    private BotSharedContext _botSharedContext;

    [SerializeField]
    private GameObject _raycastSource;

    private FiniteStateMachine<BotSharedContext> _finiteStateMachine;

    private Dictionary<Type, BaseState<BotSharedContext>> _allBotStates = new Dictionary<Type, BaseState<BotSharedContext>>();



    private List<Transform> _itemSpawnPoints;
    private List<Transform> _enemySearchingPoints;

    private PickUpHelper _pickUpHelper;
    private SoldierWeaponManager _soldierWeaponManager;
    private CheckEnemyHelper _checkEnemyHelper;
    private BotMovementManager _botMovementManager;
    private ItemsSpawnsManager _itemsSpawnManager;
    private ComandsSpawnsManager _comandsSpawnManager;

    void Awake()
    {
        _pickUpHelper = gameObject.GetComponent<PickUpHelper>();
        _soldierWeaponManager = gameObject.GetComponent<SoldierWeaponManager>();
        _checkEnemyHelper = gameObject.GetComponent<CheckEnemyHelper>();
        _botMovementManager = gameObject.GetComponent<BotMovementManager>();

        _itemsSpawnManager = ServiceLocator.Resolved<ItemsSpawnsManager>();
        _comandsSpawnManager = ServiceLocator.Resolved<ComandsSpawnsManager>();
    }

    void Start()
    {
        if (_itemsSpawnManager != null)
        {
            _itemSpawnPoints = _itemsSpawnManager.GetAllSpawns();
        }

        if (_itemsSpawnManager != null)
        {
            _enemySearchingPoints = _comandsSpawnManager.GetAllSpawns();
        }

        var _mapHelper = new BotMapHelper(_enemySearchingPoints, _itemSpawnPoints);

        _soldierWeaponManager.Initialize(_raycastSource);

        _botSharedContext = new BotSharedContext(_botMovementManager, _pickUpHelper, _mapHelper, _checkEnemyHelper, _soldierWeaponManager);

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
