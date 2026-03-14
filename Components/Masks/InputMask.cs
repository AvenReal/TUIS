namespace TUIS.Components.Masks;

/// <summary>
/// WIP. This mask let you use <see cref="Console.ReadLine"/> to get some user input as command or text 
/// </summary>
public class InputMask : Mask
{
    /// <summary>
    /// Holds whether or not the <see cref="Mask"/> should read the user inputs.  
    /// </summary>
    public bool Enabled
    {
        get;
        set
        {
            NeedReDraw = true;
            field = value;
        }
    }

    /// <summary>
    /// Holds the <see cref="string"/> corresponding to the last input of the user 
    /// </summary>
    public string Output
    {
        get;
        private set
        {
            NeedReDraw = true;
            field = value;
            _onOutputChange?.Invoke(this);
        }
    }

    /// <summary>
    /// Holds an <see cref="Action"/> that will be called when the <see cref="Output"/> changes.
    /// </summary>
    private readonly Action<InputMask>? _onOutputChange;

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


    public InputMask(Component component, Action<InputMask>? onOutputChange = null, byte horizontalPadding = 0,
        byte verticalPadding = 0, bool isVisible = true, Terminal.TextColor color = Terminal.TextColor.White,
        Terminal.BackgroundColor background = Terminal.BackgroundColor.None,
        Terminal.TextDecoration decoration = Terminal.TextDecoration.Default) : base(
        component, isVisible, color, background, decoration)
    {
        Output = "";
        _onOutputChange = onOutputChange;
        HorizontalPadding = horizontalPadding;
        VerticalPadding = verticalPadding;
    }


    protected override void Behaviour()
    {
        if (Enabled)
        {
            Component.NeedReDraw = false;
            Enabled = false;
            Console.CursorVisible = true;

            Console.Write($"\u001b[{Component.PosY + VerticalPadding};{Component.PosX + HorizontalPadding}H");

            Output = Console.ReadLine() ?? "";
        }
    }
}