using NLog.Targets;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingControl
{
    public class MemoryEventTarget : Target
    {
        /// <summary>
        /// Notifies listeners about new event
        /// 
        /// See http://dotnetsolutionsbytomi.blogspot.com/2011/06/creating-awesome-logging-control-with.html
        /// for the original source code.
        /// </summary>
        /// <param name="logEvent">The logging event.</param>
        public event Action<LogEventInfo>? EventReceived;

        protected override void Write(LogEventInfo logEvent)
        {
            if (EventReceived != null)
            {
                EventReceived(logEvent);
            }
        }
    }
}
