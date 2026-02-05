using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace Terminal.Components.Masks;

public class TextMask : Mask
{
    public string Text
    {
        get;
        set
        {
            Component.NeedRedraw = true;
            field = value;
        }
    }
    
    public byte HorizontalPadding{
        get;
        set
        {
            Component.NeedRedraw = true;
            field = value;
        }
    }
    public byte VerticalPadding{
        get;
        set
        {
            Component.NeedRedraw = true;
            field = value;
        }
    }
    
    public VerticalAlignmentEnum VerticalAlignment{
        get;
        set
        {
            Component.NeedRedraw = true;
            field = value;
        }
    }
    public HorizontalAlignmentEnum HorizontalAlignment{
        get;
        set
        {
            Component.NeedRedraw = true;
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
        
        byte charsPerLine = (byte)(Component.Width - 2 * HorizontalPadding);
        
        byte totalLines = (byte)(textLength / charsPerLine + ((textLength / charsPerLine) != textLength / (float)charsPerLine ? 1 : 0));
        
        byte effectiveWidth = (byte)(Component.Width - 2 * HorizontalPadding);
        byte effectiveHeight = (byte)(Component.Height - 2 * VerticalPadding);

        byte xOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => HorizontalPadding,
            HorizontalAlignmentEnum.Center => (byte)(HorizontalPadding + (effectiveWidth - charsPerLine) / 2),
            HorizontalAlignmentEnum.Right => (byte)(HorizontalPadding + effectiveWidth - charsPerLine),
            _ => throw new ArgumentOutOfRangeException()
        };
        byte yOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => VerticalPadding,
            HorizontalAlignmentEnum.Center => (byte)(VerticalPadding + (effectiveHeight - totalLines) / 2),
            HorizontalAlignmentEnum.Right => (byte)(VerticalPadding + effectiveHeight - totalLines),
            _ => throw new ArgumentOutOfRangeException()
        };

        for (int i = 0; i < textLength; i++)
        {
            byte lineIndex = (byte)(i / charsPerLine);
            byte charIndexInLine = (byte)(i % charsPerLine);
            Component.Display[yOffset + lineIndex, xOffset + charIndexInLine] = Text[i];
        }
    }
}