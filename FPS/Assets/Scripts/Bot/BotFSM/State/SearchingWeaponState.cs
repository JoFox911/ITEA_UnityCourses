using System.Reflection;
using UnityEngine;
namespace BotLogic
{
    public class SearchingWeaponState : BaseState<BotSharedContext>
    {
        public SearchingWeaponState(BotSharedContext sharedContext) : base(sharedContext)
        {
        }

        private Transform _destinationPoint;
        private BotMovementManager _botMovement;

        public override void OnStateEnter()
        {
            Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
            if (_sharedContext == null)
            {
                return;
            }

            _botMovement = _sharedContext.MovementManager;
            SetNewDistinationPoint();
        }

        public override void Execute()
        {
            //Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
            if (_botMovement == null)
            {
                return;
            }

            
            if (_sharedContext.ItemDetectionManager.IsItemDetected)
            {
                var item = _sharedContext.ItemDetectionManager.GetPickUpObject();
                // check is weapon
                var weapon = item.GetComponent<Weapon>();
                var ammo = item.GetComponent<Ammo>();
                if (weapon != null)
                {
                    Debug.Log("_sharedContext.WeaponManager.AddWeapon");
                    _sharedContext.WeaponManager.AddWeapon(weapon, null);
                }
                else if (ammo != null)
                {
                    _sharedContext.WeaponManager.AddAmmo(ammo, null);
                }

                if (_sharedContext.WeaponManager.IsWeaponSelected() && !_sharedContext.WeaponManager.IsNoAmmoOnAllWeapons)
                {
                    _stateSwitcher.Switch(typeof(SearchingEnemyState));
                }
                else
                {
                    SetNewDistinationPoint();
                }
            } 
            else if (_botMovement.IsMovementCompleted)
            {
                // если мы пришли, а тут пусто, то идем на другую точку
                SetNewDistinationPoint();
            }
        }

        private void SetNewDistinationPoint()
        {
            _destinationPoint = Common.SetectOneOfTheNearestPoint(_sharedContext.MapHelper.ItemSpawnPoints,
                                                                  _botMovement.GetCurrentPossition(),
                                                                  5);
            _botMovement.MoveToTarget(_destinationPoint.position);
        }
    }
}