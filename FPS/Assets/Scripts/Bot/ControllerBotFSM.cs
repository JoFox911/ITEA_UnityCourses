using System.Collections.Generic;
using UnityEngine;
using BotLogic;
using System;

[RequireComponent(typeof(PickUpHelper))]
[RequireComponent(typeof(CheckEnemyHelper))]
[RequireComponent(typeof(SoldierWeaponManager))]
[RequireComponent(typeof(BotMovementManager))]
[RequireComponent(typeof(Soldier))]
public class ControllerBotFSM : MonoBehaviour
{
    [SerializeField]
    private GameObject _raycastSource;

    private BotSharedContext _botSharedContext;

    private FiniteStateMachine<BotSharedContext> _finiteStateMachine;

    private Dictionary<Type, BaseState<BotSharedContext>> _allBotStates;

    private PickUpHelper _pickUpHelper;
    private SoldierWeaponManager _soldierWeaponManager;
    private CheckEnemyHelper _checkEnemyHelper;
    private BotMovementManager _botMovementManager;
    private ItemsSpawnsManager _itemsSpawnManager;
    private TeamsManager _teamsSpawnManager;
    private Soldier _soldier;

    void Awake()
    {
        _pickUpHelper = gameObject.GetComponent<PickUpHelper>();
        _soldierWeaponManager = gameObject.GetComponent<SoldierWeaponManager>();
        _checkEnemyHelper = gameObject.GetComponent<CheckEnemyHelper>();
        _botMovementManager = gameObject.GetComponent<BotMovementManager>();
        _soldier = gameObject.GetComponent<Soldier>();

        _itemsSpawnManager = ServiceLocator.Resolved<ItemsSpawnsManager>();
        _teamsSpawnManager = ServiceLocator.Resolved<TeamsManager>();
    }

    void Start()
    {
        BotMapHelper _mapHelper = null;
        if (_itemsSpawnManager != null && _itemsSpawnManager != null)
        {
            _mapHelper = new BotMapHelper(_teamsSpawnManager.GetAllSpawns(), _itemsSpawnManager.GetAllSpawns());
        }

        _soldierWeaponManager.Initialize(_raycastSource);

        _botSharedContext = new BotSharedContext(_botMovementManager, _pickUpHelper, _mapHelper, _checkEnemyHelper, _soldierWeaponManager, _soldier);

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

        _allBotStates = new Dictionary<Type, BaseState<BotSharedContext>>
        {
            [typeof(SearchingEnemyState)] = new SearchingEnemyState(_botSharedContext),
            [typeof(EnemyAttackState)] = new EnemyAttackState(_botSharedContext),
            [typeof(SearchingWeaponState)] = new SearchingWeaponState(_botSharedContext),
            [typeof(DeadState)] = new DeadState(_botSharedContext)
        };

        _finiteStateMachine.InitStates(_allBotStates);

        _finiteStateMachine.Switch(typeof(SearchingWeaponState));
    }
}
