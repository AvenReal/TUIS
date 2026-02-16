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

    public int HorizontalPadding
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }
    
    public int VerticalPadding
    {
        get;
        set
        {
            NeedRedraw = true;
            field = value;
        }
    }
    
    public InputMask(Component component, Action<InputMask>? onOutputChange = null, int horizontalPadding = 0, int verticalPadding = 0) : base(component)
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