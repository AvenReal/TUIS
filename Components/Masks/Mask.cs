namespace TUIS.Components.Masks;

public abstract class Mask
{
    public Component Component;

    /// <summary>
    /// Helps to optimise the drawing of components, if NeedRedraw == false, the <see cref="Mask"/> won't be re-<see cref="Draw"/>n. 
    /// </summary>
    public bool NeedRedraw
    {
        get;
        set
        {
            field = value;
            if (value)
                Component.NeedRedraw = true;
        }
    }

    /// <summary>
    /// Represent the visibility of the <see cref="Mask"/>.
    /// </summary>
    public bool IsVisible
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Keeps the current <see cref="TextColor"/> of the <see cref="Mask"/>.
    /// </summary>
    public TextColor Color
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Keeps the current <see cref="BackgroundColor"/> of the <see cref="Mask"/>.
    /// </summary>
    public BackgroundColor Background
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Keeps the current <see cref="TextDecoration"/> of the <see cref="Mask"/>.
    /// </summary>
    public TextDecoration Decoration
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    /// <summary>
    /// A Mask goes in a <see cref="component"/> and will add characters to print on it following custom rules: <see cref="Behaviour"/>.
    /// </summary>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
    public Mask(Component component, bool isVisible = true, TextColor color = TextColor.White,
        BackgroundColor background = BackgroundColor.None, TextDecoration decoration = TextDecoration.Default)
    {
        Component = component;
        Component.Masks.Add(this);

        IsVisible = isVisible;
        Color = color;
        Background = background;
        Decoration = decoration;

        NeedRedraw = true;
    }

    /// <summary>
    /// This method is what makes the mask unique, it will describe that to print and where using the <see cref="DrawChar"/>. The method will automatically be called when needed. 
    /// </summary>
    protected abstract void Behaviour();


    /// <summary>
    /// This method will be called if the mask has changed and is visible.
    /// It calls the <see cref="Behaviour"/> method of the mask.
    /// </summary>
    public void Draw()
    {
        if (!NeedRedraw || !IsVisible)
            return;

        NeedRedraw = false;
        Behaviour();
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

    /// <summary>
    /// Used in the <see cref="Behaviour"/> method of <see cref="Mask"/>s to properly print a character using relative coordinates.
    /// </summary>
    /// <param name="y">Coordinate on the y-axis (0 = top) relative to the <see cref="Component"/>.</param>
    /// <param name="x">Coordinate on the x-axis (0 = left) relative to the <see cref="Component"/>.</param>
    /// <param name="c">The character to print (null = no print).</param>
    /// <param name="textColor">The color which the <paramref name="c"/> will be printed. null = the default background color of the mask.</param>
    /// <param name="backgroundColor">The background color which the <paramref name="c"/> will be printed. null = the default background color of the mask.</param>
    /// <param name="textDecoration">The decoration with which the <paramref name="c"/> will be printed. null = the default decoration of the mask.</param>
    protected void DrawChar(int y, int x, char? c, TextColor? textColor = null, BackgroundColor? backgroundColor = null,
        TextDecoration? textDecoration = null)
    {
        if (c == null)
            return;

        Console.Write(
            $"\e[{(int)(backgroundColor ?? Background)}m\e[{(int)(textDecoration ?? Decoration)};{(int)(textColor ?? Color)}m\u001b[{y + Component.PosY};{x + Component.PosX}H{c}");
    }
}