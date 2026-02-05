namespace Terminal.Components.Masks;

public class CustomMask : Mask
{
    private readonly Action<Mask> _action;
    
    public CustomMask(Component component, Action<Mask> action) : base(component)
    {
        _action = action;
    }

    protected override void Behaviour()
    {
        _action(this);
    }
}