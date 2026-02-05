// See https://aka.ms/new-console-template for more information


using System.Timers;
using Terminal;
using Terminal.Components;
using Terminal.Components.Masks;

InputSystem inputSystem = new InputSystem();

Terminal.Terminal terminal = new Terminal.Terminal();
Component component = new Component(terminal, 50, 20, 1, 1);
BoxMask boxMask = new BoxMask(component,  BoxMask.Type.Light);






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
    
}

