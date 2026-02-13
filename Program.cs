// See https://aka.ms/new-console-template for more information


using TUIS;
using TUIS.Components;
using TUIS.Components.Masks;

Terminal terminal = new Terminal( new []
{
    new Component(316, 53, 1, 1, new []
    {
        new ImageMask("Images/wallpaper.jpg", false),
    }),
    new Component(50, 4, 20, 133, new Mask[]
    {
        new BackgroundMask(),
        new InputMask(mask =>
        {
            if(mask.Component == null || mask.Component.Terminal == null)
                return;
            
            InputMask inputMask = (InputMask) mask.Component.Terminal.Components[2].Masks[1];
            
            inputMask.Enabeled = true;
        } , 1, 2),
        new BoxMask(BoxMask.Type.Light),
        new TextMask("Login:", 1, 1)
    }),
    new Component(50, 4, 25, 133, new Mask[]
    {
        new BackgroundMask(),
        new InputMask(null, 1, 2),
        new BoxMask(BoxMask.Type.Light),
        new TextMask("Password:", 1, 1)
    })
});

terminal.Draw();
InputMask inputMask = (InputMask)terminal.Components[1].Masks[1];
inputMask.Enabeled = true;
Console.CursorVisible = true;

terminal.Start();