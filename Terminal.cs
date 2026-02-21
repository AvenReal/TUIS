using TUIS.Components;
using TUIS.Systems;

namespace TUIS;

public class Terminal
{
    public readonly InputSystem InputSystem = new();
    public readonly TimeSystem TimeSystem = new();

    public readonly List<Component> Components = [];

    public bool NeedRedraw = true;

    public Terminal()
    {
        TimeSystem.AddTimedEvent((_, _) => { Draw(); });
    }

    public void Draw()
    {
        if (!NeedRedraw)
            return;

        NeedRedraw = false;

        foreach (var component in Components)
        {
            component.Draw();
        }
        // Console.CursorVisible = false;
    }

    public void Clear()
    {
        Console.Clear();
        foreach (var component in Components)
        {
            component.NeedRedraw = true;
        }
    }

    public void Start(Action<Terminal>? onStart = null)
    {
        InputSystem.Start();
        TimeSystem.Start();
        onStart?.Invoke(this);
        while (true)
        {
        }
    }
}