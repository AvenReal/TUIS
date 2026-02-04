namespace Terminal.Components.Masks;

public class TextMask : Mask
{
    public string Text;
    
    public TextMask(Component component, string text) : base(component, delegate(Component component, Mask mask)
    {
        TextMask textMask = (TextMask) mask;

        for (int i = 0; i < textMask.Text.Length; i++)
        {
            char c = textMask.Text[i];
            if(i / component.Width < component.Height)
                component.Display[i / component.Width , i % component.Width] = c;
        }
    })
    {
        Text = text;
    }
    
    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }
    
    public enum VerticalAlignment
    {
        Top,
        Center,
        Bottom
    }
}