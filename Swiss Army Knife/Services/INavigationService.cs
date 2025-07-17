using SAK.ViewModels;

namespace SAK.Services
{
    public interface INavigationService
    {
        ViewModelBase NavigateTo(string dest, AssemblyType assemblyType = AssemblyType.MainDll, string assemblyName = null);
    }
}
