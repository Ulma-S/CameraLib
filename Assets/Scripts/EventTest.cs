using UnityEngine;

public struct MyArg : ISafeEventArg
{
    public string Name { get; private set; }

    public MyArg(string name)
    {
        Name = name;
    }
}


public class EventTest : MonoBehaviour
{
    private readonly SafeEvent<Unit, MyArg> _Event = new ();
    private SafeEventHandler<Unit, MyArg> _Handler;

    private void Start()
    {
        _Handler = _Event.Register(args => 
        {
            Debug.Log(args.Name);
            return Unit.Default;
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _Event.Execute(new MyArg("Kuma"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _Event.Unregister(_Handler);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(gameObject);
        }
    }
}
