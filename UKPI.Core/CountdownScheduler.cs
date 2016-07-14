using System;
using System.Timers;

namespace UKPI.Core
{
    public class CountdownScheduler : IScheduler
    {
        private DateTime _time;
        private LongTimer _timer;
        public event ElapsedEventHandler DoWork;
        const int MIN_SECOND = 30;

        public ScheduleType ScheduleType { get; set; }

        public int Days { get; set; }

        /// <summary>
        /// Gets or sets time for scheduler to fire event. Use Server time.
        /// </summary>
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        public CountdownScheduler()
        {
            _time = new DateTime();
            _timer = new LongTimer();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.AutoReset = false;
            ScheduleType = ScheduleType.Daily;
            Days = 0;
        }

        public CountdownScheduler(DateTime time)
            : this()
        {
            _time = time;
        }

        public CountdownScheduler(DateTime time, int days)
            : this(time)
        {
            Days = days;
        }

        public void Start()
        {
            _timer.Interval = GetPeriod();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DoWork != null)
            {
                DoWork.Invoke(sender, e);
            }
            Start();
        }

        protected TimeSpan GetPeriod()
        {
            int days = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            DateTime now = DateTime.Now;

            DateTime deadline = _time;
            TimeSpan span = new TimeSpan();
            TimeSpan ts;
            switch (ScheduleType)
            {
                case ScheduleType.Date:
                    span = deadline.Subtract(now);
                    break;
                case ScheduleType.Days:
                    days = Days;
                    hours = deadline.Hour - now.Hour;
                    minutes = deadline.Minute - now.Minute;
                    seconds = deadline.Second - now.Second;
                    ts = new TimeSpan(hours, minutes, seconds);
                    if (ts.TotalSeconds < MIN_SECOND)
                    {
                        days = 1;
                    }
                    span = new TimeSpan(days, hours, minutes, seconds);
                    break;
                case ScheduleType.Monthly:
                    int year = now.Year;
                    int month = now.Month;
                    int day = deadline.Day;
                    DateTime tmp = new DateTime(year, month, day, deadline.Hour, deadline.Minute, deadline.Second, deadline.Millisecond);
                    TimeSpan dur = tmp.Subtract(now);
                    if (dur.TotalMilliseconds < 0)
                    {
                        if (now.Month == 12)
                        {
                            year++;
                        }
                        month++;
                        if (month > 12)
                        {
                            month = 1;
                        }
                    }
                    DateTime end = new DateTime(year, month, day, deadline.Hour, deadline.Minute, deadline.Second, deadline.Millisecond);
                    span = end.Subtract(now);
                    break;
                case ScheduleType.Weekly:
                    days = (int)deadline.DayOfWeek - (int)now.DayOfWeek;
                    hours = deadline.Hour - now.Hour;
                    minutes = deadline.Minute - now.Minute;
                    seconds = deadline.Second - now.Second;
                    ts = new TimeSpan(days, hours, minutes, seconds);
                    if (ts.TotalSeconds < MIN_SECOND)
                    {
                        days = 7;
                    }
                    span = new TimeSpan(days, hours, minutes, seconds);
                    break;
                default: // ScheduleType.Daily
                    days = 0;
                    hours = deadline.Hour - now.Hour;
                    minutes = deadline.Minute - now.Minute;
                    seconds = deadline.Second - now.Second;
                    ts = new TimeSpan(hours, minutes, seconds);
                    if (ts.TotalSeconds < MIN_SECOND)
                    {
                        days = 1;
                    }
                    span = new TimeSpan(days, hours, minutes, seconds);
                    break;
            }

            return span;
        }
    }

    public enum ScheduleType
    {
        Date,
        Days,
        Monthly,
        Daily,
        Weekly
    }
}
