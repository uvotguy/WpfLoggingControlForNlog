using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLoggingControlForNlog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            InitializeComponent();

            // Apply the global style defined in App.xaml
            Style = (Style)FindResource(typeof(Window));
        }

        private void LogErrorMessage(object sender, RoutedEventArgs e)
        {
            logger.Error("This is an error message.");
        }

        private void LogWarningMessage(object sender, RoutedEventArgs e)
        {
            logger.Warn("You have been warned.");
        }

        private void LogMessage(object sender, RoutedEventArgs e)
        {
            logger.Info("This is an informational message");
        }

        private void LogDebuggingMessage(object sender, RoutedEventArgs e)
        {
            logger.Debug("This is a debugging message.");
        }

        private void LogTraceMessage(object sender, RoutedEventArgs e)
        {
            logger.Trace("This is a trace message.");
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            logger.Info("Application exiting ...");
        }
    }
}
