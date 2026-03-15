using TUIS.Components.Masks;

namespace TUIS.Components;

public class Component
{
    public Terminal Terminal;

    public int Width
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    public int Height
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    public int PosX
    {
        set
        {
            NeedReDraw = true;
            field = value;
        }
        get;
    }

    public int PosY
    {
        set
        {
            NeedReDraw = true;
            field = value;
        }
        get;
    }

    public readonly List<Mask> Masks = new();

    public bool NeedReDraw
    {
        get;
        set { field = value; }
    }

    public bool IsVisible
    {
        set
        {
            if (!value)
                NeedReDraw = true;
            field = value;
        }
        get;
    }

    /// <summary>
    /// A Component will represent an element drawn on the <see cref="Terminal"/>. 
    /// </summary>
    /// <param name="terminal">The <see cref="Terminal"/> the Component will be attached to.</param>
    /// <param name="width">The width of the component (-1 = <see cref="Terminal.Width"/>).</param>
    /// <param name="height">The height of the component (-1 = <see cref="Terminal.Height"/>).</param>
    /// <param name="posY">The position on the y axis of the top left most char of the component (0 = top, -1 = will ceter the component).</param>
    /// <param name="posX">The position on the x axis of the top left most char of the component (0 = left, -1 = will ceter the component).</param>
    public Component(Terminal terminal, int width, int height, int posY, int posX)
    {
        Terminal = terminal;
        Terminal.Components.Add(this);

        Width = width == -1 ? Terminal.Width : width;
        Height = height == -1 ? Terminal.Height : height;
        PosX = posX == -1 ? (Terminal.Width - width) / 2 : posX;
        PosY = posY == -1 ? (Terminal.Height - height) / 2 : posY;

        IsVisible = true;
        NeedReDraw = true;
    }

    public void Draw()
    {
        if (!NeedReDraw)
            return;


        NeedReDraw = false;

        foreach (Mask mask in Masks)
        {
            mask.Draw();
        }
    }
}