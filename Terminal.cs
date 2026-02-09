using Terminal.Components;

namespace Terminal;

public class Terminal
{
    public InputSystem InputSystem = new();
    public TimeSystem TimeSystem = new();
    
    public char[,] Display = new char[Console.BufferHeight, Console.BufferWidth];
    
    public readonly List<Component> Components = [];

    public Terminal(IEnumerable<Component>? components = null)
    {
        if (components != null)
        {
            foreach (Component component in components)
            {
                component.Terminal = this;
                Components.Add(component);
            }
        }
    }

    public void Draw()
    {
        foreach (var component in Components)
        {
            component.Draw();
        }
        Console.CursorVisible = false;
    }

    public void Clear()
    {
        Console.Clear();
        foreach (var component in Components)
        {
            component.NeedRedraw = true;
        }
    }

    public void Start()
    {
        InputSystem.Start();
        TimeSystem.Start();
        while (true)
        {
            
        }
    }

}