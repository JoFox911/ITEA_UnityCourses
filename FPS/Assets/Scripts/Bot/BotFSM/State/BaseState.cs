namespace BotLogic
{
    public abstract class BaseState
    {
        public virtual void OnStateEnter()
        {
        }

        public abstract void Execute();

        public virtual void OnStateExit()
        {
        }
    }
    public abstract class BaseState<T> : BaseState
        where T : class
    {
        protected T _sharedContext;
        protected IStateSwitcher<T> _stateSwitcher;

        public BaseState(T sharedContext)
        {
            _sharedContext = sharedContext;
        }

        public void InitSwitcher(IStateSwitcher<T> stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }
    }
}
