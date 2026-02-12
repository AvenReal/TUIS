// See https://aka.ms/new-console-template for more information


using System.Security.Cryptography;
using System.Timers;
using Terminal;
using Terminal.Components;
using Terminal.Components.Masks;




BoxMask boxMask = new BoxMask(BoxMask.Type.Light);



TextMask textMask = new TextMask("", 1, 1);
InputMask inputMask = new InputMask(mask =>
{
    textMask.Text += mask.Output;
    mask.Component?.Masks.ForEach(mask1 =>
    {
        if (mask1 is BoxMask boxMask1)
        {
            boxMask1.BoxType = BoxMask.Type.Light;
        }
    });
}, 1, 1);


InputMask shellInputMask = new InputMask(mask =>
{
    if (mask.Output != "clear") return;
    
    textMask.Text = "";
    textMask.Component!.Draw();

});

Terminal.Terminal terminal = new Terminal.Terminal(new []
{
    new Component(236, 42, 1, 1, new Mask[]
    {
        
        inputMask,
        textMask,
        new ImageMask("Images/wallpaper.jpg"),
        /*boxMask*/
    }),/*
    new Component(50, 20, 1, 51, new Mask[]
    {
        new BoxMask(BoxMask.Type.Light),
        shellInputMask,
    })
    {
        IsVisible = false,
    }*/
} );

terminal.InputSystem.OnKeyPress += key =>
{
    switch (key.Key)
    {
        case ConsoleKey.I:
            boxMask.BoxType = BoxMask.Type.Bold;
            boxMask.Component!.Draw();
            inputMask.HorizontalPadding = (byte)(textMask.Text.Length % inputMask.Component!.Width + 1);
            inputMask.VerticalPadding = (byte)(textMask.Text.Length / inputMask.Component.Width + 1);
            inputMask.Enabeled = true;
            break;
        case ConsoleKey.J:
            
            shellInputMask.Component!.IsVisible = true;
            
            shellInputMask.Enabeled = true;
            shellInputMask.Component!.Draw();
            
            shellInputMask.Component.IsVisible = false;
            break;
    }
};

terminal.TimeSystem.AddTimedEvent((_, _) => {terminal.Draw();});

terminal.Start();
 
