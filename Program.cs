// See https://aka.ms/new-console-template for more information


using System.Timers;
using Terminal;
using Terminal.Components;
using Terminal.Components.Masks;

InputSystem inputSystem = new InputSystem();
TimeSystem timeSystem = new TimeSystem();

BoxMask boxMask = new BoxMask(BoxMask.Type.Light);

Terminal.Terminal terminal = new Terminal.Terminal(new []
{
    new Component(50, 20, 1, 1, new Mask[]
    {
        boxMask,
        new TextMask("Salut\nComment ça va ?", 1, 1, TextMask.HorizontalAlignmentEnum.Center, TextMask.VerticalAlignmentEnum.Center ),
    })
} );

timeSystem.AddTimedEvent(((sender, eventArgs) =>
{
    if (timeSystem.Tick % 2 == 0)
    {
        boxMask.BoxType = boxMask.BoxType == BoxMask.Type.Light ? BoxMask.Type.Bold : BoxMask.Type.Light;
    }
}));



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

