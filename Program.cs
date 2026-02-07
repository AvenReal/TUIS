// See https://aka.ms/new-console-template for more information


using System.Timers;
using Terminal;
using Terminal.Components;
using Terminal.Components.Masks;

InputSystem inputSystem = new InputSystem();
TimeSystem timeSystem = new TimeSystem();

TextMask textMask = new TextMask("", 1, 1, TextMask.HorizontalAlignmentEnum.Center, TextMask.VerticalAlignmentEnum.Center);

InputMask inputMask = new InputMask(mask =>
{
    textMask.Text = mask.Output;
}, 1, 1);

Terminal.Terminal terminal = new Terminal.Terminal(new []
{
    new Component(50, 20, 1, 1, new Mask[]
    {
        new BoxMask(BoxMask.Type.Light),
        inputMask
    }),
    new Component(50, 20, 1, 51, new Mask[]
    {
        new BoxMask(BoxMask.Type.Light),
        textMask
    })
} );

inputSystem.OnKeyPress += key =>
{
    if (key.Key == ConsoleKey.I)
    {
        inputMask.Enabeled = true;
    }
};

timeSystem.AddTimedEvent((_, _) => {terminal.Draw();});

inputSystem.Start();
 

while (true)
{
    
}