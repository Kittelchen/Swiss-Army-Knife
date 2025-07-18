using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SAK.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SAK.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private bool _isPaneOpen = false;

    [ObservableProperty]
    private ViewModelBase _currentPage;

    [ObservableProperty]
    private ListItemTemplate? _selectedItem;

    partial void OnSelectedItemChanged(ListItemTemplate? value)
    {
        ITestService testService = new TestService();
        if (value is null) return;
        CurrentPage = _navigationService.NavigateTo(value.Label + "Page");

    }
    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel)),
        new ListItemTemplate(typeof(ButtonPageViewModel))
    };
    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        
    }

    [RelayCommand] 
    private void  TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;

        //await Task.CompletedTask;
    }
}

public class ListItemTemplate
{
    public string Label { get; }
    public Type ModelType { get; }

    public ListItemTemplate(Type type)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");
    }

    public ListItemTemplate() { }
}
