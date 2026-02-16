namespace TUIS.Components.Masks;

public class BackgroundMask : Mask
{
    public char BackgroundChar
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }
    
    public BackgroundMask(Component component, char backgroundChar = ' ') : base(component)
    {
        BackgroundChar = backgroundChar;
    }
    
    protected override void Behaviour()
    {
        for (int i = 0; i < Component.Height; i++)
        {
            for (int j = 0; j < Component.Width; j++)
            {
                DrawChar(i, j, BackgroundChar);
            }
        }
    }
}