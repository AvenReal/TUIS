namespace Terminal.Components;

public class BoxedComponent : Component
{
    public BoxedComponent(Terminal terminal, byte width, byte height, byte posX, byte posY) : base(terminal, width, height, posX, posY)
    {
        
    }

    public override void Draw()
    {
        for (byte i = 0; i < Height; i++)
        {
            for (byte j = 0; j < Width; j++)
            {
                Display[i, j] = Boxes.Light(i, j, Height, Width) ?? Display[i, j];
            }
        }
        base.Draw();
    }
}

public static class Boxes
{
    public static char? Light(byte y, byte x, byte height, byte width)
    {
        if (y == 0)
        {
            if(x == 0)
                return '┌';
            
            if(x == width - 1)
                return '┐';
            
            return '─';
        }
        
        if (y == height - 1)
        {
            if(x == 0)
                return '└';
            
            if(x == width - 1)
                return '┘';
            
            return '─';
        }
        
        if(x == 0 || x == width - 1)
            return '│';
        
        return null;
    }
}