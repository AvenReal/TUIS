// See https://aka.ms/new-console-template for more information


using TUIS;
using TUIS.Components;
using TUIS.Components.Masks;

int height = (int)(Console.WindowHeight * 2);
int width = (int)(Console.WindowWidth * 2);


Terminal terminal = new Terminal(width, height);

// Background

Component bg = new Component(terminal, -1, -1, 0, 0);
ImageMask bgImageMask = new ImageMask(bg, "Images/wallpaper.jpg");


Component component = new Component(terminal, 5, 5, -1, -1);
ImageMask imageMask = new ImageMask(component, "Images/wallpaper.jpg", true);
BoxMask boxMask = new BoxMask(component, BoxMask.Type.Bold);

terminal.InputSystem.OnKeyPress += key =>
{
    switch (key.Key)
    {
        case ConsoleKey.UpArrow:
            if (key.Modifiers == ConsoleModifiers.Shift)
                component.Height--;
            else
                component.PosY--;

            break;
        case ConsoleKey.DownArrow:
            if (key.Modifiers == ConsoleModifiers.Shift)
                component.Height++;
            else
                component.PosY++;

            break;
        case (ConsoleKey.RightArrow):
            if (key.Modifiers == ConsoleModifiers.Shift)
                component.Width++;
            else
                component.PosX++;

            break;
        case ConsoleKey.LeftArrow:
            if (key.Modifiers == ConsoleModifiers.Shift)
                component.Width--;
            else
                component.PosX--;

            break;
    }

    bgImageMask.NeedReDraw = true;
};

terminal.Start();