using TUIS.Components.Masks;

namespace TUIS.Components;

public class ButtonComponent : Component
{
    public bool Selected = false;
    
    public ButtonComponent(Terminal terminal, int width, int height, int posY, int posX) : base(terminal, width, height, posY, posX)
    {
        Masks.AddRange( new Mask[]
        {
            
        });
    }

    public ButtonComponent(int width, int height, int posY, int posX, IEnumerable<Mask>? masks = null) : base(width, height, posY, posX, masks)
    {
    }
}