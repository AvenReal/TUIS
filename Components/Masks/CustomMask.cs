namespace TUIS.Components.Masks;
/// <summary>
/// <inheritdoc/>
/// This <see cref="Mask"/> helps to create a <see cref="Mask"/> with unique behaviour without needing to create an entire new class.  
/// </summary>
public class CustomMask : Mask
{
    /// <summary>
    /// Holds the <see cref="Action"/> to call when the <see cref="Behaviour"/> is called.
    /// The argument of the <see cref="Action"/> represent the <see cref="CustomMask"/> object itself.
    /// </summary>
    private readonly Action<CustomMask> _action;

    /// <summary>
    /// Holds properties used in the <see cref="_action"/>.
    /// (Useless for now).
    /// </summary>
    public readonly Dictionary<string, object> Properties = new();

    
    /// <summary>
    /// <inheritdoc/>
    /// Create a <see cref="Mask"/> with unique <see cref="Behaviour"/> (<paramref name="action"/>).
    /// </summary>
    /// <param name="action">The <see cref="Action"/> to perform when <see cref="Behaviour"/> is called.</param>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
    public CustomMask(Component component, Action<CustomMask> action, bool isVisible = true,
        TextColor color = TextColor.White, BackgroundColor background = BackgroundColor.None,
        TextDecoration decoration = TextDecoration.Default) : base(component, isVisible, color, background, decoration)
    {
        _action = action;
    }

    /// <summary>
    /// <inheritdoc/>
    /// This <see cref="Mask"/>'s <see cref="Behaviour"/> is to call the <see cref="_action"/>.
    /// </summary>
    protected override void Behaviour()
    {
        _action(this);
    }
}