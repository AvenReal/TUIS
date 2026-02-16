// See https://aka.ms/new-console-template for more information


using TUIS;
using TUIS.Components;
using TUIS.Components.Masks;


Terminal terminal = new Terminal();

// Background

Component bg = new Component(terminal, 316, 53, 1, 1);
ImageMask bgImageMask = new ImageMask(bg, "Images/wallpaper.jpg");

// Login

Component login = new Component(terminal, 50, 4, 20, 133);
BackgroundMask loginBg = new BackgroundMask(login);
InputMask loginInputMask = new InputMask(login, mask =>
{
    InputMask inputMask = (InputMask)mask.Component.Terminal.Components[2].Masks[1];
    inputMask.Enabeled = true;
});
BoxMask loginBoxMask = new BoxMask(login, BoxMask.Type.Light);
TextMask loginTextMask = new TextMask(login, "Login:", 1, 1);

// Password

Component password =  new Component(terminal, 50, 4, 25, 133);
BackgroundMask passwordBg = new BackgroundMask(password);
InputMask passwordInputMask = new InputMask(password, pwInputMask =>
{
    if (loginInputMask.Output != "admin" || pwInputMask.Output != "password")
    {
        loginInputMask.Enabeled = true;
        pwInputMask.NeedRedraw = true;
    }
    else
    {
        ((TextMask)pwInputMask.Component.Masks[3]).Text = "Welcome!";
    }
});
BoxMask passwordBoxMask = new BoxMask(password, BoxMask.Type.Light);
TextMask passwordTextMask = new TextMask(password, "Password:", 1, 1);

// Clock

Component clock = new Component(terminal, 27, 3, 10, 145);
BackgroundMask clockBg = new BackgroundMask(clock);
ClockMask clockMask = new ClockMask(clock);


terminal.Draw();

terminal.TimeSystem.AddTimedEvent( ((_, _) =>
{
    if (terminal.TimeSystem.MiliSecond % 10 == 0)
    {
        clockMask.NeedRedraw = true;
    }
} ));

InputMask inputMask = (InputMask)terminal.Components[1].Masks[1];
inputMask.Enabeled = true;
Console.CursorVisible = true;

terminal.Start();