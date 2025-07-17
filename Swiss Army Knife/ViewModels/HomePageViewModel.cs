using CommunityToolkit.Mvvm.ComponentModel;
using SAK.Services;

namespace SAK.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    private readonly ITestService _testService;

    public HomePageViewModel(ITestService testService)
    {
        _testService = testService;
    }
}
