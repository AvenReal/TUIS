namespace Terminal.Components.Masks;

public class Mask
{
    public readonly Component Component;
    public readonly Action<Component, Mask> Function;
    
    
    public Mask(Component component, Action<Component, Mask> function)
    {
        Component = component;
        Component.Masks.Add(this);
        Function = function;
    }

    public void Draw()
    {
        Function(Component, this);
    }
}