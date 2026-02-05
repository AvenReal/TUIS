using System.Timers;
using Timer = System.Timers.Timer;

namespace Terminal;

public class TimeSystem
{
    public readonly Timer Timer;

    public byte Tick
    {
        get;
        private set;
    }
    public TimeSystem()
    {
        Timer = new Timer(100);
        Tick = 0;
        Timer.Elapsed += (sender, args) => { Tick = (byte)((Tick + 1) % 256); }; 
        Timer.Start();
    }

    public void AddTimedEvent(ElapsedEventHandler? eventHandler)
    {
        Timer.Elapsed += eventHandler;
    }
}