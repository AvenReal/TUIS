namespace TUIS.Components.Masks;

/// <summary>
/// A Mask must be attached to a <see cref="Component"/> and adds a layer of drawings on top of it.
/// Masks will be drawn in the same order as they have been added to the <see cref="Component.Masks"/> list.  
/// </summary>
public abstract class Mask
{
    /// <summary>
    /// The <see cref="Component"/> the <see cref="Mask"/> is attached to.
    /// </summary>
    public readonly Component Component;

    /// <summary>
    /// Helps to optimise the drawing of components, if NeedReDraw == false, the <see cref="Mask"/> won't be re-<see cref="Draw"/>n.
    /// if NeedReDraw is set to true, it will automatically set the <see cref="Components.Component.NeedReDraw"/> to true.
    /// </summary>
    public bool NeedReDraw
    {
        get;
        set
        {
            field = value;
            if (value)
                Component.NeedReDraw = true;
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
            NeedReDraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Keeps the current <see cref="Terminal.TextColor"/> of the <see cref="Mask"/>.
    /// </summary>
    public Terminal.TextColor Color
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Keeps the current <see cref="Terminal.BackgroundColor"/> of the <see cref="Mask"/>.
    /// </summary>
    public Terminal.BackgroundColor Background
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Keeps the current <see cref="Terminal.TextDecoration"/> of the <see cref="Mask"/>.
    /// </summary>
    public Terminal.TextDecoration Decoration
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    /// <summary>
    /// A Mask goes in a <see cref="Component"/> and will add characters to print on it following custom rules: <see cref="Behaviour"/>.
    /// </summary>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
    public Mask(Component component, bool isVisible = true, Terminal.TextColor color = Terminal.TextColor.White,
        Terminal.BackgroundColor background = Terminal.BackgroundColor.None,
        Terminal.TextDecoration decoration = Terminal.TextDecoration.Default)
    {
        Component = component;
        Component.Masks.Add(this);

        IsVisible = isVisible;
        Color = color;
        Background = background;
        Decoration = decoration;

        NeedReDraw = true;
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
        if (!NeedReDraw || !IsVisible)
            return;

        NeedReDraw = false;
        Behaviour();
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
    protected void DrawChar(int y, int x, char? c, Terminal.TextColor? textColor = null,
        Terminal.BackgroundColor? backgroundColor = null,
        Terminal.TextDecoration? textDecoration = null)
    {
        if (c == null)
            return;


        Component.Terminal.DrawChar(y + Component.PosY, x + Component.PosX, (char)c, textColor ?? Color,
            backgroundColor ?? Background,
            textDecoration ?? Decoration);
    }
}