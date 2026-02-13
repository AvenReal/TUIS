namespace TUIS.Components.Masks;

public class CustomMask : Mask
{
    private readonly Action<CustomMask> _action;
    public readonly Dictionary<string, object> Properties = new(); 
    
    public CustomMask(Component component, Action<CustomMask> action) : base(component)
    {
        _action = action;
    }

    public CustomMask(Action<CustomMask> action)
    {
        _action = action;
    }




    protected override void Behaviour()
    {
        _action(this);
    }
}