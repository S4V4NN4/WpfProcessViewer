using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace lab1_sp_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<ProcessInfo> processesInfo = new List<ProcessInfo>();

        private void LoadProcessesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        public void LoadProcesses()
        {
            var processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                    var processInfo = new ProcessInfo();

                    processInfo.ID = process.Id;
                    processInfo.Memory = process.WorkingSet64.ToString() + " bytes";
                    processInfo.Prio = process.BasePriority.ToString();
                    processInfo.Name = process.ProcessName.ToString();

                    var processThreads = process.Threads;
                    processInfo.ThreadCount = processThreads.Count;
                    processInfo.ThreadsInfo = new List<ThreadInfo> { };

                    foreach (ProcessThread thread in processThreads)
                    {
                        var threadInfo = new ThreadInfo();
                        threadInfo.ID = thread.Id;
                        threadInfo.Prio = thread.BasePriority.ToString();

                        processInfo.ThreadsInfo.Add(threadInfo);
                    }

                    processInfo.User = processInfo.GetProcessUser();

                    processesInfo.Add(processInfo);
                }
                catch
                {

                }

                ProcessesGrid.ItemsSource = null;
                ProcessesGrid.ItemsSource = processesInfo;
                CountLabel.Content = $"Процессов: {processesInfo.Count}";
                ThreadsGrid.ItemsSource = null;
            }
        }

        private void ProcessesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProcessesGrid.SelectedItem is ProcessInfo selected)
            {
                ThreadsGrid.ItemsSource = null;
                ThreadsGrid.ItemsSource = selected.ThreadsInfo;
            }
        }
    }
}