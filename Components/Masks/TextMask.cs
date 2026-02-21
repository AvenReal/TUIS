namespace TUIS.Components.Masks;

public class TextMask : Mask
{
    public string Text
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }
    
    
    public VerticalAlignmentEnum VerticalAlignment{
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }
    public HorizontalAlignmentEnum HorizontalAlignment{
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }
    
    
    public TextMask(Component component, string text, 
        byte horizontalPadding = 0, 
        byte verticalPadding = 0,
        HorizontalAlignmentEnum horizontalAlignment = HorizontalAlignmentEnum.Left, 
        VerticalAlignmentEnum verticalAlignment = VerticalAlignmentEnum.Top) 
        : base(component)
    {
        Text = text;
        HorizontalPadding = horizontalPadding;
        VerticalPadding = verticalPadding;
        HorizontalAlignment = horizontalAlignment;
        VerticalAlignment = verticalAlignment;
    }
    
    
    public enum HorizontalAlignmentEnum
    {
        Left,
        Center,
        Right
    }
    
    public enum VerticalAlignmentEnum
    {
        Top,
        Center,
        Bottom
    }

    protected override void Behaviour()
    {
        
        int textLength = Text.Length;
        
        int charsPerLine = (Component.Width - 2 * HorizontalPadding);
        
        int totalLines = (textLength / charsPerLine + ((textLength / charsPerLine) != textLength / (float)charsPerLine ? 1 : 0));
        
        int effectiveWidth = (Component.Width - 2 * HorizontalPadding);
        int effectiveHeight = (Component.Height - 2 * VerticalPadding);

        int xOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => HorizontalPadding,
            HorizontalAlignmentEnum.Center => (HorizontalPadding + (effectiveWidth - int.Min(charsPerLine, textLength)) / 2),
            HorizontalAlignmentEnum.Right => (HorizontalPadding + effectiveWidth - charsPerLine),
            _ => throw new ArgumentOutOfRangeException()
        };
        int yOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => VerticalPadding,
            HorizontalAlignmentEnum.Center => (VerticalPadding + (effectiveHeight - totalLines) / 2),
            HorizontalAlignmentEnum.Right => (VerticalPadding + effectiveHeight - totalLines),
            _ => throw new ArgumentOutOfRangeException()
        };

        for (int i = 0; i < textLength; i++)
        {
            int lineIndex = (i / charsPerLine);
            int charIndexInLine = (i % charsPerLine);
            DrawChar((yOffset + lineIndex), (xOffset + charIndexInLine), Text[i]);
        }
    }
}