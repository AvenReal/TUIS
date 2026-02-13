namespace Terminal.Components.Masks;

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
    
    public BackgroundMask(char backgroundChar = ' ')
    {
        BackgroundChar = backgroundChar;
    }
    
    protected override void Behaviour()
    {
        for (byte i = 0; i < Component!.Height; i++)
        {
            for (byte j = 0; j < Component.Width; j++)
            {
                DrawChar(i, j, BackgroundChar);
            }
        }
    }
}