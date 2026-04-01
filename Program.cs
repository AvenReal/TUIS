// See https://aka.ms/new-console-template for more information


using TUIS;
using TUIS.Components;
using TUIS.Components.Masks;

int height = (int)(Console.WindowHeight * 2.2);
int width = (int)(Console.WindowWidth * 3.8);


DynamicTerminal terminal =
    new DynamicTerminal(width, height, new ConsoleKeyInfo('\t', ConsoleKey.Tab, false, false, false));

Component bg = new Component(terminal, -1, -1, 0, 0);
ImageMask imageMask = new ImageMask(bg, "Images/wallpaper.jpg", true);

SelectableComponent login = new SelectableComponent(terminal, 35, 4, 25, -1);
BackgroundMask loginBg = new BackgroundMask(login);
TextMask loginTextMask = new TextMask(login, "Login:", 1, 1);


SelectableComponent password = new SelectableComponent(terminal, 35, 4, 35, -1);
BackgroundMask passwordBg = new BackgroundMask(password);
TextMask passwordTextMask = new TextMask(password, "Password:", 1, 1);
terminal.Start();