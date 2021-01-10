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
            //Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
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
                        _sharedContext.WeaponManager.AddWeapon(weapon);
                    }
                    else if (ammo != null)
                    {
                        _sharedContext.WeaponManager.AddAmmo(ammo);
                    }

                    if (_sharedContext.WeaponManager.IsWeaponSelected)
                    {
                        _stateSwitcher.Switch(typeof(SearchingEnemyState));
                    }
                    else 
                    {
                        SetNewDistinationPoint();
                    }
                }
                
            }
        }

        private void SetNewDistinationPoint()
        {
            // todo change UnityEngine.Random.Range(0, 101);
            _destinationPoint = _sharedContext.MapHelper.ItemSpawnPoints[UnityEngine.Random.Range(0, 2)];
            _botMovement.SetTarget(_destinationPoint.position);
        }
    }
}