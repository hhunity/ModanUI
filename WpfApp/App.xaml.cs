using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static IServiceProvider ServiceProvider { get; private set; }
        public ApplicationSettingsBase AppSettings => Settings.Default;

        protected override void OnExit(ExitEventArgs e)
        {
            AppSettings.Save();  // ここでまとめて保存もできる！
            base.OnExit(e);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton<View1Model>();
            services.AddSingleton<View2Model>();
            services.AddSingleton<View1>();
            services.AddSingleton<View2>();
            services.AddSingleton<MainViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            base.OnStartup(e);

            var mainWindow = new MainWindow(
                ServiceProvider.GetRequiredService<MainViewModel>());
            mainWindow.Show();
        }

    }

}
