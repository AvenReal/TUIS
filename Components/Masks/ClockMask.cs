namespace TUIS.Components.Masks;

public class ClockMask : Mask
{
    public ClockMask(Component component) : base(component)
    {
        Component.Terminal.TimeSystem.AddTimedEvent( ((_, _) =>
        {
            if (Component.Terminal.TimeSystem.MiliSecond % 10 == 0)
            {
                NeedRedraw = true;
            }
        } ));
    }

    private static Dictionary<int, char[]> _numbers = new()
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