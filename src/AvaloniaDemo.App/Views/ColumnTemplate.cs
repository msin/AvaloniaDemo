using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Eremex.AvaloniaUI.Controls.DataGrid;
using Eremex.AvaloniaUI.Controls.Editors;
using AvaloniaDemo.App.ViewModels;
using ColumnDefinition = AvaloniaDemo.App.ViewModels.ColumnDefinition;

namespace AvaloniaDemo.App.Views;

public class ColumnTemplate : ITemplate<object, GridColumn>
{
    public GridColumn Build(object param)
    {
        var columnDef = (ColumnDefinition)param;

        var gridColumn = new GridColumn
        {
            FieldName = columnDef.FieldName,
            Header = columnDef.Header,
            Width = new GridLength(1, GridUnitType.Star),
            MinWidth = 80
        };

        gridColumn.EditorProperties = columnDef.ColumnType switch
        {
            ColumnType.Integer => new SpinEditorProperties
            {
                MaskType = MaskType.Numeric,
                Mask = "d"
            },
            ColumnType.String => new TextEditorProperties(),
            ColumnType.Date => new DateEditorProperties(),
            ColumnType.ComboBox => new ComboBoxEditorProperties
            {
                ItemsSource = MainWindowViewModel.Departments
            },
            _ => new TextEditorProperties()
        };

        return gridColumn;
    }
}
