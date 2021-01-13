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
        private BotMovementHelper _botMovement;

        public override void OnStateEnter()
        {
            Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
            if (_sharedContext == null)
            {
                return;
            }

            _botMovement = _sharedContext.MovementHelper;
            SetNewDistinationPoint();
        }

        public override void Execute()
        {
            //Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
            if (_botMovement == null)
            {
                return;
            }

            if (_botMovement.IsMovementCompleted)
            {
                if (_sharedContext.ItemDetectionManager.IsItemDetected)
                {
                    var item = _sharedContext.ItemDetectionManager.GrabableObject;
                    // check is weapon
                    var weapon = item.GetComponent<Weapon>();
                    var ammo = item.GetComponent<Ammo>();
                    if (weapon != null)
                    {
                        Debug.Log("_sharedContext.WeaponManager.AddWeapon");
                        _sharedContext.WeaponManager.AddWeapon(weapon);
                    }
                    else if (ammo != null)
                    {
                        _sharedContext.WeaponManager.AddAmmo(ammo);
                    }

                    if (_sharedContext.WeaponManager.IsWeaponSelected && !_sharedContext.WeaponManager.IsNoAmmoOnAllWeapons)
                    {
                        _stateSwitcher.Switch(typeof(SearchingEnemyState));
                    }
                    else
                    {
                        SetNewDistinationPoint();
                    }
                }
                else
                {
                    // если мы пришли, а тут пусто, то идем на другую точку
                    SetNewDistinationPoint();
                }
                
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