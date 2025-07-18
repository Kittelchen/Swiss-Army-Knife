using Microsoft.Extensions.DependencyInjection;
using SAK.Common;
using SAK.ViewModels;
using SAK.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAK.Services
{
    public static class IDummyService
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register all necessary services
            RegisterServices(services);

            // Register view models
            RegisterViewModels(services);

            // Register views
            RegisterViews(services);

            // Register windows (if any)
            RegisterWindow(services);

            // Return the configured service collection
            return services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services
                .AddSingleton<ITestService, TestService>()
                .AddSingleton<INavigationService, NavigationService>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services
                .AddTransient<ViewModelBase>()
                .AddTransient<MainViewModel>()
                .AddTransient<HomePageViewModel>()
                .AddTransient<ButtonPageViewModel>();
        }

        private static void RegisterViews(IServiceCollection services)
        {
            services
                .AddTransient<MainView>()
                .AddTransient<HomePageView>()
                .AddTransient<ButtonPageView>();
        }

        private static void RegisterWindow(IServiceCollection services)
        {
            services
                .AddSingleton<ShellWindow>()
                .AddSingleton<MainWindow>();
        }
    }
}
