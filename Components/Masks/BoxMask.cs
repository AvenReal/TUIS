namespace TUIS.Components.Masks;

public class BoxMask : Mask
{
    /// <summary>
    /// Keeps the <see cref="Type"/> of boxing 
    /// </summary>
    public Type BoxType
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// Add a bounding box on the component.
    /// </summary>
    /// <param name="component"><inheritdoc/></param>
    /// <param name="type">The <see cref="Type"/> of boxing.</param>
    /// <param name="isVisible"><inheritdoc/></param>
    /// <param name="color"><inheritdoc/></param>
    /// <param name="background"><inheritdoc/></param>
    /// <param name="decoration"><inheritdoc/></param>
    public BoxMask(Component component, Type type, bool isVisible = true, TextColor color = TextColor.White,
        BackgroundColor background = BackgroundColor.None, TextDecoration decoration = TextDecoration.Default) : base(
        component, isVisible, color, background, decoration)
    {
        BoxType = type;
    }

    /// <summary>
    /// The Type of boxing
    /// </summary>
    public enum Type
    {
        Simple,
        Double,
        Thick,
        None
    }

    /// <summary>
    /// Maps the <see cref="BoxType"/> to the corresponding chars.
    /// </summary>
    private static
        Dictionary<Type, (char west, char east, char north, char south, char northWest, char northEast, char southWest,
            char southEast)> _boxChars = new()
        {
            { Type.Simple, ('│', '│', '─', '─', '┌', '┐', '└', '┘') },
            { Type.Double, ('║', '║', '═', '═', '╔', '╗', '╚', '╝') },
            { Type.Thick, ('▌', '▐', '▀', '▄', '█', '█', '█', '█') }
        };


    /// <summary>
    /// Helps getting the appropriate char regarding of the local coordinates (<paramref name="x"/>, <paramref name="y"/>).
    /// </summary>
    /// <param name="type">The <see cref="Type"/> of boxing.</param>
    /// <param name="x">The coordinate of the x-axis (0 = left) relative to the <see cref="Component"/>.</param>
    /// <param name="y">The coordinate of the y-axis (0 = top) relative to the <see cref="Component"/>.</param>
    /// <param name="height">The height of the <see cref="Component"/>.</param>
    /// <param name="width">The width of the <see cref="Component"/>.</param>
    /// <returns>The appropriate char regarding of the local coordinates (<paramref name="x"/>, <paramref name="y"/>).</returns>
    private static char? GetBox(Type type, int x, int y, int height, int width)
    {
        if (type == Type.None)
            return null;

        if (y == 0)
        {
            if (x == 0) return _boxChars[type].northWest;

            if (x == width - 1) return _boxChars[type].northEast;

            return _boxChars[type].north;
        }

        if (y == height - 1)
        {
            if (x == 0) return _boxChars[type].southWest;

            if (x == width - 1) return _boxChars[type].southEast;

            return _boxChars[type].south;
        }

        if (x == 0) return _boxChars[type].west;
        if (x == width - 1) return _boxChars[type].east;

        return null;
    }

    protected override void Behaviour()
    {
        for (int i = 0; i < Component.Height; i++)
        {
            for (int j = 0; j < Component.Width; j++)
            {
                DrawChar(i, j, GetBox(BoxType, j, i, Component.Height, Component.Width));
            }
        }
    }
}