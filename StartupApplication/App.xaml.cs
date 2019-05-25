using Microsoft.Practices.Prism;
using System.Windows;

namespace StartupApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper bootstrapper = new StartUpBootStrapper();
            bootstrapper.Run();
        }
    }
}
