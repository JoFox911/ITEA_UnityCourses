using System.Reflection;
using UnityEngine;
namespace BotLogic
{
    public class SearchingEnemyState : BaseState<BotSharedContext>
    {
        public SearchingEnemyState(BotSharedContext sharedContext) : base(sharedContext)
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

            if (!_botMovement.IsMovementCompleted)
            {
                //
                if (_sharedContext.EnemySpyManager.IsAnyEnemySpyed)
                {
                    _stateSwitcher.Switch(typeof(EnemyAttackState));
                }
            }
            else
            {
                //take next enemy searching point
                SetNewDistinationPoint();
            }
        }

        private void SetNewDistinationPoint()
        {
            _destinationPoint = Common.SetectOneOfTheNearestPoint(_sharedContext.MapHelper.EnemySearchingPoints,
                                                                  _botMovement.GetCurrentPossition(), 2);
            _botMovement.MoveToTarget(_destinationPoint.position);
        }
    }
}