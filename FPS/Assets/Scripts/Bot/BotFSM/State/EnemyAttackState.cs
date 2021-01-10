namespace BotLogic
{
    public class EnemyAttackState : BaseState<BotSharedContext>
    {
        public EnemyAttackState(BotSharedContext sharedContext) : base(sharedContext)
        {
        }

        public override void OnStateEnter()
        {
            // _sharedContext.Weapon.SetAttackTarget(_sharedContext.EnemySpyed.CurrentTarget);
        }

        public override void Execute()
        {
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
    }
}