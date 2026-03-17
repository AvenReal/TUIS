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
            field = value;

            foreach (var mask in Masks)
            {
                mask.Screen = new List<List<string?>>(Height);
                for (int i = 0; i < Height; i++)
                {
                    mask.Screen.Add(new List<string?>(Width));
                    for (int j = 0; j < Width; j++)
                    {
                        mask.Screen[i].Add(null);
                    }
                }
            }

            NeedReCalculate = true;
        }
    }

    public int Height
    {
        get;
        set
        {
            foreach (var mask in Masks)
            {
                field = value;
                mask.Screen = new List<List<string?>>(Height);
                for (int i = 0; i < Height; i++)
                {
                    mask.Screen.Add(new List<string?>(Width));
                    for (int j = 0; j < Width; j++)
                    {
                        mask.Screen[i].Add(null);
                    }
                }
            }

            NeedReCalculate = true;
        }
    }

    public int PosX
    {
        set
        {
            field = value;
            NeedReCalculate = true;
        }
        get;
    }

    public int PosY
    {
        set
        {
            field = value;
            NeedReCalculate = true;
        }
        get;
    }

    public readonly List<Mask> Masks = new();

    public bool NeedReCalculate
    {
        get;
        set
        {
            field = value;
            Terminal.NeedReCalculate = true;
        }
    }

    public bool NeedReDraw
    {
        get;
        set
        {
            if (value)
                Terminal.NeedReDraw = true;
            field = value;
        }
    }

    public bool IsVisible
    {
        set
        {
            if (!value)
                field = value;
            NeedReCalculate = true;
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
        NeedReCalculate = true;
    }

    public void Calculate()
    {
        if (!NeedReCalculate)
            return;

        NeedReCalculate = false;

        foreach (Mask mask in Masks)
        {
            mask.Calculate();
        }
    }

    public string? DrawChar(int y, int x)
    {
        if (y < PosY || y >= PosY + Height || x < PosX || x >= PosX + Width)
            return null;

        y -= PosY;
        x -= PosX;

        string? res = null;
        foreach (var mask in Masks)
        {
            res = mask.DrawChar(y, x) ?? res;
        }

        return res;
    }
}