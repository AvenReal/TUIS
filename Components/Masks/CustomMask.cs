namespace TUIS.Components.Masks;
/// <summary>
/// <inheritdoc/>
/// This <see cref="Mask"/> helps to create a <see cref="Mask"/> with unique behaviour without needing to create an entire new class.  
/// </summary>
public class CustomMask : Mask
{
    /// <summary>
    /// Holds the Action.
    /// </summary>
    private readonly Action<CustomMask> _action;

    /// <summary>
    /// Holds properties used in the <see cref="_action"/>.
    /// (Useless for now).
    /// </summary>
    public readonly Dictionary<string, object> Properties = new();

    /// <summary>
    /// Create a <see cref="Mask"/> with unique <see cref="Behaviour"/> (<paramref name="action"/>).
    /// </summary>
    /// <param name="component"><inheritdoc/></param>
    /// <param name="action"></param>
    /// <param name="isVisible"><inheritdoc/></param>
    /// <param name="color"><inheritdoc/></param>
    /// <param name="background"><inheritdoc/></param>
    /// <param name="decoration"><inheritdoc/></param>
    public CustomMask(Component component, Action<CustomMask> action, bool isVisible = true,
        TextColor color = TextColor.White, BackgroundColor background = BackgroundColor.None,
        TextDecoration decoration = TextDecoration.Default) : base(component, isVisible, color, background, decoration)
    {
        _action = action;
    }


    protected override void Behaviour()
    {
        _action(this);
    }
}