using System.Collections.Generic;
using UnityEngine;
using BotLogic;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PickUpHelper))]
[RequireComponent(typeof(SoldierWeaponManager))]
public class BotFSMController : MonoBehaviour
{
    [SerializeField]
    public List<Transform> _enemySearchingPoints;
    [SerializeField]
    public List<Transform> _itemSpawnPoints;

    [SerializeField]
    private BotSharedContext _botSharedContext = new BotSharedContext();

    private BotFiniteStateMachine<BotSharedContext> _finiteStateMachine;

    private Dictionary<Type, BaseState<BotSharedContext>> _allBotStates = new Dictionary<Type, BaseState<BotSharedContext>>();

    

    private NavMeshAgent _navMeshAgent;
    private PickUpHelper _pickUpHelper;
    private SoldierWeaponManager _soldierWeaponManager;

    void Awake()
    {
        _pickUpHelper = gameObject.GetComponent<PickUpHelper>();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _soldierWeaponManager = gameObject.GetComponent<SoldierWeaponManager>();
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

        UpdateContext();
    }

    private void UpdateContext()
    {
        _botSharedContext.MovementManager.UpdateState();
        _botSharedContext.WeaponManager.UpdateState();
        _botSharedContext.ItemDetectionManager.UpdateState();
    }

    private void InitFSM()
    {
        _botSharedContext.MovementManager.Init(_navMeshAgent);
        _botSharedContext.MapHelper.Init(_enemySearchingPoints, _itemSpawnPoints);
        _botSharedContext.ItemDetectionManager.Init(_pickUpHelper);
        _botSharedContext.WeaponManager.Init(_soldierWeaponManager);

        _finiteStateMachine = new BotFiniteStateMachine<BotSharedContext>();

        _allBotStates[typeof(SearchingEnemyState)] = new SearchingEnemyState(_botSharedContext);
        _allBotStates[typeof(EnemyAttackState)] = new EnemyAttackState(_botSharedContext);
        _allBotStates[typeof(SearchingWeaponState)] = new SearchingWeaponState(_botSharedContext);

        _finiteStateMachine.InitStates(_allBotStates);

        _finiteStateMachine.Switch(typeof(SearchingWeaponState));
    }
}
