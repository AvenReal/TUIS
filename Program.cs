// See https://aka.ms/new-console-template for more information


using Terminal.Components;

Terminal.Terminal terminal = new Terminal.Terminal();

BoxedComponent component = new BoxedComponent(terminal, 20, 5, 1, 10);
BoxedComponent component1 = new BoxedComponent(terminal, 20, 5, 1, 35);
component1.Box = Boxes.Type.Light;
BoxedComponent component2 = new BoxedComponent(terminal, 20, 5, 20, 10);
Component component3 = new Component(terminal, 20, 3, 1, 70);
component2.Box = Boxes.Type.Bold;
terminal.Components.Add(component);
terminal.Components.Add(component1);
terminal.Components.Add(component2);
terminal.Components.Add(component3);




for (int i = 0; i < component3.Height; i++)
{
    for (int j = 0; j < component3.Width; j++)
    {
        component3.Display[i, j] = '█';
        //component.Display[i, j] = '█';
    }
}
terminal.PrintScreen();