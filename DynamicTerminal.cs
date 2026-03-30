using TUIS.Components;

namespace TUIS;

public class DynamicTerminal : Terminal
{
    public List<SelectableComponent> SelectableComponents = new();

    private int _index = 0;

    public DynamicTerminal(int width, int height, ConsoleKeyInfo switchKeyInfo) : base(width, height)
    {
        InputSystem.OnKeyPress += keyInfo =>
        {
            if (keyInfo.Key == switchKeyInfo.Key)
            {
                SelectableComponents[_index].IsSelected = false;
                _index = (_index + 1) % SelectableComponents.Count;
                SelectableComponents[_index].IsSelected = true;
            }
        };
    }

    public override void AddComponent(Component component)
    {
        if (component is SelectableComponent selectableComponent)
        {
            SelectableComponents.Add(selectableComponent);
        }

        base.AddComponent(component);
    }
}