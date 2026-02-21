namespace TUIS.Components.Masks;

public class ClockMask : Mask
{
    /// <summary>
    /// Add a real time clock to the <see cref="Component"/>.
    /// Warning: The clock is 27 wide by 3 tall.
    /// </summary>
    /// <param name="component">The component which the mask is attached to.</param>
    /// <param name="isVisible">Represent the visibility of the mask (default = true).</param>
    /// <param name="color">The default color of the mask (a mask's <see cref="Behaviour"/>) method can override the color (default = white).</param>
    /// <param name="background">The default background color of the mask (a mask's <see cref="Behaviour"/>) method can override the background color (default = None).</param>
    /// <param name="decoration">The default decoration of the mask (a mask's <see cref="Behaviour"/>) method can override the decoration (default = Default).</param>
    public ClockMask(Component component, bool isVisible = true, TextColor color = TextColor.White,
        BackgroundColor background = BackgroundColor.None, TextDecoration decoration = TextDecoration.Default) : base(
        component, isVisible, color, background, decoration)
    {
        Component.Terminal.TimeSystem.AddTimedEvent(((_, _) =>
        {
            if (Component.Terminal.TimeSystem.MiliSecond % 10 == 0)
            {
                NeedRedraw = true;
            }
        }));
    }

    /// <summary>
    /// Helps to map numbers to their 'big text' version.
    /// </summary>
    private static readonly Dictionary<int, char[]> _numbers = new()
    {
        { 0, ['█', '▀', '█', '█', ' ', '█', '█', '▄', '█'] },
        { 1, [' ', ' ', '█', ' ', ' ', '█', ' ', ' ', '█'] },
        { 2, ['▀', '▀', '█', '█', '▀', '▀', '█', '▄', '▄'] },
        { 3, ['▀', '▀', '█', '▀', '▀', '█', '▄', '▄', '█'] },
        { 4, ['█', ' ', '█', '▀', '▀', '█', ' ', ' ', '█'] },
        { 5, ['█', '▀', '▀', '▀', '▀', '█', '▄', '▄', '█'] },
        { 6, ['█', '▀', '▀', '█', '▀', '█', '█', '▄', '█'] },
        { 7, ['▀', '▀', '█', ' ', ' ', '█', ' ', ' ', '█'] },
        { 8, ['█', '▀', '█', '█', '▀', '█', '█', '▄', '█'] },
        { 9, ['█', '▀', '█', '▀', '▀', '█', '▄', '▄', '█'] }
    };

    /// <summary>
    /// <inheritdoc/>
    /// This <see cref="Mask"/>'s <see cref="Behaviour"/> is to print the big numbers in the right place.
    /// </summary>
    protected override void Behaviour()
    {
        int seconds = DateTime.Now.Second;
        int minutes = DateTime.Now.Minute;
        int hours = DateTime.Now.Hour;

        int secondsUnits = seconds % 10;
        int secondsTenth = seconds / 10;

        int minutesUnits = minutes % 10;
        int minutesTenth = minutes / 10;

        int hoursUnits = hours % 10;
        int hoursTenth = hours / 10;


        for (int i = 0; i < 9; i++)
        {
            DrawChar(i / 3, i % 3, _numbers[hoursTenth][i]);
            DrawChar(i / 3, i % 3 + 4, _numbers[hoursUnits][i]);

            DrawChar(i / 3, i % 3 + 10, _numbers[minutesTenth][i]);
            DrawChar(i / 3, i % 3 + 14, _numbers[minutesUnits][i]);

            DrawChar(i / 3, i % 3 + 20, _numbers[secondsTenth][i]);
            DrawChar(i / 3, i % 3 + 24, _numbers[secondsUnits][i]);
        }

        DrawChar(0, 8, Component.Terminal.TimeSystem.MiliSecond % 20 == 0 ? ' ' : '▄');
        DrawChar(2, 8, Component.Terminal.TimeSystem.MiliSecond % 20 == 0 ? ' ' : '▀');

        DrawChar(0, 18, Component.Terminal.TimeSystem.MiliSecond % 20 == 0 ? ' ' : '▄');
        DrawChar(2, 18, Component.Terminal.TimeSystem.MiliSecond % 20 == 0 ? ' ' : '▀');
    }
}