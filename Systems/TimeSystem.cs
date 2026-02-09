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
        Timer.Elapsed += (_, _) => { Tick = (byte)((Tick + 1) % 256); }; 
    }

    public void AddTimedEvent(ElapsedEventHandler? eventHandler)
    {
        Timer.Elapsed += eventHandler;
    }

    public void Start()
    {
        Timer.Start();
    }
}