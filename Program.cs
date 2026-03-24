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
BigTextTextMask bigTextTextMask =
    new BigTextTextMask(component,
        "Aa Bb Cc Dd Ee Ff Gg Hh Ii Jj Kk Ll Mm Nn Oo Pp Qq Rr Ss Tt Uu Vv Ww Xx Yy Zz Ceci est une phrase de test pour voir comment les lettres s'arranges entre elles");

/*
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
        case ConsoleKey.Tab:
        {
            windowBoxMask.BoxType = (BoxMask.Type)((int)(windowBoxMask.BoxType + 1) % 7);
            break;
        }
    }

    windowBoxMask.NeedReDraw = true;
};
*/

terminal.Start();