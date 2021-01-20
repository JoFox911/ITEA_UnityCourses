using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace BotLogic
{
    public class SearchingEnemyState : BaseState<BotSharedContext>
    {
        private Transform _destinationPoint;
        private List<Transform> _searchingPointsList;

        public SearchingEnemyState(BotSharedContext sharedContext) : base(sharedContext)
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
            else if (_sharedContext.EnemySpyManager.IsAnyEnemySpyed)
            {
                _stateSwitcher.Switch(typeof(EnemyAttackState));
            }
            else if (_sharedContext.MovementManager.IsMovementCompleted)
            {
                //take next enemy searching point
                SetNewDistinationPoint();
            }
        }

        private void SetNewDistinationPoint()
        {
            _searchingPointsList = _sharedContext.MapHelper.EnemySearchingPoints.Where(point => point != _destinationPoint).ToList();
            _destinationPoint = Common.SetectOneOfTheNearestPoint(_searchingPointsList,
                                                                  _sharedContext.MovementManager.GetCurrentPossition(), _searchingPointsList.Count);
            _sharedContext.MovementManager.MoveToTarget(_destinationPoint.position);
        }
    }
}