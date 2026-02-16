using System.Timers;
using Timer = System.Timers.Timer;

namespace TUIS.Systems;

public class TimeSystem
{
    public readonly Timer Timer;

    public byte MiliSecond
    {
        get;
        private set;
    }
    public TimeSystem()
    {
        Timer = new Timer(100);
        MiliSecond = 0;
        Timer.Elapsed += (_, _) => { MiliSecond = (byte)((MiliSecond + 1) % 100); }; 
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