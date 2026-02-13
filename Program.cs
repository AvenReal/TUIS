// See https://aka.ms/new-console-template for more information


using System.Security.Cryptography;
using System.Timers;
using Terminal;
using Terminal.Components;
using Terminal.Components.Masks;


Terminal.Terminal terminal = new Terminal.Terminal( new []
{
    new Component(255, 50, 1, 1, new []
    {
        new ImageMask("Images/wallpaper.jpg"),
    }),
    new Component(36, 3, 20, 109, new Mask[]
    {
        new BackgroundMask(),
        new InputMask(null, 1, 1),
        new BoxMask(BoxMask.Type.Light),
        new TextMask("┌┤ Login ├")
    }),
    new Component(36, 3, 25, 109, new Mask[]
    {
        new BackgroundMask(),
        new InputMask(null, 1, 1),
        new BoxMask(BoxMask.Type.Light),
        new TextMask("┌┤ Password ├")
    })
});

terminal.Start();