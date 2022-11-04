using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SharedLogic
{
    public class TimerHelper
    {
        public static void Do(Action action,int sec)
        {
            var t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, sec);
            t.Tick += (o, args) =>
                          {
                              Task.Factory.StartNew(action);
                              t.Stop();
                          };
            t.Start();
        }
    }
}