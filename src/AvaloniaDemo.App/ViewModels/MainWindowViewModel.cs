using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using AvaloniaDemo.App.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using AvaloniaDemo.App.ViewModels.Entities;
using AvaloniaDemo.CIL.Common;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaDemo.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private TItem[] _rows = [];
    [ObservableProperty] private ColumnDef[] _cols = [];

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FilterString))]
    private string _art = string.Empty;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FilterString))]
    private string _name = string.Empty;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FilterString))]
    private string _descr = string.Empty;

    [ObservableProperty] private string _filterString = string.Empty;

    public MainWindowViewModel()
    {
        Cols = ColumnData.Data;
        Rows = ItemData.Load(Cols);
    }

    [RelayCommand]
    private void Edit(object? row)
    {
    }

    partial void OnArtChanged(string? value) => BuildFilter();
    partial void OnNameChanged(string? value) => BuildFilter();
    partial void OnDescrChanged(string? value) => BuildFilter();

    private void BuildFilter()
    {
        List<string> results = [];
        FilterString = string.Empty;

        if (!string.IsNullOrEmpty(Art)) results.Add(ProcessValue(Art, nameof(Art)));
        if (!string.IsNullOrEmpty(Name)) results.Add(ProcessValue(Name, nameof(Name)));
        if (!string.IsNullOrEmpty(Descr)) results.Add(ProcessValue(Descr, nameof(Descr)));

        FilterString = string.Join(" AND ", results.Where(t => !string.IsNullOrEmpty(t)));
    }

    internal string ProcessValue(string? value, string propName)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;

        if (!value.Contains('%')) return $"[{propName}] = '{value}'";

        var result = value.Split('%');
        var first = 0;
        var last = result.Length - 1;

        if (!string.IsNullOrEmpty(result[first])) result[first] = $"StartsWith([{propName}],'{result[first]}')";
        
        if (!string.IsNullOrEmpty(result[last])) result[last] = $"EndsWith([{propName}],'{result[last]}')";

        if (result.Length < 2) return string.Join(" and ", result.Where(t => !string.IsNullOrEmpty(t)));
        
        for (var mid = 1; mid < last; mid++)
        {
            if (string.IsNullOrEmpty(result[mid])) continue;
                
            result[mid] = $"Contains([{propName}],'{result[mid]}')";
        }

        return string.Join(" and ", result.Where(t => !string.IsNullOrEmpty(t)));
        
        // return value.Split('%') switch
        // {
        //     var arr when value.StartsWith('%') => $"EndsWith([{propName}],'{arr.Last()}')",
        //     var arr when value.EndsWith('%') => $"StartsWith([{propName}],'{arr.First()}')",
        //     var arr => $"StartsWith([{propName}],'{arr[0]}') and EndsWith([{propName}],'{arr[1]}')",
        // };
    }
}