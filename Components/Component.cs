using TUIS.Components.Masks;

namespace TUIS.Components;

public class Component
{
    public Terminal? Terminal;

    public int Width
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public int Height
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public int PosX
    {
        set
        {
            NeedRedraw = true;
            field = value;
        }
        get;
    }
    public int PosY
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

    public Component(Terminal terminal, int width, int height, int posY, int posX)
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
    
    public Component(int width, int height, int posY, int posX, IEnumerable<Mask>? masks = null)
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