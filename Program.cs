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

timeSystem.AddTimedEvent(((_, _) =>
{
    if (timeSystem.Tick % 8 == 0)
    {
        boxMask.BoxType = boxMask.BoxType == BoxMask.Type.Light ? BoxMask.Type.Bold : BoxMask.Type.Light;
    }
}));

timeSystem.AddTimedEvent( ((_, _) =>
{
    terminal.Draw();
} ));

inputSystem.Start();
 

while (true)
{
    
}