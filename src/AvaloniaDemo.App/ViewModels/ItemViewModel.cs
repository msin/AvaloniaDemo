using AvaloniaDemo.App.Models;
using AvaloniaDemo.App.ViewModels.Entities;
using AvaloniaDemo.App.Views.Filters;
using AvaloniaDemo.CIL.Common;
using AvaloniaDemo.CIL.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaDemo.App.ViewModels;

public partial class ItemViewModel : ViewModelBase
{
    [ObservableProperty] private TItem[] _rows = [];
    [ObservableProperty] private ColumnDef[] _cols = [];
    [ObservableProperty] private string _itemFilterString = string.Empty;

    public ItemFilterViewModel ItemFilterVM { get; }

    public ItemViewModel()
    {
        Cols = ColumnData.Data;
        Rows = ItemData.Load(Cols);

        ItemFilterVM = new ItemFilterViewModel();

        WeakReferenceMessenger.Default.Register<ItemFilterChangedMessage>(this, (r, m) => 
            ((ItemViewModel)r).ItemFilterString = m.Value);
    }

    [RelayCommand]
    private void Edit(object? row)
    {
    }
}
