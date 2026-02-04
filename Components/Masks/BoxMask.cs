namespace Terminal.Components.Masks;

public class BoxMask : Mask
{
    public Type BoxType;
    
    public BoxMask(Component component, Type type) : base(component, delegate(Component component, Mask mask)
    {
        BoxMask boxMask = (BoxMask)mask;
        for (byte i = 0; i < component.Height; i++)
        {
            for (byte j = 0; j < component.Width; j++)
            {
                component.Display[i, j] = boxMask.GetBox(boxMask.BoxType, j, i, component.Height, component.Width) ?? component.Display[i, j];
            }
        }
    })
    {
        BoxType = type;
    }
    
    public enum Type
    {
        Light,
        Bold,
        ExtraBold,
        None
    }

    private Dictionary<Type, (char west, char east, char north, char south, char northWest, char northEast, char southWest, char southEast)> _boxChars = new()
        {
            { Type.Light,     ('│', '│', '─', '─', '┌', '┐', '└', '┘') },
            { Type.Bold,      ('║', '║', '═', '═', '╔', '╗', '╚', '╝') },
            { Type.ExtraBold, ('▌', '▐', '▀', '▄', '█', '█', '█', '█') }
        };

    public char? GetBox(Type type, byte x, byte y, byte height, byte width)
    {
        if (type == Type.None)
        {
            return null;
        }
        if (y == 0)
        {
            if(x == 0)
                return _boxChars[type].northWest;
            
            if(x == width - 1)
                return _boxChars[type].northEast;
            
            return _boxChars[type].north;
        }
        
        if (y == height - 1)
        {
            if(x == 0)
                return _boxChars[type].southWest;
            
            if(x == width - 1)
                return _boxChars[type].southEast;
            
            return _boxChars[type].south;
        }
        
        if(x == 0)
            return _boxChars[type].west;
        if (x == width - 1)
            return _boxChars[type].east;
        
        return null;
    }
    
}