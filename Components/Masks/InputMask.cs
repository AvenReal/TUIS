namespace TUIS.Components.Masks;

public class InputMask : Mask
{
    public bool Enabeled
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }

    public string Output
    {
        get;
        private set
        {
            NeedRedraw = true;
            field = value;
            _onOutputChange?.Invoke(this);
        }
    }

    private readonly Action<InputMask>? _onOutputChange;

    public byte HorizontalPadding
    {
        get;
        set
        {
            field = value;
            NeedRedraw = true;
        }
    }

    public byte VerticalPadding
    {
        get;
        set
        {
            field = value;
            NeedRedraw = true;
        }
    }


    public InputMask(Component component, Action<InputMask>? onOutputChange = null, byte horizontalPadding = 0,
        byte verticalPadding = 0, bool isVisible = true, TextColor color = TextColor.White,
        BackgroundColor background = BackgroundColor.None, TextDecoration decoration = TextDecoration.Default) : base(
        component, isVisible, color, background, decoration)
    {
        Output = "";
        _onOutputChange = onOutputChange;
        HorizontalPadding = horizontalPadding;
        VerticalPadding = verticalPadding;
    }


    protected override void Behaviour()
    {
        if (Enabeled)
        {
            Component.NeedRedraw = false;
            Enabeled = false;
            Console.CursorVisible = true;

            Console.Write($"\u001b[{Component.PosY + VerticalPadding};{Component.PosX + HorizontalPadding}H");

            Output = Console.ReadLine() ?? "";
        }
    }
}