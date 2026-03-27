// See https://aka.ms/new-console-template for more information


using TUIS;
using TUIS.Components;
using TUIS.Components.Masks;

int height = (int)(Console.WindowHeight * 2);
int width = (int)(Console.WindowWidth * 2);

Terminal terminal = new Terminal(width, height);

// Background
/*
Component bg = new Component(terminal, -1, -1, 0, 0);
ImageMask bgImageMask = new ImageMask(bg, "Images/wallpaper.jpg");
*/

Component component = new Component(terminal, -1, -1, 1, 1);
BigTextMask bigTextMask = new BigTextMask(component, "Made by AvenReal :)");

terminal.InputSystem.OnKeyPress += key =>
{
    if (key.Key == ConsoleKey.Tab)
    {
        bigTextMask.Font = (BigTextMask.FontType)(((int)bigTextMask.Font + 1) % 2);
    }
};

terminal.Start();