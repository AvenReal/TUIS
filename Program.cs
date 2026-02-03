// See https://aka.ms/new-console-template for more information


using Terminal.Components;

Terminal.Terminal terminal = new Terminal.Terminal();

BoxedComponent component = new BoxedComponent(terminal, 20, 5, 1, 10);
terminal.Components.Add(component);





for (int i = 0; i < component.Height; i++)
{
    for (int j = 0; j < component.Width; j++)
    {
        component.Display[i, j] = '█';
    }
}
terminal.PrintScreen();