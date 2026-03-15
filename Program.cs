// See https://aka.ms/new-console-template for more information


using TUIS;
using TUIS.Components;
using TUIS.Components.Masks;


int height = (int)(Console.WindowHeight * 2.6);
int width = (int)(Console.WindowWidth * 4.7);

Terminal terminal = new Terminal(width, height);

// Background

Component bg = new Component(terminal, -1, -1, 0, 0);
ImageMask bgImageMask = new ImageMask(bg, "Images/wallpaper.jpg");

Component window = new Component(terminal, 15, 10, -1, -1);
BoxMask windowBoxMask = new BoxMask(window, BoxMask.Type.Double, true, Terminal.TextColor.Blue);

terminal.InputSystem.OnKeyPress += key =>
{
    switch (key.Key)
    {
        case ConsoleKey.LeftArrow:
        {
            if (key.Modifiers == ConsoleModifiers.Shift)
                window.Width--;
            else
                window.PosX--;
            break;
        }
        case ConsoleKey.RightArrow:
        {
            if (key.Modifiers == ConsoleModifiers.Shift)
                window.Width++;
            else
                window.PosX++;
            break;
        }
        case ConsoleKey.UpArrow:
        {
            if (key.Modifiers == ConsoleModifiers.Shift)
                window.Height--;
            else
                window.PosY--;
            break;
        }
        case ConsoleKey.DownArrow:
        {
            if (key.Modifiers == ConsoleModifiers.Shift)
                window.Height++;
            else
                window.PosY++;
            break;
        }
    }

    windowBoxMask.NeedReDraw = true;
};

terminal.Start();