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
    
    public int HorizontalPadding{
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }
    public int VerticalPadding{
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
        int horizontalPadding = 0, 
        int verticalPadding = 0,
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
        
        int charsPerLine = (int)(Component.Width - 2 * HorizontalPadding);
        
        int totalLines = (int)(textLength / charsPerLine + ((textLength / charsPerLine) != textLength / (float)charsPerLine ? 1 : 0));
        
        int effectiveWidth = (int)(Component.Width - 2 * HorizontalPadding);
        int effectiveHeight = (int)(Component.Height - 2 * VerticalPadding);

        int xOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => HorizontalPadding,
            HorizontalAlignmentEnum.Center => (int)(HorizontalPadding + (effectiveWidth - int.Min(charsPerLine, (int)textLength)) / 2),
            HorizontalAlignmentEnum.Right => (int)(HorizontalPadding + effectiveWidth - charsPerLine),
            _ => throw new ArgumentOutOfRangeException()
        };
        int yOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => VerticalPadding,
            HorizontalAlignmentEnum.Center => (int)(VerticalPadding + (effectiveHeight - totalLines) / 2),
            HorizontalAlignmentEnum.Right => (int)(VerticalPadding + effectiveHeight - totalLines),
            _ => throw new ArgumentOutOfRangeException()
        };

        for (int i = 0; i < textLength; i++)
        {
            int lineIndex = (int)(i / charsPerLine);
            int charIndexInLine = (int)(i % charsPerLine);
            DrawChar((int)(yOffset + lineIndex), (int)(xOffset + charIndexInLine), Text[i]);
        }
    }
}