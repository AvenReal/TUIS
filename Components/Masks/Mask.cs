namespace Terminal.Components.Masks;

public abstract class Mask
{
    public Component? Component;
    
    

    public bool IsVisible
    {
        get;
        set
        {
            Component?.NeedRedraw = true;
            field = value;
        }
    }
    
    public Mask(Component component)
    {
        Component = component;
        Component.Masks.Add(this);
        
        
        IsVisible = true;
    }

    public Mask()
    {
        IsVisible = true;
    }
    
    protected abstract void Behaviour();

    public void Draw()
    {
        if(Component == null)
            throw new NullReferenceException("Component of Mask is null");
        
        
        if(IsVisible)
            Behaviour();
    }
}