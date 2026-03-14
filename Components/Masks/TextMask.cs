namespace TUIS.Components.Masks;

/// <summary>
/// This <see cref="Mask"/> will draw literal custom text.
/// </summary>
public class TextMask : Mask
{
    public string Text
    {
        get;
        set
        {
            field = value;
            _textLength = Text.Length;
            NeedReDraw = true;
        }
    }

    private int _textLength;

    public byte HorizontalPadding
    {
        get;
        set
        {
            field = value;
            NeedReDraw = true;
        }
    }

    public byte VerticalPadding
    {
        get;
        set
        {
            field = value;
            NeedReDraw = true;
        }
    }


    public VerticalAlignmentEnum VerticalAlignment
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    public HorizontalAlignmentEnum HorizontalAlignment
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }


    public TextMask(Component component, string text,
        byte horizontalPadding = 0,
        byte verticalPadding = 0,
        HorizontalAlignmentEnum horizontalAlignment = HorizontalAlignmentEnum.Left,
        VerticalAlignmentEnum verticalAlignment = VerticalAlignmentEnum.Top,
        bool isVisible = true,
        Terminal.TextColor color = Terminal.TextColor.White,
        Terminal.BackgroundColor background = Terminal.BackgroundColor.None,
        Terminal.TextDecoration decoration = Terminal.TextDecoration.Default) : base(component, isVisible, color,
        background, decoration)
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void Behaviour()
    {
        int charsPerLine = (Component.Width - 2 * HorizontalPadding);

        int totalLines = (_textLength / charsPerLine +
                          ((_textLength / charsPerLine) != _textLength / (float)charsPerLine ? 1 : 0));

        int effectiveWidth = (Component.Width - 2 * HorizontalPadding);
        int effectiveHeight = (Component.Height - 2 * VerticalPadding);

        int xOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => HorizontalPadding,
            HorizontalAlignmentEnum.Center => (HorizontalPadding +
                                               (effectiveWidth - int.Min(charsPerLine, _textLength)) / 2),
            HorizontalAlignmentEnum.Right => (HorizontalPadding + effectiveWidth - charsPerLine),
        };
        int yOffset = HorizontalAlignment switch
        {
            HorizontalAlignmentEnum.Left => VerticalPadding,
            HorizontalAlignmentEnum.Center => (VerticalPadding + (effectiveHeight - totalLines) / 2),
            HorizontalAlignmentEnum.Right => (VerticalPadding + effectiveHeight - totalLines),
        };

        for (int i = 0; i < _textLength; i++)
        {
            int lineIndex = (i / charsPerLine);
            int charIndexInLine = (i % charsPerLine);
            DrawChar((yOffset + lineIndex), (xOffset + charIndexInLine), Text[i]);
        }
    }
}