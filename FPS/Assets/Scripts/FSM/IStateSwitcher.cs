using System;


public interface IStateSwitcher<T> where T : class
{
    void Switch(Type nextState);
}