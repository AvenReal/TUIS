namespace TUIS.Components.Masks;

public class BigTextTextMask : TextMask
{
    private static Dictionary<char, char[]> _font = new()
    {
        { 'A', new[] { '▄', '▀', '▄', '█', '▄', '█', '█', ' ', '█' } },
        { 'a', new[] { '▄', '▄', ' ', ' ', '▄', '█', '▀', '▄', '█' } },

        { 'B', new[] { '█', '▀', '▄', '█', '▀', '▄', '█', '▄', '▀' } },
        { 'b', new[] { '█', ' ', ' ', '█', '▀', '▄', '█', '▄', '▀' } },

        { 'C', new[] { '▄', '▀', '▄', '█', ' ', ' ', '▀', '▄', '▀' } },
        { 'c', new[] { ' ', ' ', ' ', '▄', '▀', '▀', '▀', '▄', '▄' } },

        { 'D', new[] { '█', '▀', '▄', '█', ' ', '█', '█', '▄', '▀' } },
        { 'd', new[] { ' ', ' ', '█', '▄', '▀', '█', '▀', '▄', '█' } },

        { 'E', new[] { '█', '▀', '▀', '█', '▀', '▀', '█', '▄', '▄' } },
        { 'e', new[] { ' ', '▄', ' ', '█', '▄', '█', '▀', '▄', '▄' } },

        { 'F', new[] { '█', '▀', '▀', '█', '▀', ' ', '█', ' ', ' ' } },
        { 'f', new[] { ' ', '▄', '▀', '▀', '█', '▀', ' ', '█', ' ' } },

        { 'G', new[] { '█', '▀', '▀', '█', ' ', '▄', '█', '▄', '█' } },
        { 'g', new[] { ' ', '▄', '▄', '▀', '▄', '█', '▄', '▄', '▀' } },

        { 'H', new[] { '█', ' ', '█', '█', '▀', '█', '█', ' ', '█' } },
        { 'h', new[] { '█', ' ', ' ', '█', '▀', '▄', '█', ' ', '█' } },

        { 'I', new[] { '▀', '█', '▀', ' ', '█', ' ', '▄', '█', '▄' } },
        { 'i', new[] { ' ', '▄', ' ', ' ', '▄', ' ', ' ', '█', ' ' } },

        { 'J', new[] { '▀', '▀', '█', ' ', ' ', '█', '▀', '▄', '█' } },
        { 'j', new[] { ' ', ' ', '▀', '▄', ' ', '█', '▀', '█', '▀' } },

        { 'K', new[] { '█', ' ', '█', '█', '▄', '▀', '█', ' ', '█' } },
        { 'k', new[] { '█', ' ', ' ', '█', '▄', '▀', '█', ' ', '█' } },

        { 'L', new[] { '█', ' ', ' ', '█', ' ', ' ', '█', '▄', '▄' } },
        { 'l', new[] { ' ', '█', ' ', ' ', '█', ' ', ' ', '▀', '▄' } },

        { 'M', new[] { '█', '▄', '█', '█', ' ', '█', '█', ' ', '█' } },
        { 'm', new[] { ' ', ' ', ' ', '█', '▄', '█', '█', ' ', '█' } },

        { 'N', new[] { '▄', ' ', '█', '█', '█', '█', '█', ' ', '▀' } },
        { 'n', new[] { ' ', ' ', ' ', '▄', '▄', ' ', '█', ' ', '█' } },

        { 'O', new[] { '▄', '▀', '▄', '█', ' ', '█', '▀', '▄', '▀' } },
        { 'o', new[] { ' ', ' ', ' ', '▄', '▀', '▄', '▀', '▄', '▀' } },

        { 'P', new[] { '█', '▀', '█', '█', '▄', '█', '█', ' ', ' ' } },
        { 'p', new[] { '█', '▀', '▄', '█', '▄', '▀', '█', ' ', ' ' } },

        { 'Q', new[] { '▄', '▀', '▄', '█', ' ', '█', '▀', '█', '▄' } },
        { 'q', new[] { '▄', '▀', '█', '▀', '▄', '█', ' ', ' ', '█' } },

        { 'R', new[] { '█', '▀', '▄', '█', '▄', '▀', '█', ' ', '█' } },
        { 'r', new[] { ' ', ' ', ' ', '█', '▄', '▀', '█', ' ', ' ' } },

        { 'S', new[] { '▄', '▀', '▀', ' ', '▀', '▄', '▄', '▄', '▀' } },
        { 's', new[] { ' ', '▄', '▄', '▀', '▄', ' ', '▄', '▄', '▀' } },

        { 'T', new[] { '▄', '▄', '▄', ' ', '█', ' ', ' ', '█', ' ' } },
        { 't', new[] { ' ', '▄', ' ', '▀', '█', '▀', ' ', '▀', '▄' } },

        { 'U', new[] { '▄', ' ', '▄', '█', ' ', '█', '█', '▄', '█' } },
        { 'u', new[] { '▄', ' ', '▄', '█', '▄', '█', ' ', ' ', ' ' } },

        { 'V', new[] { '▄', ' ', '▄', '█', ' ', '█', '▀', '▄', '▀' } },
        { 'v', new[] { '▄', ' ', '▄', '▀', '▄', '▀', ' ', ' ', ' ' } },

        { 'W', new[] { '█', ' ', '█', '█', ' ', '█', '█', '▀', '█' } },
        { 'w', new[] { '█', ' ', '█', '█', '▀', '█', ' ', ' ', ' ' } },

        { 'X', new[] { '█', ' ', '█', '█', ' ', ' ', '█', ' ', '█' } },
        { 'x', new[] { '▄', ' ', '▄', '▀', '▄', '▀', '█', ' ', '█' } },

        { 'Y', new[] { '█', ' ', '█', '▀', '█', '▀', '█', ' ', '█' } },
        { 'y', new[] { '▄', ' ', '▄', '▀', '▄', '▀', '▄', '▀', ' ' } },

        { 'Z', new[] { '▀', '▀', '█', '█', ' ', ' ', '█', '▄', '▄' } },
        { 'z', new[] { '▄', '▄', '▄', '▄', '▀', ' ', '█', '▄', '▄' } }
    };

    public BigTextTextMask(Component component, string text, byte horizontalPadding = 0, byte verticalPadding = 0,
        HorizontalAlignmentEnum horizontalAlignment = HorizontalAlignmentEnum.Left,
        VerticalAlignmentEnum verticalAlignment = VerticalAlignmentEnum.Top, bool isVisible = true,
        Terminal.TextColor color = Terminal.TextColor.White,
        Terminal.BackgroundColor background = Terminal.BackgroundColor.None,
        Terminal.TextDecoration decoration = Terminal.TextDecoration.Default) : base(component, text, horizontalPadding,
        verticalPadding, horizontalAlignment, verticalAlignment, isVisible, color, background, decoration)
    {
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
        if ((x + 3) > Component.Width)
        {
            y += 4;
            x = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                char c = _font.ContainsKey(letter) ? _font[letter][i * 3 + j] : ' ';

                DrawChar(y + i, x + j, c);
            }
        }

        return (y, x + 5);
    }
}