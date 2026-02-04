using Terminal.Components;

namespace Terminal;

public class Terminal
{
    public const byte ScreenWidth = 192;
    public const byte ScreenHeight = 37;

    public readonly char[,] Screen = new char[ScreenHeight, ScreenWidth];

    public List<Component> Components = new();

    public Terminal()
    {
        for (byte i = 0; i < ScreenHeight; i++)
        {
            for (byte j = 0; j < ScreenWidth; j++)
            {
                Screen[i,j] = '░';
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
        for (byte i = 0; i < ScreenHeight; i++)
        {
            for (byte j = 0; j < ScreenWidth; j++)
            {
                Console.Write(Screen[i,j]);
            }
            Console.WriteLine();
        }
    }
    
}