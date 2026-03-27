namespace TUIS.Components.Masks;

/// <summary>
/// This <see cref="Mask"/> will draw literal custom text.
/// </summary>
public class TextMask : Mask
{
    /// <summary>
    /// Holds the string of text to draw.
    /// </summary>
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

    /// <summary>
    /// Holds the lengh of <see cref="Text"/>.
    /// </summary>
    private int _textLength;

    /// <summary>
    /// Holds the distance between the left/right sides of the <see cref="Component"/> and the begining/end of the <see cref="Text"/>'s rows.
    /// </summary>
    public byte HorizontalPadding
    {
        get;
        set
        {
            field = value;
            NeedReDraw = true;
        }
    }

    /// <summary>
    /// Holds the distance between the top/bottom sides of the <see cref="Component"/> and the first/last row of the <see cref="Text"/>.
    /// </summary>
    public byte VerticalPadding
    {
        get;
        set
        {
            field = value;
            NeedReDraw = true;
        }
    }

    /// <summary>
    /// Holds the current <see cref="VerticalAlignmentEnum"/> of the <see cref="Text"/>;
    /// </summary>
    public VerticalAlignmentEnum VerticalAlignment
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Holds the current <see cref="HorizontalAlignmentEnum"/> of the <see cref="Text"/>. 
    /// </summary>
    public HorizontalAlignmentEnum HorizontalAlignment
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text">the string of text to draw.</param>
    /// <param name="horizontalPadding">the distance between the left/right sides of the <see cref="component"/> and the beginning/end of the <see cref="text"/>'s rows.</param>
    /// <param name="verticalPadding">the distance between the top/bottom sides of the <see cref="component"/> and the first/last row of the <see cref="text"/>.</param>
    /// <param name="horizontalAlignment">Whether the <see cref="text"/> should be aligned to the <see cref="HorizontalAlignmentEnum.Left"/>, <see cref="HorizontalAlignmentEnum.Center"/> or <see cref="HorizontalAlignmentEnum.Right"/> of the <see cref="component"/>.</param>
    /// <param name="verticalAlignment">Whether the <see cref="text"/> should be aligned to the <see cref="VerticalAlignmentEnum.Top"/>, <see cref="VerticalAlignmentEnum.Center"/> or <see cref="VerticalAlignmentEnum.Bottom"/> of the <see cref="component"/>.</param>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
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