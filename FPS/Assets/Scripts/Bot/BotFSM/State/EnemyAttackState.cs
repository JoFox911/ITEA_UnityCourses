using System;
using System.Reflection;
using UnityEngine;

namespace BotLogic
{
    public class EnemyAttackState : BaseState<BotSharedContext>
    {
        private BotMovementManager _botMovement;
        private SoldierWeaponManager _botWeapon;
        private GameObject _target;

        private float _folowDistance = 7f;

        private float _enemyCooldownAfterShoot = 1.5f;
        private float _cooldownEndTime = 0f;

        public EnemyAttackState(BotSharedContext sharedContext) : base(sharedContext)
        {
        }

        public override void OnStateEnter()
        {
            Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
            if (_sharedContext == null)
            {
                return;
            }

            _botMovement = _sharedContext.MovementManager;
            _botWeapon = _sharedContext.WeaponManager;
            _target = _sharedContext.EnemySpyManager.GetTarget();

            FolowTarget();
        }

        public override void Execute()
        {
            if (!_sharedContext.SoldierState.IsAlive())
            {
                _stateSwitcher.Switch(typeof(DeadState));
            } 
            else if (!_sharedContext.EnemySpyManager.IsAnyEnemySpyed)
            {
                //Debug.Log("Lost ENEMY");
                _stateSwitcher.Switch(typeof(SearchingEnemyState));
            }
            else
            {
                _botMovement.LookAtTarget();

                if (_botWeapon.IsNoAmmoOnAllWeapons)
                {
                    _stateSwitcher.Switch(typeof(SearchingWeaponState));
                } 
                else if ((Time.time > _cooldownEndTime) && !_botWeapon.isReloading)
                {
                    FolowTarget();
                    _botWeapon.Attack(UpdateEndCooldownTime);
                }
            }
        }

        private void UpdateEndCooldownTime()
        {
            _cooldownEndTime = Time.time + _enemyCooldownAfterShoot;
        }

        private void FolowTarget()
        {
            _botMovement.FolowTarget(_target.transform.position, _folowDistance);
        }

    }
}