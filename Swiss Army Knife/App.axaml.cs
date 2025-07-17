using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SAK.Services;
using SAK.ViewModels;
using SAK.Views;
using System;
using System.Threading.Tasks;
using SAK.Common;

namespace SAK;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    { 
        BindingPlugins.DataValidators.RemoveAt(0);

        IServiceCollection services = IDummyService.ConfigureServices();

        _serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = _serviceProvider.GetRequiredService<ShellWindow>();
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            var view = _serviceProvider.GetRequiredService<MainView>();
            var viewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            // Set the view's DataContext to the resolved view model
            (view as dynamic).DataContext = viewModel;

            // Set the MainWindow's content to the new view
            mainWindow.Content = view;

            mainWindow.DataContext = mainViewModel;
            desktop.MainWindow = mainWindow;

            /*desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };*/
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            /*singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };*/
        }

        base.OnFrameworkInitializationCompleted();
    }
}
