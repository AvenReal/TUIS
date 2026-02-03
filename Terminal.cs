using Terminal.Components;

namespace Terminal;

public class Terminal
{
    public const byte ScreenWidth = 192;
    public const byte ScreenHeight = 37;

    public readonly char[,] Screen = new char[ScreenHeight, ScreenWidth];

    public List<Component> Components = new List<Component>();

    public Terminal()
    {
        for (int i = 0; i < ScreenHeight; i++)
        {
            for (int j = 0; j < ScreenWidth; j++)
            {
                Screen[i,j] = '_';
            }
        }
    }

    public void PrintScreen()
    {
        foreach (Component component in Components)
        {
            component.Draw();
        }
        Console.Clear();
        for (int i = 0; i < ScreenHeight; i++)
        {
            for (int j = 0; j < ScreenWidth; j++)
            {
                Console.Write(Screen[i,j]);
            }
            Console.WriteLine();
        }
    }
    
}