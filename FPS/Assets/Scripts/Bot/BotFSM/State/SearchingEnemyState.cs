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
            // todo change UnityEngine.Random.Range(0, 101);
            // use Vector3.distance
            _destinationPoint = _sharedContext.MapHelper.EnemySearchingPoints[UnityEngine.Random.Range(0, _sharedContext.MapHelper.EnemySearchingPoints.Count)];
            _botMovement.SetTarget(_destinationPoint.position);
        }
    }
}