using Terminal.Components;

namespace Terminal;

public class Terminal
{
    public readonly InputSystem InputSystem = new();
    public readonly TimeSystem TimeSystem = new();
    
    public readonly List<Component> Components = [];
    
    public bool NeedRedraw = true;

    public Terminal(IEnumerable<Component>? components = null)
    {
        TimeSystem.AddTimedEvent((_, _) => {Draw();});
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
        if (!NeedRedraw)
            return;
        
        NeedRedraw = false;
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