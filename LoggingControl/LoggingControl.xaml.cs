using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Targets;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace LoggingControl
{
    /// <summary>
    /// Interaction logic for LoggingControl.xaml
    /// </summary>
    public partial class LoggingControl : UserControl
    {
        readonly MemoryEventTarget _logTarget;  // My new custom Target (code is attached here MemoryQueue.cs)

        public static ObservableCollection<LogEventInfo> LogCollection { get; set; }

        public LoggingControl()
        {
            InitializeComponent();

            LogCollection = new ObservableCollection<LogEventInfo>();

            // init memory queue
            _logTarget = new MemoryEventTarget();
            _logTarget.EventReceived += EventReceived;

            // Attach this target to an existing NLog configuration.
            var configuration = LogManager.Configuration;
            configuration.AddRuleForAllLevels(_logTarget);
            LogManager.ReconfigExistingLoggers();

            DataContext = this;
        }

        private void EventReceived(LogEventInfo message)
        {
            // This event handler is executed on its own thread.  Updating the UI is done on the main UI
            // thread.  So update the LogCollection on the UI thread.  While we're at it, scroll to the
            // bottom of the list of messages to keep the most recent items in view.
            Dispatcher.Invoke(new Action(() => {
                // Don't let the log grow without bound
                if (LogCollection.Count >= 2000) LogCollection.RemoveAt(0);
                LogCollection?.Add(message);

                // Scroll to bottom
                int selectedIndex = LogCollection.Count - 1;
                if (selectedIndex < 0)
                    return;
                logView.SelectedItem = LogCollection[selectedIndex];
                logView.ScrollIntoView(logView.SelectedItem);

                // The selected item has a different color. Deselect all so we see the desired color
                // set in the converters.
                logView.UnselectAll();
            }));
        }

        private void LogControlLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
        }
    }
}
