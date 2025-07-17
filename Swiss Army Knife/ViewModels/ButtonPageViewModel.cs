using CommunityToolkit.Mvvm.ComponentModel;
using SAK.Services;

namespace SAK.ViewModels;

public class ButtonPageViewModel : ViewModelBase
{
    private readonly ITestService _testService;

    public ButtonPageViewModel(ITestService testService)
    {
        _testService = testService;
    }
}
