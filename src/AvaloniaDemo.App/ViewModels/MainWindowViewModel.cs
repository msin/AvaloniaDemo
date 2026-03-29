using AvaloniaDemo.App.Models;
using AvaloniaDemo.App.ViewModels.Entities;
using AvaloniaDemo.CIL.Common;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDemo.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private object? _currentPage;
    [ObservableProperty] private TModule? _form;
    public TModule[] Modules { get; private set; } = ModuleData.Load();

    public MainWindowViewModel()
    {
        CurrentPage = new ItemViewModel();
    }
}
