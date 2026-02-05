using Terminal.Components.Masks;

namespace Terminal.Components;

public class Component
{
    public Terminal Terminal;

    public byte Width
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public byte Height
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public byte PosX
    {
        set
        {
            NeedRedraw = true;
            field = value;
        }
        get;
    }
    public byte PosY
    {
        set
        {
            NeedRedraw = true;
            field = value;
        }
        get;
    }
    
    public readonly char[,] Display;

    public readonly List<Mask> Masks = new();
    
    public bool NeedRedraw = true;
    public bool IsVisible
    {
        set
        {
            if(!value)
                NeedRedraw = true;
            field = value;
        }
        get;
    }

    public Component(Terminal terminal, byte width, byte height, byte posY, byte posX)
    {
        Terminal = terminal;
        Terminal.Components.Add(this);
        Width = width;
        Height = height;
        PosX = posX;
        PosY = posY;
        Display = new char[Height,Width];
        IsVisible = true;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Display[i,j] = ' ';
            }
        }
    }
    

    

    public void Draw()
    {
        if(!NeedRedraw)
            return;
        
        foreach (Mask mask in Masks)
        {
            mask.Draw();
        }
        
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Console.Write($"\u001b[{PosY + i};{PosX + j}H{(IsVisible ? Display[i, j] : ' ')}");
            }
        }
        
        NeedRedraw = false;
    }
}