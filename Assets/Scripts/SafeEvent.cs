using System.Collections.Generic;

public struct Unit : System.IEquatable<Unit>
{
    public static Unit Default;

    public bool Equals(Unit other)
    {
        return true;
    }
}


public class SafeEvent<T, TArg> where TArg : SafeEventArg
{
    public delegate T SafeFunc(TArg arg);

    private readonly Dictionary<SafeEventHandler<T, TArg>, SafeFunc> _CallbackMap = new Dictionary<SafeEventHandler<T, TArg>, SafeFunc>();

    public SafeEvent()
    {

    }


    public SafeEventHandler<T, TArg> Register(SafeFunc eventCallback)
    {
        var handler = new SafeEventHandler<T, TArg>(this);
        _CallbackMap.Add(handler, eventCallback);
        return handler;
    }


    public void Unregister(in SafeEventHandler<T, TArg> handler)
    {
        if (!_CallbackMap.ContainsKey(handler))
        {
            return;
        }
        _CallbackMap.Remove(handler);
    }


    public void Execute(params object[] args)
    {
        foreach (var method in _CallbackMap.Values)
        {
            method?.DynamicInvoke();
        }
    }
}


public class SafeEventHandler<T, TArg> where TArg : SafeEventArg
{
    private readonly SafeEvent<T, TArg> _Event;

    public SafeEventHandler(SafeEvent<T, TArg> safeEvent)
    {
        _Event = safeEvent;
    }


    ~SafeEventHandler()
    {
        if (_Event == null)
        {
            return;
        }
        _Event.Unregister(this);
    }
}


public class SafeEventArg
{
    public static T CreateArg<T>() where T : SafeEventArg, new()
    {
        return new T();
    }
}