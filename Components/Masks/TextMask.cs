using System.Runtime.Intrinsics.X86;

namespace Terminal.Components.Masks;

public class TextMask : Mask
{
    public string Text;
    
    public TextMask(Component component, string text, 
        HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, 
        VerticalAlignment verticalAlignment = VerticalAlignment.Top) 
        : base(component, delegate(Component component, Mask mask)
    {
        TextMask textMask = (TextMask) mask;
        
        int textLength = textMask.Text.Length;

        byte horizontalOffset = (byte)(horizontalAlignment == 0
            ? 0
            : (component.Width - textLength % component.Width ) / (int)horizontalAlignment);

        byte verticalOffset = (byte)(verticalAlignment == 0
            ? 0
            : (component.Height - textLength / component.Width) / (int)verticalAlignment);
        
        for (int i = 0; i < textLength; i++)
        {
            char c = textMask.Text[i];
            if(i / component.Width < component.Height)
                component.Display[i / component.Width + verticalOffset, i % component.Width + horizontalOffset] = c;
        }
    })
    {
        Text = text;
    }
    
    public enum HorizontalAlignment : byte
    {
        Left = 0,
        Center = 2,
    }
    
    public enum VerticalAlignment :  byte
    {
        Top = 0,
        Center = 2,
    }
}