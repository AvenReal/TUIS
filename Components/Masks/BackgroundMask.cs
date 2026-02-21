namespace TUIS.Components.Masks;

/// <summary>
/// Print a single char in the background (Warning Masks will be printed in order so this one will override any Mask before itself)
/// </summary>
public class BackgroundMask : Mask
{
    /// <summary>
    /// Keeps the background character.
    /// </summary>
    public char BackgroundChar
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Print a single <paramref name="backgroundChar"/> in the background (Warning Masks will be printed in order so this one will override any Mask before itself) <
    /// <inheritdoc/>
    /// </summary>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="backgroundChar">The character representing the background of the component (default = ' ').</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
    public BackgroundMask(Component component, char backgroundChar = ' ', bool isVisible = true,
        TextColor color = TextColor.White, BackgroundColor background = BackgroundColor.None,
        TextDecoration decoration = TextDecoration.Default) : base(component, isVisible, color, background, decoration)
    {
        BackgroundChar = backgroundChar;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
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