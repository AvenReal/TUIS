namespace TUIS.Systems;

public class InputSystem : IDisposable
{
    // The Event
    public event Action<ConsoleKeyInfo>? OnKeyPress;
    
    private readonly CancellationTokenSource _cts = new();
    private Task? _listenerTask;

    public void Start()
    {
        _listenerTask = Task.Run(() => ListenLoop(_cts.Token),  _cts.Token);
    }

    private void ListenLoop(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                OnKeyPress?.Invoke(key);
            }
        }
    }
    
    public void Dispose()
    {
        _cts.Cancel();
        _listenerTask?.Wait();
        _cts.Dispose();
    }
}