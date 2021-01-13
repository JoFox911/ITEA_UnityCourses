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

        private float _enemyCooldownAfterShoot = 1f;
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

            // _sharedContext.Weapon.SetAttackTarget(_sharedContext.EnemySpyed.CurrentTarget);
        }

        public override void Execute()
        {
            if (!_sharedContext.EnemySpyManager.IsAnyEnemySpyed)
            {
                Debug.Log("Lost ENEMY");
                _stateSwitcher.Switch(typeof(SearchingEnemyState));
            }
            else
            {
                _botMovement.LookAtTarget();

                if (_botWeapon.IsNoAmmoOnAllWeapons)
                {
                    _stateSwitcher.Switch(typeof(SearchingWeaponState));
                } 
                else if ((Time.time > _cooldownEndTime) && !_botWeapon.IsReload())
                {
                    FolowTarget();

                    // делаем так, чтоб враг стрелял если подошел на какую-то подходящую ему дистанцию 
                    //if (IsTryToShootPossible())
                    //{
                        Debug.Log("Shoot");
                        _cooldownEndTime = Time.time + _enemyCooldownAfterShoot;

                        _botWeapon.Shoot(null);
                    //}
                }

                // начать стрелять когда подходим на какое-то рандомное от 5-7 растояние, после вістрела, тупить секунду, потом _isShooting делаем фолс
            }

            //if (!_sharedContext.Weapon.IsTargetIsDead)
            //{
            //    if (_sharedContext.Weapon.IsWeapoReady)
            //    {
            //        _sharedContext.Weapon.Fire();
            //    }
            //    else
            //    {
            //        //switch to reload system
            //    }
            //}
            //else
            //{
            //    _stateSwitcher.Switch(typeof(SearchingEnemyState));
            //}
        }

        private void FolowTarget()
        {
            _botMovement.FolowTarget(_target.transform.position, _folowDistance);
        }

        //private bool IsTryToShootPossible()
        //{
        //    return _botMovement.DistanceToTarget() < Random.Range(4, 10);
        //}
    }
}