namespace Terminal.Components;

public class Component
{
    public Terminal Terminal;
    
    public readonly byte Width;
    public readonly byte Height;

    protected byte PosX
    {
        set
        {
            if (value + Width > Terminal.ScreenWidth)
            {
                throw new ArgumentOutOfRangeException(nameof(PosY),
                    $"Max value of {Terminal.ScreenWidth} but got PosX ({value}) + Width ({Width}) = {value + Width}.");
            }
            field = value;
        }
        get;
    }
    protected byte PosY
    {
        set
        {
            if (value + Height > Terminal.ScreenHeight)
            {
                throw new ArgumentOutOfRangeException(nameof(PosY), 
                    $"Max value of {Terminal.ScreenHeight} but got PosY ({value}) + Height ({Height}) = {value + Height}.");
            }
            field = value;
        }
        get;
    }
    
    public readonly char[,] Display;

    public Component(Terminal terminal, byte width, byte height, byte posX, byte posY)
    {
        Terminal = terminal;
        Width = width;
        Height = height;
        PosX = posX;
        PosY = posY;
        Display = new char[Height,Width];
    }

    public virtual void Draw()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Terminal.Screen[PosY + i, PosX + j] = Display[i,j];
            }
        }
    }
}