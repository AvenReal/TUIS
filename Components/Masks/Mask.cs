namespace Terminal.Components.Masks;

public abstract class Mask
{
    public Component? Component;
    
    public bool NeedRedraw;
    

    public bool IsVisible
    {
        get;
        set
        {
            NeedRedraw = true;
            Component?.NeedRedraw = true;
            field = value;
        }
    }
    
    public Mask(Component component)
    {
        Component = component;
        Component.Masks.Add(this);
        
        
        IsVisible = true;
    }

    public Mask()
    {
        IsVisible = true;
    }
    
    protected abstract void Behaviour();

    public void Draw()
    {
        if(Component == null)
            throw new NullReferenceException("Component of Mask is null");
        
        
        if(NeedRedraw && IsVisible)
        {
            Behaviour();
            NeedRedraw = false;
        }
    }

    protected void DrawChar(byte y, byte x, char? c)
    {
        if(c == null)
            return;
        
        Console.Write($"\u001b[{y + Component!.PosY};{x + Component.PosX}H{c}");
    }
}