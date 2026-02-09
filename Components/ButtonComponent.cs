using Terminal.Components.Masks;

namespace Terminal.Components;

public class ButtonComponent : Component
{
    public bool Selected = false;
    
    public ButtonComponent(Terminal terminal, byte width, byte height, byte posY, byte posX) : base(terminal, width, height, posY, posX)
    {
        Masks.AddRange( new Mask[]
        {
            
        });
    }

    public ButtonComponent(byte width, byte height, byte posY, byte posX, IEnumerable<Mask>? masks = null) : base(width, height, posY, posX, masks)
    {
    }
}