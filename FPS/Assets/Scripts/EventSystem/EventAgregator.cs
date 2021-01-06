using System;

public static class EventAgregator
{
    public static void Subscribe<T>(Action<object, T> eventCallback)
    {
        EventInner<T>.Event += eventCallback;
    }

    public static void Unsubscribe<T>(Action<object, T> eventCallback)
    {
        EventInner<T>.Event -= eventCallback;
    }

    public static void Post<T>(object sender, T eventData)
    {
        EventInner<T>.Post(sender, eventData);
    }

    internal static class EventInner<T>
    {
        internal static event Action<object, T> Event;

        public static void Post(object sender, T eventData)
        {
            Event?.Invoke(sender, eventData);
        }
    }

}
