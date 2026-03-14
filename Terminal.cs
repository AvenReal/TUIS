using System.Text;
using TUIS.Components;
using TUIS.Systems;

namespace TUIS;

/// <summary>
/// A Terminal is the main object of this TUI system. It will hold each <see cref="Component"/> that will be drawn on the screen.
/// </summary>
public class Terminal
{
    public readonly int Width;
    public readonly int Height;
    public readonly InputSystem InputSystem = new();
    public readonly TimeSystem TimeSystem = new();

    public readonly List<Component> Components = [];

    private readonly string[,] _screen;

    public bool[] NeedReDraw;

    public Terminal(int width, int height)
    {
        Width = width;
        Height = height;
        TimeSystem.AddTimedEvent((_, _) => { Draw(); });
        TimeSystem.AddTimedEvent((_, _) => { UpdateScreen(); });
        _screen = new string[Height, Width];
        NeedReDraw = new bool[Height];

        for (int i = 0; i < Height; i++)
        {
            NeedReDraw[i] = true;
            for (int j = 0; j < Width; j++)
            {
                _screen[i, j] = " ";
            }
        }
    }

    /// <summary>
    /// This method will automatically be called to re-draw each row of the <see cref="_screen"/> that have been modified.  
    /// </summary>
    private void UpdateScreen()
    {
        for (int i = 0; i < Height; i++)
        {
            if (NeedReDraw[i])
            {
                NeedReDraw[i] = false;

                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < Width; j++)
                {
                    sb.Append(_screen[i, j]);
                }

                Console.Write($"\u001b[{i};{1}H{sb.ToString()}");
            }
        }
    }

    /// <summary>
    /// This method will automatically be called and will call <see cref="Component.Draw"/> on each <see cref="Components"/>. 
    /// </summary>
    private void Draw()
    {
        foreach (var component in Components)
        {
            component.Draw();
        }
    }

    /// <summary>
    /// This method will be called by <see cref="TUIS.Components.Masks.Mask.DrawChar"/> to correctly update a character of the internal <see cref="_screen"/> of the <see cref="Terminal"/>. 
    /// </summary>
    /// <param name="y">The y coordinate of the char to update (0 = top).</param>
    /// <param name="x">The x coordinate of the char to update (0 = left).</param>
    /// <param name="c">The new char.</param>
    /// <param name="textColor">The <see cref="TextColor"/> the <see cref="c"/> will be drawn.</param>
    /// <param name="backgroundColor">The <see cref="BackgroundColor"/> the <see cref="c"/> will be drawn.</param>
    /// <param name="textDecoration">The <see cref="TextDecoration"/> the <see cref="c"/> will be drawn.</param>
    public void DrawChar(int y, int x, char c, TextColor textColor, BackgroundColor backgroundColor,
        TextDecoration textDecoration)
    {
        string oldValue = _screen[y, x];
        _screen[y, x] = $"\e[{(int)(backgroundColor)}m\e[{(int)(textDecoration)};{(int)(textColor)}m{c}";
        NeedReDraw[y] = NeedReDraw[y] || oldValue != _screen[y, x];
    }

    /// <summary>
    /// Get the <see cref="TextColor"/> using th ANSI color code.
    /// </summary>
    public enum TextColor
    {
        Black = 30,
        Red = 31,
        Green = 32,
        Yellow = 33,
        Blue = 34,
        Purple = 35,
        Cyan = 36,
        White = 37,
    }

    /// <summary>
    /// Get the <see cref="BackgroundColor"/> using the ANSI color code.
    /// </summary>
    public enum BackgroundColor
    {
        Black = 40,
        Red = 41,
        Green = 42,
        Yellow = 43,
        Blue = 44,
        Purple = 45,
        Cyan = 46,
        White = 47,
        None = 0
    }


    /// <summary>
    /// Get the <see cref="TextDecoration"/> using the ANSI color code.
    /// </summary>
    public enum TextDecoration
    {
        Default = 0,
        Bold = 1,
        Underline = 4,
    }


    public void Clear()
    {
        Console.Clear();
        foreach (var component in Components)
        {
            component.NeedReDraw = true;
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