namespace Terminal.Components.Masks;

public abstract class Mask
{
    public Component? Component;

    public bool NeedRedraw
    {
        get;
        set
        {
            field = value;
            if(value)
                Component?.NeedRedraw = true;
        }
    }
    

    public bool IsVisible
    {
        get;
        set
        {
            NeedRedraw = true;
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
            NeedRedraw = false;
            Behaviour();
        }
    }


    public enum TextColor : byte
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

    public enum BackgroundColor : byte
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
    
    protected void DrawChar(byte y, byte x, char? c, TextColor textColor = TextColor.White, BackgroundColor backgroundColor = BackgroundColor.None)
    {
        if(c == null)
            return;
        
        
         
        Console.Write($"\e[{ (byte) backgroundColor}m\e[0;{ (byte) textColor }m\u001b[{y + Component!.PosY};{x + Component.PosX}H{c}");
    }
}