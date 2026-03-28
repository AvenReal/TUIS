namespace TUIS.Components.Masks;

/// <summary>
/// This <see cref="Mask"/> will draw literal custom text using ascii art. 
/// </summary>
public class BigTextMask : TextMask
{
    /// <summary>
    /// Enum representing the different font types possible.
    /// </summary>
    public enum FontType
    {
        ThreeByThreeClassic,
    }

    /// <summary>
    /// Holds the way to construct the <see cref="FontType.ThreeByThreeClassic"/> font. 
    /// </summary>
    private static readonly Dictionary<char, char[]> ThreeByThreeClassic = new()
    {
        { 'A', ['▄', '▀', '▄', '█', '▄', '█', '█', ' ', '█'] },
        { 'a', ['▄', '▄', ' ', ' ', '▄', '█', '▀', '▄', '█'] },
        { 'B', ['█', '▀', '▄', '█', '▀', '▄', '█', '▄', '▀'] },
        { 'b', ['█', ' ', ' ', '█', '▀', '▄', '█', '▄', '▀'] },
        { 'C', ['▄', '▀', '▄', '█', ' ', ' ', '▀', '▄', '▀'] },
        { 'c', [' ', ' ', ' ', '▄', '▀', '▀', '▀', '▄', '▄'] },
        { 'D', ['█', '▀', '▄', '█', ' ', '█', '█', '▄', '▀'] },
        { 'd', [' ', ' ', '█', '▄', '▀', '█', '▀', '▄', '█'] },
        { 'E', ['█', '▀', '▀', '█', '▀', '▀', '█', '▄', '▄'] },
        { 'e', [' ', ' ', ' ', '▄', '▀', '▄', '▀', '█', '▄'] },
        { 'F', ['█', '▀', '▀', '█', '▀', ' ', '█', ' ', ' '] },
        { 'f', [' ', '▄', '▀', '▀', '█', '▀', ' ', '█', ' '] },
        { 'G', ['█', '▀', '▀', '█', ' ', '▄', '█', '▄', '█'] },
        { 'g', [' ', '▄', '▄', '▀', '▄', '█', '▄', '▄', '▀'] },
        { 'H', ['█', ' ', '█', '█', '▀', '█', '█', ' ', '█'] },
        { 'h', ['█', ' ', ' ', '█', '▀', '▄', '█', ' ', '█'] },
        { 'I', ['▀', '█', '▀', ' ', '█', ' ', '▄', '█', '▄'] },
        { 'i', [' ', '▄', ' ', ' ', '▄', ' ', ' ', '█', ' '] },
        { 'J', ['▀', '▀', '█', ' ', ' ', '█', '▀', '▄', '█'] },
        { 'j', [' ', ' ', '▀', '▄', ' ', '█', '▀', '█', '▀'] },
        { 'K', ['█', ' ', '█', '█', '▄', '▀', '█', ' ', '█'] },
        { 'k', ['█', ' ', ' ', '█', '▄', '▀', '█', ' ', '█'] },
        { 'L', ['█', ' ', ' ', '█', ' ', ' ', '█', '▄', '▄'] },
        { 'l', [' ', '█', ' ', ' ', '█', ' ', ' ', '▀', '▄'] },
        { 'M', ['█', '▄', '█', '█', ' ', '█', '█', ' ', '█'] },
        { 'm', [' ', ' ', ' ', '█', '▄', '█', '█', ' ', '█'] },
        { 'N', ['▄', ' ', '█', '█', '█', '█', '█', ' ', '▀'] },
        { 'n', [' ', ' ', ' ', '▄', '▄', ' ', '█', ' ', '█'] },
        { 'O', ['▄', '▀', '▄', '█', ' ', '█', '▀', '▄', '▀'] },
        { 'o', [' ', ' ', ' ', '▄', '▀', '▄', '▀', '▄', '▀'] },
        { 'P', ['█', '▀', '█', '█', '▄', '█', '█', ' ', ' '] },
        { 'p', ['█', '▀', '▄', '█', '▄', '▀', '█', ' ', ' '] },
        { 'Q', ['▄', '▀', '▄', '█', ' ', '█', '▀', '█', '▄'] },
        { 'q', ['▄', '▀', '█', '▀', '▄', '█', ' ', ' ', '█'] },
        { 'R', ['█', '▀', '▄', '█', '▄', '▀', '█', ' ', '█'] },
        { 'r', [' ', ' ', ' ', '█', '▄', '▀', '█', ' ', ' '] },
        { 'S', ['▄', '▀', '▀', ' ', '▀', '▄', '▄', '▄', '▀'] },
        { 's', [' ', '▄', '▄', '▀', '▄', ' ', '▄', '▄', '▀'] },
        { 'T', ['▀', '█', '▀', ' ', '█', ' ', ' ', '█', ' '] },
        { 't', [' ', '▄', ' ', '▀', '█', '▀', ' ', '▀', '▄'] },
        { 'U', ['▄', ' ', '▄', '█', ' ', '█', '█', '▄', '█'] },
        { 'u', [' ', ' ', ' ', '▄', ' ', '▄', '▀', '▄', '█'] },
        { 'V', ['▄', ' ', '▄', '█', ' ', '█', '▀', '▄', '▀'] },
        { 'v', [' ', ' ', ' ', '▄', ' ', '▄', '▀', '▄', '▀'] },
        { 'W', ['█', ' ', '█', '█', ' ', '█', '█', '▀', '█'] },
        { 'w', [' ', ' ', ' ', '█', ' ', '█', '█', '▀', '█'] },
        { 'X', ['█', ' ', '█', '▄', '▀', '▄', '█', ' ', '█'] },
        { 'x', [' ', ' ', ' ', '▀', '▄', '▀', '█', ' ', '█'] },
        { 'Y', ['█', ' ', '█', '▀', '█', '▀', ' ', '█', ' '] },
        { 'y', ['▄', ' ', '▄', '▀', '▄', '▀', '▄', '▀', ' '] },
        { 'Z', ['▀', '▀', '█', ' ', '█', ' ', '█', '▄', '▄'] },
        { 'z', ['▄', '▄', '▄', ' ', '▄', '▀', '█', '▄', '▄'] },
        { '0', ['█', '▀', '█', '█', ' ', '█', '█', '▄', '█'] },
        { '1', [' ', ' ', '█', ' ', ' ', '█', ' ', ' ', '█'] },
        { '2', ['▀', '▀', '█', '█', '▀', '▀', '█', '▄', '▄'] },
        { '3', ['▀', '▀', '█', '▀', '▀', '█', '▄', '▄', '█'] },
        { '4', ['█', ' ', '█', '▀', '▀', '█', ' ', ' ', '█'] },
        { '5', ['█', '▀', '▀', '▀', '▀', '█', '▄', '▄', '█'] },
        { '6', ['█', '▀', '▀', '█', '▀', '█', '█', '▄', '█'] },
        { '7', ['▀', '▀', '█', ' ', ' ', '█', ' ', ' ', '█'] },
        { '8', ['█', '▀', '█', '█', '▀', '█', '█', '▄', '█'] },
        { '9', ['█', '▀', '█', '▀', '▀', '█', '▄', '▄', '█'] },
        { '.', [' ', ' ', ' ', ' ', ' ', ' ', ' ', '▄', ' '] },
        { ',', [' ', ' ', ' ', ' ', ' ', ' ', '▄', '▀', ' '] },
    };

    /// <summary>
    /// Map the <see cref="Font"/> to the corresponding Font Dictionary.
    /// </summary>
    private static readonly Dictionary<FontType, (int size, Dictionary<char, char[]>)> GetFont = new()
    {
        { FontType.ThreeByThreeClassic, (3, ThreeByThreeClassic) },
    };

    /// <summary>
    /// Holds the current <see cref="FontType"/>.
    /// </summary>
    public FontType Font
    {
        get;
        set
        {
            field = value;
            NeedReDraw = true;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// This <see cref="TextMask"/> will draw the text using ascii art.
    /// </summary>
    /// <param name="fontType">The type of font to draw.</param>
    /// <param name="text">The string of text to draw.</param>
    /// <param name="horizontalPadding">The distance between the left/right sides of the <paramref name="component"/> and the beginning/end of the <paramref name="text"/>'s rows.</param>
    /// <param name="verticalPadding">The distance between the top/bottom sides of the <paramref name="component"/> and the first/last row of the <paramref name="text"/>.</param>
    /// <param name="horizontalAlignment">Whether the <paramref name="text"/> should be aligned to the <see cref="HorizontalAlignmentEnum.Left"/>, <see cref="HorizontalAlignmentEnum.Center"/> or <see cref="HorizontalAlignmentEnum.Right"/> of the <paramref name="component"/>.</param>
    /// <param name="verticalAlignment">Whether the <paramref name="text"/> should be aligned to the <see cref="VerticalAlignmentEnum.Top"/>, <see cref="VerticalAlignmentEnum.Center"/> or <see cref="VerticalAlignmentEnum.Bottom"/> of the <paramref name="component"/>.</param>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
    public BigTextMask(Component component, string text, FontType fontType = FontType.ThreeByThreeClassic,
        byte horizontalPadding = 0, byte verticalPadding = 0,
        HorizontalAlignmentEnum horizontalAlignment = HorizontalAlignmentEnum.Left,
        VerticalAlignmentEnum verticalAlignment = VerticalAlignmentEnum.Top, bool isVisible = true,
        Terminal.TextColor color = Terminal.TextColor.White,
        Terminal.BackgroundColor background = Terminal.BackgroundColor.None,
        Terminal.TextDecoration decoration = Terminal.TextDecoration.Default) : base(component, text, horizontalPadding,
        verticalPadding, horizontalAlignment, verticalAlignment, isVisible, color, background, decoration)
    {
        Font = fontType;
    }

    protected override void Behaviour()
    {
        int x = 0;
        int y = 0;
        foreach (var c in Text)
        {
            (y, x) = DrawLetter(y, x, c);
        }
    }

    private (int newY, int newX) DrawLetter(int y, int x, char letter)
    {
        (int size, Dictionary<char, char[]> font) = GetFont[Font];
        if ((x + size) > Component.Width)
        {
            y += size + 1;
            x = 0;
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                char c = ThreeByThreeClassic.ContainsKey(letter) ? font[letter][i * size + j] : ' ';

                DrawChar(y + i, x + j, c);
            }
        }

        return (y, x + (int)(size * 1.8));
    }
}