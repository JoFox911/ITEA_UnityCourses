using System.Reflection;
using UnityEngine;
namespace BotLogic
{
    public class DeadState : BaseState<BotSharedContext>
    {
        public DeadState(BotSharedContext sharedContext) : base(sharedContext)
        {
        }

        public override void OnStateEnter()
        {
            Debug.Log($"[{GetType().Name}][{MethodBase.GetCurrentMethod().Name}] - OK");
            if (_sharedContext == null)
            {
                return;
            }

            _sharedContext.MovementManager.StopMovement();
        }

        public override void Execute()
        {
        }
    }
}