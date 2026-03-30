using TUIS.Components.Masks;

namespace TUIS.Components;

public class SelectableComponent : Component
{
    public virtual bool IsSelected
    {
        get;
        set
        {
            field = value;
            _boxMask.BoxType = value ? BoxMask.Type.Bold : BoxMask.Type.Rounded;
        }
    }

    private BoxMask _boxMask;

    public SelectableComponent(Terminal terminal, int width, int height, int posY, int posX) : base(terminal, width,
        height, posY, posX)
    {
        _boxMask = new BoxMask(this, BoxMask.Type.Rounded);
    }
}