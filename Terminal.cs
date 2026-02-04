using Terminal.Components;

namespace Terminal;

public class Terminal
{
    public const byte ScreenWidth = 192;
    public const byte ScreenHeight = 37;
    

    public List<Component> Components = new();

    public Terminal()
    {
        
    }

    public void Draw()
    {
        foreach (Component component in Components)
        {
            component.Draw();
        }
        
        Console.Write($"\u001b[47;0H");
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