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
        set
        {
            if (value)
            {
                try // awful, need to go away
                {
                    Draw();
                }
                catch
                {
                }
            }

            field = value;
        }
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

    public Component(Terminal terminal, int width, int height, int posY, int posX)
    {
        Terminal = terminal;
        Terminal.Components.Add(this);

        Width = width;
        Height = height;
        PosX = posX;
        PosY = posY;

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