using Terminal.Components.Masks;

namespace Terminal.Components;

public class Component
{
    public Terminal? Terminal;

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
    

    public readonly List<Mask> Masks = new();



    public bool NeedRedraw
    {
        get;
        set
        {
            if(value)
                Terminal?.NeedRedraw = true;
            field = value;
        }
    }
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
        NeedRedraw = true;
        Terminal = terminal;
        Terminal.Components.Add(this);
        Width = width;
        Height = height;
        PosX = posX;
        PosY = posY;
        
        IsVisible = true;
        
    }
    
    public Component(byte width, byte height, byte posY, byte posX, IEnumerable<Mask>? masks = null)
    {
        NeedRedraw = true;
        Width = width;
        Height = height;
        PosX = posX;
        PosY = posY;
        IsVisible = true;
        

        if (masks != null)
        {
            foreach (Mask mask in masks)
            {
                mask.Component = this;
                Masks.Add(mask);
            }
        }
    }
    
    public void Draw()
    {
        if(!NeedRedraw)
            return;
        
        
        NeedRedraw = false;
        
        foreach (Mask mask in Masks)
        {
            mask.Draw();
        }
        
        
    }
}