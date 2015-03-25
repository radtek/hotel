using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CheckIn
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process process = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(process.ProcessName).Any(p => p.Id != process.Id))
            {
                MessageBox.Show("您的程序已经启动!");
                Application.Current.Shutdown();
                return;
            }
            base.OnStartup(e);
        }
    }
}
