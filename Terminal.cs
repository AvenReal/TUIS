using Terminal.Components;

namespace Terminal;

public class Terminal
{
    //public const byte ScreenWidth = 192;
    //public const byte ScreenHeight = 37;
    

    public List<Component> Components = new();

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
        foreach (Component component in Components)
        {
            component.Draw();
        }
        Console.CursorVisible = false;
    }

    public void Clear()
    {
        Console.Clear();
        foreach (Component component in Components)
        {
            component.NeedRedraw = true;
        }
    }

}