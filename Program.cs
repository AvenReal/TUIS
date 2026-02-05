// See https://aka.ms/new-console-template for more information


using System.Timers;
using Terminal;
using Terminal.Components;
using Terminal.Components.Masks;

Terminal.Terminal terminal = new Terminal.Terminal();

Component component = new Component(terminal, 20, 5, 1, 10);
TextMask textMask = new TextMask(component, "Salut, ceci est un test avec du text");
Component component1 = new Component(terminal, 20, 5, 1, 35);
BoxMask boxMask1 = new BoxMask(component1, BoxMask.Type.Light);
TextMask textMask1 = new TextMask(component1, "Salut", TextMask.HorizontalAlignment.Center, TextMask.VerticalAlignment.Center);
Component component2 = new Component(terminal, 20, 5, 20, 10);
BoxMask boxMask2 = new BoxMask(component2, BoxMask.Type.Bold);
Component component3 = new Component(terminal, 20, 5, 1, 70);
BoxMask boxMask3 = new BoxMask(component3, BoxMask.Type.ExtraBold);


InputSystem inputSystem = new InputSystem();

inputSystem.OnKeyPress += (key) =>
{
    switch (key.Key)
    {
        case ConsoleKey.UpArrow:
            if (key.Modifiers == ConsoleModifiers.Shift)
                component2.Height -= 1;
            else
                component2.PosY -= 1;
            break;
        case ConsoleKey.DownArrow:
            if (key.Modifiers == ConsoleModifiers.Shift)
                component2.Height += 1;
            else
                component2.PosY += 1;
            break;
        case ConsoleKey.LeftArrow:
            if (key.Modifiers == ConsoleModifiers.Shift)
                component2.Width -= 1;
            else
                component2.PosX -= 1;
            break;
        case ConsoleKey.RightArrow:
            if (key.Modifiers == ConsoleModifiers.Shift)
                component1.Width += 1;
            else
                component2.PosX += 1;
            break;
    }
}; 

inputSystem.Start();

bool tick = true;
Console.Clear();
System.Timers.Timer timer = new System.Timers.Timer(100);
timer.Elapsed += Tick;
timer.Start();
while (true)
{
    
}
timer.Stop();





void Tick(Object source, ElapsedEventArgs e)
{
    terminal.Draw();
    boxMask1.BoxType = tick ?  BoxMask.Type.Light : BoxMask.Type.ExtraBold;
    tick = !tick;
    component1.NeedRedraw = true;
    string str = Console.ReadLine();
    terminal.Clear();
    if(str == "test")
        component1.IsVisible = !component1.IsVisible;
    textMask1.Text = str;
    component1.NeedRedraw = true;
}

