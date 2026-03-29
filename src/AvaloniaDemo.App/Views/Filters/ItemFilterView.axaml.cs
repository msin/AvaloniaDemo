using Avalonia.Controls;
using AvaloniaDemo.CIL.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaDemo.App.Views.Filters;

public partial class ItemFilterView : UserControl
{
    public ItemFilterView() => InitializeComponent();
}

public partial class ItemFilterViewModel : ObservableObject
{
    [ObservableProperty] private string _art = string.Empty;
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _descr = string.Empty;

    partial void OnArtChanged(string value) => BuildFilter();
    partial void OnNameChanged(string value) => BuildFilter();
    partial void OnDescrChanged(string value) => BuildFilter();

    private void BuildFilter()
    {
        List<string> results = [];
        
        if (!string.IsNullOrEmpty(Art)) results.Add(ProcessValue(Art, nameof(Art)));
        if (!string.IsNullOrEmpty(Name)) results.Add(ProcessValue(Name, nameof(Name)));
        if (!string.IsNullOrEmpty(Descr)) results.Add(ProcessValue(Descr, nameof(Descr)));

        var filterString = string.Join(" AND ", results.Where(t => !string.IsNullOrEmpty(t)));
        WeakReferenceMessenger.Default.Send(new ItemFilterChangedMessage(filterString));
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
    }
}