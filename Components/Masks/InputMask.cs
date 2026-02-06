namespace Terminal.Components.Masks;

public class InputMask : Mask
{
    public bool Enabeled
    {
        get;
        set
        {
            Component?.NeedRedraw = true;
            field = value;
        }
    }
    public string Output = "";

    public byte HorizontalPadding
    {
        get;
        set
        {
            Component?.NeedRedraw = true;
            field = value;
        }
    }
    
    public byte VerticalPadding
    {
        get;
        set
        {
            Component?.NeedRedraw = true;
            field = value;
        }
    }
    
    public InputMask(Component component, byte horizontalPadding = 0, byte verticalPadding = 0) : base(component)
    {
        HorizontalPadding = horizontalPadding;
        VerticalPadding = verticalPadding;
    }

    public InputMask(byte horizontalPadding = 0, byte verticalPadding = 0)
    {
        HorizontalPadding = horizontalPadding;
        VerticalPadding = verticalPadding;
    }
    
    protected override void Behaviour()
    {
        if (Enabeled)
        {
            Console.Write($"\u001b[{Component!.PosY + VerticalPadding};{Component.PosX + HorizontalPadding}H");
            Console.CursorVisible = true;
            Output = Console.ReadLine() ?? "";
            Enabeled = false;
        }
    }
}