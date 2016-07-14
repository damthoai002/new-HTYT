using System;
using System.Timers;

namespace UKPI.Core
{
    public class LongTimer
    {
        private TimeSpan interval;
        private Timer timer;
        private Timer daily;
        private volatile int days;
        private int totaldays;
        private double totalMiliseconds;

        public event ElapsedEventHandler Elapsed;

        public bool AutoReset { get; set; }

        public TimeSpan Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
                totaldays = Convert.ToInt32(interval.Days);
                int d = 0;
                if (interval.Hours == 0 && interval.Minutes == 0 && interval.Seconds == 0 && interval.Milliseconds == 0)
                {
                    d = 1;
                    totaldays--;
                }
                TimeSpan span = new TimeSpan(d, interval.Hours, interval.Minutes, interval.Seconds, interval.Milliseconds);
                totalMiliseconds = span.TotalMilliseconds;
                timer.Interval = totalMiliseconds;
            }
        }

        public LongTimer()
        {
            timer = new Timer();
            daily = new Timer();
            AutoReset = true;
            daily.AutoReset = false;
            daily.Interval = new TimeSpan(1, 0, 0, 0, 0).TotalMilliseconds; // 1 day - temporary use 1 minutes for test
            timer.AutoReset = false;
            daily.Elapsed += new ElapsedEventHandler(daily_Elapsed);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);            
        }

        void daily_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (days > 0)
            {
                days--;
            }
            else
            {
                daily.Stop();
                timer.Start();
            }
        }

        public void Start()
        {
            try
            {
                days = totaldays;
                if (days > 0)
                {
                    daily.Start();
                }
                else
                {
                    timer.Start();
                }
            }
            catch (Exception) { }
        }

        public void Stop()
        {
            try
            {
                daily.Stop();
                timer.Stop();
            }
            catch (Exception) { }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Elapsed != null)
            {
                Elapsed.Invoke(sender, e);
            }
            if (AutoReset)
            {
                Start();
            }
        }
    }
}
