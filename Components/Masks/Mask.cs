namespace TUIS.Components.Masks;

public abstract class Mask
{
    public Component Component;

    public bool NeedRedraw
    {
        get;
        set
        {
            field = value;
            if(value)
                Component.NeedRedraw = true;
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

    public TextColor Color
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public BackgroundColor Background
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public TextDecoration Decoration
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public byte HorizontalPadding
    {
        get;
        set
        {
            field = value;
            NeedRedraw = true;
        }
    }

    public byte VerticalPadding
    {
        get;
        set
        {
            field = value;
            NeedRedraw = true;
        }
    }
    

    public Mask(Component component, bool isVisible = true, TextColor color = TextColor.White, BackgroundColor background = BackgroundColor.None, TextDecoration decoration = TextDecoration.Default, byte horizontalPadding = 0, byte verticalPadding = 0)
    {
        Component = component;
        Component.Masks.Add(this);
        
        IsVisible = isVisible;
        Color = color;
        Background = background;
        Decoration = decoration;
        HorizontalPadding = horizontalPadding;
        VerticalPadding = verticalPadding;
    }


    protected abstract void Behaviour();

    public void Draw()
    {
        if (!NeedRedraw || !IsVisible) 
            return;
        
        NeedRedraw = false;
        Behaviour();
    }


    public enum TextColor : int
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

    public enum BackgroundColor : int
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

    public enum TextDecoration : int
    {
        Default = 0,
        Bold = 1,
        Underline = 4,
    }
    
    protected void DrawChar(int y, int x, char? c, TextColor? textColor = null, BackgroundColor? backgroundColor = null, TextDecoration? textDecoration = null)
    {
        if(c == null)
            return;
        
        Console.Write($"\e[{ (int)(backgroundColor ?? Background) }m\e[{ (int)(textDecoration ?? Decoration) };{ (int)(textColor ?? Color) }m\u001b[{y + Component.PosY};{x + Component.PosX}H{c}");
    }
}