namespace Terminal;

public class InputSystem : IDisposable
{
    // The Event
    public event Action<ConsoleKeyInfo>? OnKeyPress;
    
    private readonly CancellationTokenSource cts = new();
    private Task? _listenerTask;
    
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}