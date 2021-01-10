using System;

namespace BotLogic
{
    public interface IStateSwitcher<T> where T : class
    {
        void Switch(Type nextState);
    }
}