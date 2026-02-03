namespace Terminal.Components;

public class BoxedComponent : Component
{
    public Boxes.Type Box = Boxes.Type.None;
    
    public BoxedComponent(Terminal terminal, byte width, byte height, byte posX, byte posY) : base(terminal, width, height, posX, posY)
    {
        
    }

    public override void Draw()
    {
        for (byte i = 0; i < Height; i++)
        {
            for (byte j = 0; j < Width; j++)
            {
                Display[i, j] = Boxes.Box(Box, j, i, PosX, PosY, Height, Width) ?? Display[i, j];
            }
        }
        base.Draw();
    }
}

public static class Boxes
{
    public enum Type
    {
        Light,
        Bold,
        None
    }

    public static char? Box(Type type, byte x, byte y, byte posX, byte posY, byte height, byte width)
    {
        switch (type)
        {
            case Type.Light:
                return Light(x, y, posX, posY, height, width);
            case Type.Bold:
                return Bold(x, y, posX, posY, height, width);
            case Type.None:
                return null;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public static char? Light(byte x, byte y, byte posX, byte posY, byte height, byte width)
    {
        if (y == posY)
        {
            if(x == posX)
                return '┌';
            
            if(x == width - 1)
                return '┐';
            
            return '─';
        }
        
        if (y == height - 1)
        {
            if(x == posX)
                return '└';
            
            if(x == width - 1)
                return '┘';
            
            return '─';
        }
        
        if(x == posX || x == width - 1)
            return '│';
        
        return null;
    }
    
    public static char? Bold(byte x, byte y, byte posX, byte posY, byte height, byte width)
    {
        return x == posX || x == width-1 || y == posY || y == height-1 ? '█' : null;
    }
}