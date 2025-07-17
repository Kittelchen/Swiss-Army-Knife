using Microsoft.Extensions.DependencyInjection;
using SAK.Common;
using SAK.ViewModels;
using SAK.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SAK.Services
{
    public enum AssemblyType
    {
        MainDll, //is the one from the exe
        CommonDll, //is the one from the DLL
        OtherDll //specifies another Dll as string then
    }
    public class NavigationService : INavigationService
    {
        private readonly ShellWindow _shellWindow;
        private readonly IServiceProvider _serviceProvider;
        private Assembly _assembly;

        public NavigationService(IServiceProvider serviceProvider, ShellWindow shellWindow)
        {
            _serviceProvider = serviceProvider;
            _shellWindow = shellWindow;
            //_assembly = Assembly.GetExecutingAssembly();
        }
        public virtual ViewModelBase NavigateTo(string viewName, AssemblyType assemblyType = AssemblyType.MainDll, string assemblyName = null)
        {
            var stackTrace = new StackTrace();

            var callingMethod = stackTrace.GetFrame(1)?.GetMethod();
            var callingAssembly = callingMethod.DeclaringType?.Assembly;

            if (assemblyType is AssemblyType.MainDll)
            {
                _assembly = callingAssembly;
            }
            else if (assemblyType is AssemblyType.CommonDll)
            {
                _assembly = typeof(NavigationService).Assembly;
            }
            else
            {
                _assembly = Assembly.LoadFrom(assemblyName);
            }

            // Construct the full type name for the view and view model
            string viewTypeName = $"{viewName}View";
            string viewModelTypeName = $"{viewName}ViewModel";

            // Use reflection to find types that match the view and view model names
            Type viewType = _assembly.GetTypes().FirstOrDefault(t => t.Name == viewTypeName);
            Type viewModelType = _assembly.GetTypes().FirstOrDefault(t => t.Name == viewModelTypeName);

            if (viewType is null) throw new Exception($"Could not find view or view model for '{viewName}'");
            
            if (viewModelType is null) throw new Exception($"Could not find view model for '{viewName}'");


            // Resolve the view and view model from the DI container
            var view = _serviceProvider.GetRequiredService(viewType);
            var viewModel = _serviceProvider.GetRequiredService(viewModelType);

            // Set the view's DataContext to the resolved view model
            (view as dynamic).DataContext = viewModel;

            return (ViewModelBase) viewModel;

            // Set the MainWindow's content to the new view
            //_shellWindow.Content = view;

        }
    }
}
