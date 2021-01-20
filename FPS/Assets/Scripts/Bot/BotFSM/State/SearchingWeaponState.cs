using System.Reflection;
using UnityEngine;
namespace BotLogic
{
    public class SearchingWeaponState : BaseState<BotSharedContext>
    {
        private Transform _destinationPoint;

        private GameObject _item;

        private Weapon _weapon;
        private Ammo _ammo;
        private Heal _heal;

        private readonly int _searchInNearesrPointsNumber = 5;

        public SearchingWeaponState(BotSharedContext sharedContext) : base(sharedContext)
        {
        }
        public override void OnStateEnter()
        {
            Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
            if (_sharedContext == null)
            {
                return;
            }

            SetNewDistinationPoint();
        }

        public override void Execute()
        {
            if (!_sharedContext.SoldierState.IsAlive())
            {
                _stateSwitcher.Switch(typeof(DeadState));
            }
            else if (_sharedContext.WeaponManager.isWeaponReady)
            {
                _stateSwitcher.Switch(typeof(SearchingEnemyState));
            }
            else if (_sharedContext.ItemDetectionManager.IsItemDetected)
            {
                _item = _sharedContext.ItemDetectionManager.GetPickUpObject();
                
                 _weapon = _item.GetComponent<Weapon>();
                _ammo = _item.GetComponent<Ammo>();
                _heal = _item.GetComponent<Heal>();
                if (_weapon != null)
                {
                    _sharedContext.WeaponManager.AddWeapon(_weapon, null);
                }
                else if (_ammo != null)
                {
                    _sharedContext.WeaponManager.AddAmmo(_ammo, null);
                }
                else if (_heal != null)
                {
                    _sharedContext.SoldierState.ApplyHeal(_heal);
                }

                SetNewDistinationPoint();
            }
            else if (_sharedContext.MovementManager.IsMovementCompleted)
            {
                // если мы пришли, а тут пусто, то идем на другую точку
                SetNewDistinationPoint();
            }
        }

        private void SetNewDistinationPoint()
        {
            _destinationPoint = Common.SetectOneOfTheNearestPoint(_sharedContext.MapHelper.ItemSpawnPoints,
                                                                  _sharedContext.MovementManager.GetCurrentPossition(),
                                                                  _searchInNearesrPointsNumber);
            _sharedContext.MovementManager.MoveToTarget(_destinationPoint.position);
        }
    }
}