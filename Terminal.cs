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

    public bool NeedReDraw = false;

    private readonly string[,] _screen;
    private readonly bool[,] _updatedPixels;

    /// <summary>
    /// The terminal is the class that holds every <see cref="Component"/>s together, it also holds the <see cref="InputSystem"/> and the <see cref="TimeSystem"/>.
    /// You should only have 1 instance of a terminal in your program. 
    /// </summary>
    /// <param name="width">With of the terminal (default = -1 = <see cref="Console.WindowWidth"/>) (/!\ cannot be changed).</param>
    /// <param name="height">Height of the terminal (default = -1 = <see cref="Console.WindowHeight"/>) (/!\ cannot be changed).</param>
    public Terminal(int width = -1, int height = -1)
    {
        Width = width == -1 ? Console.WindowWidth : width;
        Height = height == -1 ? Console.WindowHeight : height;

        TimeSystem.AddTimedEvent((_, _) => { Draw(); });
        _screen = new string[Height, Width];
        _updatedPixels = new bool[Height, Width];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                _updatedPixels[i, j] = true;
            }
        }
    }

    // #################################################################################################################
    //                                              Display Methods
    // #################################################################################################################

    /// <summary>
    /// This method will automatically be called and will call <see cref="Component.Draw"/> on each <see cref="Components"/>. 
    /// </summary>
    private void Draw()
    {
        if (!NeedReDraw)
            return;

        NeedReDraw = true;

        foreach (var component in Components)
        {
            component.Draw();
        }

        UpdateScreen();
    }

    /// <summary>
    /// This method will be called by <see cref="TUIS.Components.Masks.Mask.DrawChar"/> to correctly update a character of the internal <see cref="_screen"/> of the <see cref="Terminal"/>. 
    /// </summary>
    /// <param name="y">The y coordinate of the char to update (0 = top).</param>
    /// <param name="x">The x coordinate of the char to update (0 = left).</param>
    /// <param name="c">The new char to draw.</param>
    /// <param name="textColor">The <see cref="TextColor"/> the <paramref name="c"/> will be drawn.</param>
    /// <param name="backgroundColor">The <see cref="BackgroundColor"/> the <paramref name="c"/> will be drawn.</param>
    /// <param name="textDecoration">The <see cref="TextDecoration"/> the <paramref name="c"/> will be drawn.</param>
    public void DrawChar(int y, int x, char c, TextColor textColor, BackgroundColor backgroundColor,
        TextDecoration textDecoration)
    {
        string oldValue = _screen[y, x];
        string newValue = $"\e[{(int)(backgroundColor)}m\e[{(int)(textDecoration)};{(int)(textColor)}m{c}";
        if (oldValue != newValue)
        {
            _screen[y, x] = newValue;
            _updatedPixels[y, x] = true;
        }
    }

    public void DrawChar(int y, int x, char c, (int r, int g, int b) textColor, BackgroundColor backgroundColor,
        TextDecoration textDecoration)
    {
        string oldValue = _screen[y, x];
        string newValue =
            $"\e[{(int)(backgroundColor)}m\e[{(int)(textDecoration)};0m\e[38;2;{textColor.r};{textColor.g};{textColor.b}m{c}";
        if (oldValue != newValue)
        {
            _screen[y, x] = newValue;
            _updatedPixels[y, x] = true;
        }
    }

    private void UpdateScreen()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (_updatedPixels[i, j])
                {
                    _updatedPixels[i, j] = false;
                    Console.Write($"\u001b[{i};{j}H{_screen[i, j]}");
                }
            }
        }
    }

    // #################################################################################################################
    //                                              Enums
    // #################################################################################################################

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

    // #################################################################################################################
    //                                              Misc
    // #################################################################################################################

    public virtual void AddComponent(Component component)
    {
        Components.Add(component);
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
            Thread.Sleep(10000);
        }
    }
}